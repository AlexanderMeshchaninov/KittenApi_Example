namespace AuthServiceAbstraction.AuthServiceInterfaces
{
    public interface IAuthGenerateJwtTokenInterface
    {
        public string GenerateJwtToken(int id, int minutes);
    }
}