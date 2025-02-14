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
internal class SkalmejenRoundDbo
{
    public const string TableName = "contest_round";

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string RoundId { get; set; }
    public string ContestId { get; set; }
    public int RoundIndex { get; set; }
    public SkalmejenRoundTypeDbo RoundType { get; set; }
    public string RoundName { get; set; }
    public string? RoundDescription { get; set; }
    public string? HelpInfo { get; set; }
    public int? NumberOfSeconds { get; set; }
    public decimal? PointFactor { get; set; }
    public string? SoundByteId { get; set; }

    public SkalmejenRound ToDomain(SkalmejenSoundByteDbo? SoundByte) => RoundType switch
    {
        SkalmejenRoundTypeDbo.Buzzer => new SkalmejenBuzzerRound(
            RoundId: Guid.Parse(RoundId),
            ContestId: Guid.Parse(ContestId),
            RoundName: RoundName,
            Description: RoundDescription,
            HelpInfo: HelpInfo,
            NumberOfSeconds: NumberOfSeconds!.Value,
            PointFactor: PointFactor,
            SoundByte: SoundByte!.ToDomain()
        ),
        _ => throw new InvalidOperationException("Cannot convert round DBO")
    };
}

internal static class SkalmejenRoundDboExtensions
{

    public static SkalmejenRoundDbo ToDbo(this SkalmejenRound rnd, int index) => rnd switch
    {
        SkalmejenBuzzerRound bz => bz.ToDbo(index),
        _ => throw new InvalidOperationException("")
    };

    public static SkalmejenRoundDbo ToDbo(this SkalmejenBuzzerRound rnd, int index) => new SkalmejenRoundDbo
    {
        RoundId = rnd.RoundId.ToString(),
        ContestId = rnd.ContestId.ToString(),
        RoundIndex = index,
        RoundName = rnd.RoundName,
        RoundDescription = rnd.Description,
        HelpInfo = rnd.HelpInfo,
        NumberOfSeconds = rnd.NumberOfSeconds,
        PointFactor = rnd.PointFactor,
        RoundType = SkalmejenRoundTypeDbo.Buzzer,
        SoundByteId = rnd.SoundByte.SoundByteId.ToString()
    };
}


