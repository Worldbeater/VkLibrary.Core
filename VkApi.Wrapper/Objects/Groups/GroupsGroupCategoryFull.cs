using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace VkApi.Wrapper.Objects
{
    public class GroupsGroupCategoryFull
    {
        ///<summary>
        /// Category ID
        ///</summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        ///<summary>
        /// Category name
        ///</summary>
        [JsonProperty("name")]
        public String Name { get; set; }

        ///<summary>
        /// Pages number
        ///</summary>
        [JsonProperty("page_count")]
        public int PageCount { get; set; }
        [JsonProperty("page_previews")]
        public GroupsGroup[] PagePreviews { get; set; }
        [JsonProperty("subcategories")]
        public GroupsGroupCategory[] Subcategories { get; set; }
    }
}