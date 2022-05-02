namespace WebApiBusinessLayerAbstraction.Requests
{
    public interface IRequestClinicServiceFromWebApiDto
    {
        public int KittenId { get; set; }
        public string MedicalProcedure { get; set; }
    }
}