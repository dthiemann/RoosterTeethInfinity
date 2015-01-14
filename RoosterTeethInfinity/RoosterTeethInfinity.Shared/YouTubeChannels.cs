﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace RoosterTeethInfinity
{
    class YouTubeChannels
    {
        public List<string> Channels;
        public List<string> ChannelTitles;
            
        [STAThread]
        private async Task Search(string criteria) {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer() {
                // iOS ApiKey because I can't find how to do it with Window's Phones
                ApiKey = "AIzaSyAj45pQlT6IRJlTmt8ykUUbZPhONmnwvgo",
                ApplicationName = this.GetType().ToString()
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = criteria; // This is the search term
            searchListRequest.MaxResults = 50;
            //Call the search.list method to retrieve results matching specified query term
            var searchListResponse = await searchListRequest.ExecuteAsync();

            Channels = new List<string>();
            ChannelTitles = new List<string>();

            // Add each result to the appropriate list and then display the list of
            // matching videos, channels, and playlists
            foreach (var searchResult in searchListResponse.Items) {
                switch (searchResult.Id.Kind) {
                    case "youtube#channel":
                        Channels.Add(searchResult.Id.ChannelId);
                        ChannelTitles.Add(searchResult.Snippet.Title);
                        //channels.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.ChannelId));
                        break;
                }
            }
        }

        public string ExecuteSearch(string searchCriteria)
        {
            Search(searchCriteria).Wait();
            Debug.WriteLine("We have completed");
            string result = Channels[0] + " " + ChannelTitles[0];
            return result;
        }
    }
}
