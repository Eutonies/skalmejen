namespace Skalmejen.Common.Contest.Model;
public record SkalmejenContest(
    Guid ContestId,
    Guid CreatorMalarkeyId,
    string ContestName,
    string? Description,
    string ContestCode,
    IReadOnlyCollection<SkalmejenRound> Rounds
    );
