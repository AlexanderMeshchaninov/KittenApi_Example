using WebApiFluentValidation.Interfaces;

namespace WebApiFluentValidation.Models
{
    public class OperationFailure : IOperationFailure
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