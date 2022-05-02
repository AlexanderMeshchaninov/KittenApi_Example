namespace AuthApiFluentValidationAbstraction.Interfaces
{
    public interface IUserRequestValidation
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}