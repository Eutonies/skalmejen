namespace Skalmejen.Common.Contest.Model;
public record SkalmejenContest(
    Guid ContestId,
    string ContestName,
    string? Description,
    string ContestCode,
    IReadOnlyCollection<SkalmejenRound> Rounds
    );
