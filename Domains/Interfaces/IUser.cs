using Domains.DTos;

namespace Domains.Interfaces;

public interface IUser
{
    AddUserResponseDto CreateUser(AddUserRequestDto request);
    AddUserResponseDto Login(string emailAddress, string password);
}
