using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentMicroservice.Models;

public class Student : IdentityUser
{

    [ForeignKey("Lga")]
    public required string LgaId { get; set; }
    public Lga Lga { get; set; }

}
