namespace WebApiAuthService
{
    public interface IAuthService
    {
    }

    public sealed class WebApiAuthService : IAuthService
    {
        public const string SecretCode = "THIS IS SOME VERY SECRET STRING!!! Im blue da ba dee da ba " +
                                         "di da ba dee da ba di da d ba dee da ba di da ba dee";
    }
}