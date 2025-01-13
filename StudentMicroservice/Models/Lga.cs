using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentMicroservice.Models;

public class Lga
{
    public string LgaId { get; set; } = Guid.NewGuid().ToString();

    [StringLength(100)]
    public required string LgaName { get; set; }


    [ForeignKey("State")]
    public required string StateId { get; set; }
    public State State { get; set; }
}
