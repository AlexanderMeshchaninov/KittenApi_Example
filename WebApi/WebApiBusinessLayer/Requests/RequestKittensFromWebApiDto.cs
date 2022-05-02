﻿using WebApiBusinessLayerAbstraction.Requests;

namespace WebApiBusinessLayer.Requests
{
    public class RequestKittensFromWebApiDto :IRequestKittensFromWebApiDto
    {
        public int Id { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public string NickName { get; set; }
        public int Weight { get; set; }
        public string Color { get; set; }
        public string FeedName { get; set; }
        public bool HasCertificate { get; set; }
    }
}