using WebApiBusinessLayerAbstraction.Requests;

namespace WebApiBusinessLayer.Requests
{
    public sealed class RequestClinicServiceFromWebApiDto : IRequestClinicServiceFromWebApiDto
    {
        public int KittenId { get; set; }
        public string MedicalProcedure { get; set; }
    }
}