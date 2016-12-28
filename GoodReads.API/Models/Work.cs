using Newtonsoft.Json;
using System;

namespace GoodReads.API.Models
{
    [JsonObject]
    [Serializable]
    public class Work
    {
        public string Id { get; set; }
        public string NumberOfBooks { get; set; }
        public string OriginalPublicationDate { get; set; }
        public string NumberOfRatings { get; set; }
        public string NumberOfTextReviews { get; set; }
        public string AverageRating { get; set; }
        public Book Book { get; set; }
    }
}