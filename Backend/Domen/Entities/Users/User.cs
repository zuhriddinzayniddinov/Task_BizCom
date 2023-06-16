using System.ComponentModel.DataAnnotations;
using Topshiriq.Domain.Enums;
using Topshiriq.Domain.Enums;

namespace Topshiriq.Domain.Entities.Users;

public class User
{
    [Key]
    public int Id { get; set; }
    [MinLength(2)]
    [MaxLength(20)]
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public PhoneCompony PhoneCompony { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    public DateTime Birthdate { get; set; }
    public string Salt { get; set; }
    public string PasswordHash { get; set; }
    public UserRole Role { get; set; }
}

