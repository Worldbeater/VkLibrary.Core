using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace VkApi.Wrapper.Objects
{
    public class NewsfeedItemVideo : NewsfeedItemBase
    {
        [JsonProperty("video")]
        public NewsfeedItemVideoVideo Video { get; set; }
    }
}