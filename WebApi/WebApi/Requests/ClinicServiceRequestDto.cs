using WebApiAbstraction.Interfaces;

namespace WebApi.Requests
{
    public sealed class ClinicServiceRequestDto : IClinicServiceRequestDto
    {
        public int KittenId { get; set; }
        public string MedicalProcedure { get; set; }
    }
}