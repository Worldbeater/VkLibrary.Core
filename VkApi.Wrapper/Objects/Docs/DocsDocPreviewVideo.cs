using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace VkApi.Wrapper.Objects
{
    public class DocsDocPreviewVideo
    {
        ///<summary>
        /// Video file size in bites
        ///</summary>
        [JsonProperty("filesize")]
        public int Filesize { get; set; }

        ///<summary>
        /// Video's height in pixels
        ///</summary>
        [JsonProperty("height")]
        public int Height { get; set; }

        ///<summary>
        /// Video URL
        ///</summary>
        [JsonProperty("src")]
        public String Src { get; set; }

        ///<summary>
        /// Video's width in pixels
        ///</summary>
        [JsonProperty("width")]
        public int Width { get; set; }
    }
}