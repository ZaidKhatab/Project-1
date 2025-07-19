using Domains.Enums;
namespace Domains.DTos;

public class AddUserResponseDto
{
    public OpStatus Status { get; set; }
    public string Message { get; set; }
}
