using AuthService.Models;

namespace AuthService.TokenResponses
{
    public sealed class AuthResponse
    {
        public string Password { get; set; }
        public RefreshToken LatestRefreshToken { get; set; }
    }
}