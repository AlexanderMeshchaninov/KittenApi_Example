namespace WebApiFluentValidation.Interfaces
{
    public interface IOperationFailure
    {
        string PropertyName { get; set; }
        string Description { get; set; }
        string Code { get; set; }
    }
}