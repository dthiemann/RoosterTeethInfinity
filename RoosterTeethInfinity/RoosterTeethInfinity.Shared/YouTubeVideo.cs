using System;
using System.Collections.Generic;
using System.Text;

namespace RoosterTeethInfinity
{
    public class YouTubeVideo
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public DateTime PubDate { get; set; }
        public Uri YouTubeLink { get; set; }
        public Uri VideoLink { get; set; }
        public Uri Thumbnail { get; set; }
    }
}
