using Domains.DTos;
using Domains.Entities;
using Domains.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Interfaces
{
    public interface IUser
    {
        AddUserResponseDto CreateUser(AddUserRequestDto request);
        AddUserResponseDto Login(string emailAddress, string password);
    }
}
