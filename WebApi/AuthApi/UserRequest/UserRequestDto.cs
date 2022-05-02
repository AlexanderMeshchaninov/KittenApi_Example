using AuthApiAbstraction.Interfaces;

namespace AuthApi.UserRequest
{
    public sealed class UserRequestDto : IUserRequestDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}