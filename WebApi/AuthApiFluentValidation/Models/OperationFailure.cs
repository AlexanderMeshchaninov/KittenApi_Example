using AuthApiFluentValidationAbstraction.Interfaces;

namespace AuthApiFluentValidation.Models
{
    public sealed class OperationFailure : IOperationFailure
    {
        public string PropertyName { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public OperationFailure(
            string propertyName, 
            string description, 
            string code)
        {
            PropertyName = propertyName;
            Description = description;
            Code = code;
        }
    }
}