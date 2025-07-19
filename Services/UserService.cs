using Domains.DTos;
using Domains.Entities;
using Domains.Enums;
using Domains.Interfaces;
using Infrastructure;

namespace Services;

public class UserService : IUser
{
    private static List<User> _users;

    public UserService()
    {
        if (_users == null)
        {
            _users = new List<User>();
        }
    }

    public AddUserResponseDto CreateUser(AddUserRequestDto request)
    {
        var response = new AddUserResponseDto();

        try
        {
            var isValidate = IsValidUser(request);
            if (isValidate)
            {
                var user = new User()
                {
                    UserName = request.UserName,
                    Password = request.Password,
                    EmailAddress = request.EmailAddress,
                    Country = request.Country,
                };
                user.IsDeleted = false;
                user.Id = Guid.NewGuid();
                user.CreatedAt = DateTime.Now;
                user.UserType = UserTypeEnum.Client;
                user.StatusType = StatusTypeEnum.Active;
                user.Password = Encryption.Hash(user.Password);
                _users.Add(user);
                response.Status = OpStatus.Success;
                response.Message = "User created successfully.";
            }
            else
            {
                response.Status = OpStatus.AlreadyExist;
                response.Message = "Email Address or Username is already exists";
            }
        }
        catch (Exception ex)
        {
            response.Status = OpStatus.Error;
            response.Message = $"An error occurred while creating the user: {ex.Message}";
        }
        return response;
    }

    public AddUserResponseDto Login(string emailAddress, string password)
    {
        var response = new AddUserResponseDto();
        if (string.IsNullOrWhiteSpace(emailAddress) || string.IsNullOrWhiteSpace(password))
        {
            response.Status = OpStatus.Warning;
            response.Message = "Email Address and Password cannot be empty.";
            return response;
        }
        var user = _users.FirstOrDefault(u => u.EmailAddress == emailAddress && u.IsDeleted == false);
        if (user != null)
        {
            if (Encryption.Verify(password, user.Password))
            {
                response.Status = OpStatus.Success;
                response.Message = "Login successful.";
                return response;
            }
            else
            {
                response.Status = OpStatus.Warning;
                response.Message = "Invalid password.";
                return response;
            }
        }
        else
        {
            response.Status = OpStatus.Error;
            response.Message = "User not found.";
            return response;
        }
    }

    private bool IsValidUser(AddUserRequestDto request)
    {
        var isExists = _users.Any(q => q.EmailAddress == request.EmailAddress);
        if (isExists)
        {
            return false;
        }
        else if (string.IsNullOrWhiteSpace(request.UserName) ||
            string.IsNullOrWhiteSpace(request.Password) ||
            string.IsNullOrWhiteSpace(request.EmailAddress))
        {
            return false;
        }
        return true;
    }
}
