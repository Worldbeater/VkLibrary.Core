using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace VkApi.Wrapper.Objects
{
    public class MessageChatPreview
    {
        [JsonProperty("admin_id")]
        public int AdminId { get; set; }
        [JsonProperty("joined")]
        public Boolean Joined { get; set; }
        [JsonProperty("local_id")]
        public int LocalId { get; set; }
        [JsonProperty("members")]
        public int[] Members { get; set; }
        [JsonProperty("members_count")]
        public int MembersCount { get; set; }
        [JsonProperty("title")]
        public String Title { get; set; }
    }
}