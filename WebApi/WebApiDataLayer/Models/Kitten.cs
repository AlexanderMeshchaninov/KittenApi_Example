using System;
using System.Collections.Generic;

namespace WebApiDataLayer.Models
{
    public sealed class Kitten
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public int Weight { get; set; }
        public string Color { get; set; }
        public string FeedName { get; set; }
        public bool HasCertificate { get; set; }
        public bool HasMedicalInspection { get; set; }
        public DateTimeOffset LastInspection { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<Clinic> Clinics { get; set; } = new List<Clinic>();
    }
}