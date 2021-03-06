using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace VkApi.Wrapper.Objects
{
    public class StatsCountry
    {
        ///<summary>
        /// Country code
        ///</summary>
        [JsonProperty("code")]
        public String Code { get; set; }

        ///<summary>
        /// Visitors number
        ///</summary>
        [JsonProperty("count")]
        public int Count { get; set; }

        ///<summary>
        /// Country name
        ///</summary>
        [JsonProperty("name")]
        public String Name { get; set; }

        ///<summary>
        /// Country ID
        ///</summary>
        [JsonProperty("value")]
        public int Value { get; set; }
    }
}