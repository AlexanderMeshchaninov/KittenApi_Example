using System;

namespace WebApiBusinessLayer.Form
{
    public sealed class ClinicMedicalForm
    {
        public Guid ProcedureId { get; set; }
        public DateTimeOffset ApproveDate { get; private set; }
        public bool IsApproved { get; private set; }
        public int KittenId { get; set; }
        public string MedicalProcedure { get; set; }
        public void Approved()
        {
            IsApproved = true;
            ApproveDate = DateTimeOffset.Now;
        }
    }
}