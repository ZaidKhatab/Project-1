namespace Domains.DTos;

public class AddUserRequestDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string EmailAddress { get; set; }
    public string Country { get; set; }
}
