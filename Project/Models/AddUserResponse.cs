using Domains.Enums;
using System.Reflection.Metadata.Ecma335;

namespace Project.Models
{
    public class AddUserResponse
    {
        public OpStatus Status { get; set; }
        public string Message { get; set; }
    }
}
