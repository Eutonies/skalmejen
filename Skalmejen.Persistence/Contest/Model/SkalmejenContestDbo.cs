using Skalmejen.Common.Contest.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skalmejen.Persistence.Contest.Model;
[Table(TableName)]
internal class SkalmejenContestDbo
{
    public const string TableName = "contest";

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public string ContestId { get; set; }
    public string CreatorMalarkeyId { get; set; }
    public string ContestName { get; set; }
    public string? ContestDescription { get; set; }
    public string ContestCode { get; set; }

    public SkalmejenContest ToDomain(IEnumerable<SkalmejenRoundDbo> rounds, IEnumerable<SkalmejenSoundByteDbo> soundBytes) => new SkalmejenContest(
        ContestId: Guid.Parse(ContestId),
        CreatorMalarkeyId: Guid.Parse(CreatorMalarkeyId),
        ContestName: ContestName,
        Description: ContestDescription,
        ContestCode: ContestCode,
        Rounds: Convert(rounds, soundBytes)
        );

    private IReadOnlyCollection<SkalmejenRound> Convert(IEnumerable<SkalmejenRoundDbo> rounds, IEnumerable<SkalmejenSoundByteDbo> sounds)
    {
        var soundMap = sounds
            .ToDictionary(_ => _.SoundByteId);
        var returnee = rounds
            .OrderBy(_ => _.RoundIndex)
            .Select(_ => _.ToDomain(soundMap!.GetValueOrDefault(_.SoundByteId)))
            .ToList();
        return returnee;
    }

}
