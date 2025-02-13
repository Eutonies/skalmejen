using Microsoft.AspNetCore.Components;

namespace Skalmejen.UI.Pages.Setup;

public partial class RoundComponent
{
    [Parameter]
    public long ContestId { get; set; }

    [Parameter]
    public Round? ExistingRound { get; set; }

    [Parameter]
    public RoundEditType? TypeForNew { get; set; }

    [Parameter]
    public Round? Before { get; set; }
    private bool IsFirst => Before == null;

    [Parameter]
    public Round? After { get; set; }
    public bool IsLast => After == null;

    [Parameter]
    public Action<Contest> UpdateContestUI { get; set; }

    [Parameter]
    public Action<string?, int?> PlaySoundAction { get; set; }

    [Parameter]
    public Func<bool> AllowPlaySound { get; set; }


    [Inject]
    public IHeadQuartersContestAdminRepo ContestRepo { get; set; }


    public const int OptionsPerQuestion = 4;

    private RoundEditType EditType => ExistingRound switch
    {
        null => TypeForNew!.Value,
        BuzzerRound _ => RoundEditType.Buzzer,
        _ => RoundEditType.Question
    };

    private RoundEditData? _editData;

    private void OnSaveButtonClicked()
    {
        _ = Task.Run(async () =>
        {
            if(_editData != null && _editData.CanSave)
            {
                var reloaded = EditType switch
                {
                    RoundEditType.Buzzer when _editData.SoundBytes != null => await ContestRepo.UpsertBuzzerRound(
                        ContestId, 
                        roundId: _editData.RoundId, 
                        roundName: _editData.RoundName!, 
                        bytes: _editData.SoundBytes!, 
                        durationInSeconds: _editData.DurationInSeconds!.Value, 
                        soundName: _editData.SoundName, 
                        points: _editData.Points,
                        extraSeconds: _editData.AdditionalSeconds
                    ),
                    RoundEditType.Buzzer => await ContestRepo.UpsertBuzzerRound(
                        ContestId,
                        roundId: _editData.RoundId,
                        roundName: _editData.RoundName!,
                        durationInSeconds: _editData.DurationInSeconds!.Value,
                        existingSoundId: _editData.SoundId!,
                        points: _editData.Points,
                        extraSeconds: _editData.AdditionalSeconds
                    ),

                    _ => await ContestRepo.UpsertOptionRound(
                        contestId: ContestId,
                        roundId: _editData.RoundId,
                        roundName: _editData.RoundName,
                        points: _editData.Points,
                        durationInSeconds: _editData.DurationInSeconds!.Value,
                        question: _editData.Question!,
                        options: _editData.Options.Select(opt => (new RoundOption(0L, opt.Value), opt.IsCorrect)).ToArray() 
                        )
                };
                UpdateContestUI(reloaded);

            }
        });
    }

    private void OnUpButtonClicked()
    {
        if(ExistingRound != null && Before != null)
        {
            _ = Task.Run(async () =>
            {
                var reloaded = await ContestRepo.SwapIndexes(ExistingRound.RoundId, Before.RoundId);
                UpdateContestUI(reloaded);
            });

        }
    }

    private void OnDownButtonClicked()
    {
        if (ExistingRound != null && After != null)
        {
            _ = Task.Run(async () =>
            {
                var reloaded = await ContestRepo.SwapIndexes(ExistingRound.RoundId, After.RoundId);
                UpdateContestUI(reloaded);
            });

        }
    }

    private void OnDeleteButtonClicked()
    {
        if (ExistingRound != null)
        {
            _ = Task.Run(async () =>
            {
                var reloaded = await ContestRepo.DeleteRound(ExistingRound.RoundId);
                UpdateContestUI(reloaded);
            });

        }
    }

    private void OnPlayButtonClicked()
    {
        if (ExistingRound != null && ExistingRound is BuzzerRound buzz)
        {
            PlaySoundAction(buzz.SoundId.ToString(), (int) buzz.RoundTime.TotalSeconds);
        }

    }

    private bool CanSave => _editData != null && _editData.CanSave;
    private bool CanUp => ExistingRound != null && Before != null;
    private bool CanDown => ExistingRound != null && After != null;

    private bool CanPlay => ExistingRound != null && (ExistingRound is BuzzerRound buzz) && AllowPlaySound();


    private void UpdateComponent() => _ = InvokeAsync(StateHasChanged);

    private async Task OnDragDropFileUpdate(FileDragDropResult res)
    {
        if(res is FileDragDropSuccess succ && _editData != null)
        {
            _editData.SoundBytes = succ.Bytes;
        }
        await Task.CompletedTask;
    }

    protected override async Task OnParametersSetAsync()
    {
        _editData = (ExistingRound, EditType) switch
        {
            (null, RoundEditType.Buzzer) => new RoundEditData((BuzzerRound?)null, UpdateComponent),
            (null, RoundEditType.Question) => new RoundEditData((QuestionRound?)null, UpdateComponent),
            (BuzzerRound rnd, _) => new RoundEditData(rnd, UpdateComponent),
            (QuestionRound rnd, _) => new RoundEditData(rnd, UpdateComponent),
            _ => throw new NotImplementedException()
        };
        await InvokeAsync(StateHasChanged);

    }

    private class RoundEditData
    {
        public RoundEditData(BuzzerRound? existingRound, Action updateUI)
        {
            _editType = RoundEditType.Buzzer;
            UpdateUI = updateUI;
            RoundId = existingRound?.RoundId;
            RoundName = existingRound?.RoundName ?? "Buzzer Round";
            Points = existingRound?.Points ?? 10;
            AdditionalSeconds = existingRound?.AdditionalSeconds ?? 0;
            SoundId = (existingRound?.SoundId)?.ToString();
            DurationInSeconds = (int?) (existingRound?.RoundTime)?.TotalSeconds;
            _soundName = existingRound?.SongName;
        }
        private Action UpdateUI;

        public long? RoundId { init; get; }
        public string RoundName { get; set; }
        public int Points { get; set; }
        public string? Question { get; set; }
        public int? DurationInSeconds { get; set; }
        private byte[]? _soundBytes;
        public byte[]? SoundBytes { get => _soundBytes; set
            {
                _soundBytes = value;
                UpdateFromSoundBytes();
            }
        }
        public int AdditionalSeconds { get; set; }

        private string? _soundName;
        public string SoundName => _soundName ?? "Unknown";

        public string? SoundId { get; set; }

        public IReadOnlyCollection<OptionEntry> Options = [];
        


        private void UpdateFromSoundBytes()
        {
            if(_soundBytes == null)
            {
                _soundName = null;
                return;
            }
            try
            {
                var metaData = _soundBytes.Mp3MetaDataFor();
                DurationInSeconds = (int) metaData.DurationInSeconds;
                _soundName = metaData.TrackName;
                if (string.IsNullOrWhiteSpace(_soundName))
                    _soundName = metaData.ArtistName;
                if (string.IsNullOrWhiteSpace(_soundName))
                    _soundName = "Unknown";
                UpdateUI();
            }
            catch { }
        }

        public bool CanSave => !string.IsNullOrWhiteSpace(RoundName) && Points > 0 && DurationInSeconds > 0 &&
            (
              (_editType == RoundEditType.Buzzer && (SoundBytes != null || SoundId != null))||
              (
                 _editType == RoundEditType.Question && 
                 !string.IsNullOrWhiteSpace(Question) && 
                 Options.Where(_ => !string.IsNullOrWhiteSpace(_.Value)).Count() == OptionsPerQuestion &&
                 Options.Where(_ => !string.IsNullOrWhiteSpace(_.Value))
                        .Select(_ => _.Value!.ToLower())
                        .Distinct()
                        .Count() == OptionsPerQuestion &&
                 Options.Where(_ => _.IsCorrect).Count() == 1  


              )
            );
    }

    private class OptionEntry
    {
        public string Value { get; set; }
        public bool IsCorrect { get; set; }
    }


}
