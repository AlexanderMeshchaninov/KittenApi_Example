﻿using WebApiAbstraction.Interfaces;

namespace WebApi.Requests
{
    public sealed class ClinicRequestDto : IClinicRequestDto
    {
        public int Id { get; set; }
        public int ClinicId { get; set; }
        public int KittenId { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public string ClinicName { get; set; }
    }
}