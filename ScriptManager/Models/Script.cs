using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptManager.Models
{
    class Script
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("forumUrl")]
        public string ForumUrl { get; set; }
        [JsonProperty("websiteUrl")]
        public string WebsiteUrl { get; set; }
        [JsonProperty("isPaid")]
        public bool IsPaid { get; set; }
        [JsonProperty("isWorking")]
        public bool IsWorking { get; set; }
        [JsonProperty("user")]
        public string Author { get; set; }
        [JsonProperty("updateUrl")]
        public string UpdateUrl { get; set; }
        [JsonProperty("createdAt")]
        public string CreatedAt { get; set; }

        public Script()
        {

        }

        public Script(int id, string title, string type, string forumurl, string websiteUrl, bool isPaid, bool isWorking, string author, string updateUrl, string createdAt)
        {
            this.Id = Id;
            this.Title = title;
            this.Type = type;
            this.ForumUrl = forumurl;
            this.WebsiteUrl = websiteUrl;
            this.IsPaid = isPaid;
            this.IsWorking = isWorking;
            this.Author = author;
            this.UpdateUrl = updateUrl;
            this.CreatedAt = createdAt;
        }
    }
}
