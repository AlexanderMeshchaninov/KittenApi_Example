using WebApiFluentValidation.Interfaces;

namespace WebApiFluentValidation.Models
{
    public sealed class ClinicServiceRequestValidation : IClinicServiceRequestValidation
    {
        public int KittenId { get; set; }
        public string MedicalProcedure { get; set; }
    }
}