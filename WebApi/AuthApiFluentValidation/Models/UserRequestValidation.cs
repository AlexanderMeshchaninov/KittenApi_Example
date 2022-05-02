using AuthApiFluentValidationAbstraction.Interfaces;

namespace AuthApiFluentValidation.Models
{
    public sealed class UserRequestValidation : IUserRequestValidation
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}