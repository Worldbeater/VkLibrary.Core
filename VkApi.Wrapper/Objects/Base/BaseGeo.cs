using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace VkApi.Wrapper.Objects
{
    public class BaseGeo
    {
        [JsonProperty("coordinates")]
        public BaseGeoCoordinates Coordinates { get; set; }
        [JsonProperty("place")]
        public BasePlace Place { get; set; }

        ///<summary>
        /// Information whether a map is showed
        ///</summary>
        [JsonProperty("showmap")]
        public int Showmap { get; set; }

        ///<summary>
        /// Place type
        ///</summary>
        [JsonProperty("type")]
        public String Type { get; set; }
    }
}