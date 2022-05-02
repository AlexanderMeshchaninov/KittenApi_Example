namespace WebApiAbstraction.Interfaces
{
    public interface IClinicServiceRequestDto
    {
        public int KittenId { get; set; }
        public string MedicalProcedure { get; set; }
    }
}