using Domains.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domains.Entities;

public class User : Base<Guid>
{
    [Required]
    public string UserName { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string EmailAddress { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string Country { get; set; }
    public StatusTypeEnum StatusType { get; set; }
    public UserTypeEnum UserType { get; set; }
}
