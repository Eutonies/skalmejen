namespace Skalmejen.Common.Contest.Model;
public abstract record SkalmejenRound(
    Guid RoundId,
    Guid ContestId,
    string RoundName,
    string? Description,
    string? HelpInfo,
    SkalmejenRountType RountType,
    decimal? PointFactor
    );

public record SkalmejenBuzzerRound(
    Guid RoundId,
    Guid ContestId,
    string RoundName,
    string? Description,
    string? HelpInfo,
    int NumberOfSeconds,
    decimal? PointFactor,
    SkalmejenSoundByte SoundByte
    ) : SkalmejenRound(
        RoundId, 
        ContestId, 
        RoundName,
        Description,
        HelpInfo,
        SkalmejenRountType.Buzzer, 
        PointFactor)
{

}
