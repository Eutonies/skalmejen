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
    public string ContestName { get; set; }
    public string? ContestDescription { get; set; }
    public string ContestCode { get; set; }

    public SkalmejenContest ToDomain(IEnumerable<SkalmejenRoundDbo> rounds) => new SkalmejenContest(
        ContestId: Guid.Parse(ContestId),
        ContestName: ContestName,
        Description: ContestDescription,
        ContestCode: ContestCode,
        Rounds: rounds
          .


        );

}
