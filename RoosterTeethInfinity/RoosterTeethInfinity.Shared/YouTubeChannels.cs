using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

using MyToolkit;
using MyToolkit.MyToolkit_Extended_WinRT_XamlTypeInfo;
using MyToolkit.Multimedia;

using Windows.Web.Syndication;
using System.Xml;
using System.Net.Http;
using System.IO;

namespace RoosterTeethInfinity
{
    internal class YouTubeChannels
    {
        /* Loads up the given YouTube Channel */
        public static async Task<List<YouTubeVideo>> GetYoutubeChannel(string url) {
            try {
                SyndicationClient client = new SyndicationClient();
                SyndicationFeed feed = await client.RetrieveFeedAsync(new Uri(url));

                List<YouTubeVideo> videosList = new List<YouTubeVideo>();
                YouTubeVideo video;
                foreach (SyndicationItem item in feed.Items) {
                    video = new YouTubeVideo();

                    video.YouTubeLink = item.Links[0].Uri;
                    string a = video.YouTubeLink.ToString().Remove(0, 31);
                    video.Id = a.Substring(0, 11);
                    video.Title = item.Title.Text;
                    video.PubDate = item.PublishedDate.DateTime;

                    video.Thumbnail = YouTube.GetThumbnailUri(video.Id, YouTubeThumbnailSize.Large);

                    videosList.Add(video);
                }

                return videosList;
            }
            catch {
                return null;
            }
        }
        public async Task<List<YouTubeVideo>> GetYoutubeChannelXML(string url) {
            try {
                HttpClient client = new HttpClient();
                var feedXML = await client.GetStringAsync(new Uri(url));
                StringReader stringReader = new StringReader(feedXML);

                XmlReader xmlReader = XmlReader.Create(stringReader);
                SyndicationFeed feed = SyndicationFeed.Load(xmlReader);
                List<YouTubeVideo> videosList = new List<YouTubeVideo>();
                YouTubeVideo video;
                foreach (SyndicationItem item in feed.Items) {
                    video = new YouTubeVideo();

                    video.YouTubeLink = item.Links[0].Uri;
                    string a = video.YouTubeLink.ToString().Remove(0, 31);
                    video.Id = a.Substring(0, 11);
                    video.Title = item.Title.Text;
                    video.PubDate = item.PublishedDate.DateTime;
                    item.

                    video.Thumbnail = YouTube.GetThumbnailUri(video.Id, YouTubeThumbnailSize.Small);

                    videosList.Add(video);
                }

                return videosList;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return null;
            }
        } 
    }
}
