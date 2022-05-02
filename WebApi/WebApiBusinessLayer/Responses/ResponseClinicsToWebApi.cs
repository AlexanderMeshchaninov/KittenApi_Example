using System.Collections.Generic;
using WebApiDataLayer.Models;

namespace WebApiBusinessLayer.Responses
{
    public class ResponseClinicsToWebApiDto
    {
        public List<ClinicDTO> Content { get; set; }
    }

    public class ClinicDTO
    {
        public int Id { get; set; }

        public string ClinicName { get; set; }

        public List<Kitten> Kittens { get; set; }
    }
}