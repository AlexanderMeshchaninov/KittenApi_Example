namespace WebApiFluentValidation.Interfaces
{
    public interface IClinicServiceRequestValidation
    {
        public int KittenId { get; set; }
        public string MedicalProcedure { get; set; }
    }
}