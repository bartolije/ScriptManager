using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptManager.Models
{
    class Champion
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("realName")]
        public string Key { get; set; }

        public Champion()
        {

        }

        public Champion(int id, string name, string key)
        {
            this.Id = Id;
            this.Name = name;
            this.Key = key;
        }
    }
}
