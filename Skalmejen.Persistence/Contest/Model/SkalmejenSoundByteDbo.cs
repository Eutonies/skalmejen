using Skalmejen.Common.Contest.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Skalmejen.Persistence.Contest.Model;
[Table(TableName)]
internal class SkalmejenSoundByteDbo
{
    public const string TableName = "sound_byte";

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string SoundByteId { get; set; }
    public SkalmejenMusicProviderDbo MusicProvider { get; set; }
    public string? TrackId { get; set; }
    public decimal? StartAt { get; set; }
    public decimal? EndAt { get; set; }

    public SkalmejenSoundByte ToDomain() => MusicProvider switch
    {
        SkalmejenMusicProviderDbo.Spotify => new SkalmejenSpotifySoundByte(
            SoundByteId: Guid.Parse(SoundByteId),
            TrackId: TrackId!,
            StartAt: StartAt!.Value,
            EndAt: EndAt!.Value
            ),
        _ => throw new NotSupportedException()
    };
}

internal static class SkalmejenSoundByteDboExtensions 
{
    public static SkalmejenSoundByteDbo ToDbo(this SkalmejenSoundByte sb) => sb switch
    {
        SkalmejenSpotifySoundByte sp => new SkalmejenSoundByteDbo
        {
            SoundByteId = sp.SoundByteId.ToString(),
            MusicProvider = SkalmejenMusicProviderDbo.Spotify,
            TrackId = sp.TrackId.ToString(),
            StartAt = sp.StartAt,
            EndAt = sp.EndAt
        },
        _ => throw new NotSupportedException()
    };
}

