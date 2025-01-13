using System.ComponentModel.DataAnnotations;

namespace StudentMicroservice.Models;

public class State
{
    [Key]
    public string StateId { get; set; } = Guid.NewGuid().ToString();

    [StringLength(100)]
    public required string StateName { get; set; }
}
