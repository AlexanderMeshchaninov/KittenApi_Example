using System.Collections.Generic;
using WebApiDataLayer.Models;

namespace WebApiBusinessLayer.Responses
{
    public class ResponseKittensToWebApiDTO
    {
        public List<KittenDTO> Content { get; set; }
    }

    public class KittenDTO
    {
        public int Id { get; set; }

        public string NickName { get; set; }

        public int Weigth { get; set; }

        public string Color { get; set; }

        public string FeedName { get; set; }

        public bool HasCertificate { get; set; }
    }
}