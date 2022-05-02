using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace WebApiDataLayer.Models
{
    public sealed class Clinic
    {
        public int Id { get; set; }
        [AllowNull]
        public int KittenId { get; set; }
        public string ClinicName { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<Kitten> Kittens { get; set; } = new List<Kitten>();
    }
}