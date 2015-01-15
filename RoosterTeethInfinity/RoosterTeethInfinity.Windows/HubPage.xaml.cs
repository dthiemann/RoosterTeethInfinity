﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MyToolkit.Multimedia;
using RoosterTeethInfinity.Data;
using RoosterTeethInfinity.Common;

using System.Threading.Tasks;

// References to Google API
using Google.Apis.Auth.OAuth2;
using Google.Apis.Upload;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

using MyToolkit;
using MyToolkit.Multimedia;
using Windows.UI.Popups;
using Windows.Web.Syndication;
using System.Net.NetworkInformation;
// The Universal Hub Application project template is documented at http://go.microsoft.com/fwlink/?LinkID=391955

namespace RoosterTeethInfinity
{
    /// <summary>
    /// A page that displays a grouped collection of items.
    /// </summary>
    public sealed partial class HubPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        

        /// <summary>
        /// Gets the NavigationHelper used to aid in navigation and process lifetime management.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the DefaultViewModel. This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        public HubPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.LoadVideos();
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private async void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            // TODO: Create an appropriate data model for your problem domain to replace the sample data
            var sampleDataGroup = await SampleDataSource.GetGroupAsync("Group-4");
            this.DefaultViewModel["Section3Items"] = sampleDataGroup;
        }

        /// <summary>
        /// Invoked when a HubSection header is clicked.
        /// </summary>
        /// <param name="sender">The Hub that contains the HubSection whose header was clicked.</param>
        /// <param name="e">Event data that describes how the click was initiated.</param>
        void Hub_SectionHeaderClick(object sender, HubSectionHeaderClickEventArgs e)
        {
            HubSection section = e.Section;
            var group = section.DataContext;
            this.Frame.Navigate(typeof(SectionPage), ((SampleDataGroup)group).UniqueId);
        }

        /// <summary>
        /// Invoked when an item within a section is clicked.
        /// </summary>
        /// <param name="sender">The GridView or ListView
        /// displaying the item clicked.</param>
        /// <param name="e">Event data that describes the item clicked.</param>
        void ItemView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Navigate to the appropriate destination page, configuring the new page
            // by passing required information as a navigation parameter
            var itemId = ((SampleDataItem)e.ClickedItem).UniqueId;
            this.Frame.Navigate(typeof(ItemPage), itemId);
        }

        private async void LoadVideos()
        {
            pageTitle.Text = "Rooster Teeth Inifinty";
            try 
            { 
                if (NetworkInterface.GetIsNetworkAvailable()) 
                { 
                    //Choose the gap of the videos list 
                    //Index 
                    int index = 1; 
                    //The number of videos you would like to get (from 1 to 50) 
                    int max_results = 6; 
 
                    //To get more than 50 videos, just use pagination and change the index : 
                    //int index = 51; 
 
                    //Channel Videos 
 
                    /* Rooster Teeth */ 
                    string YoutubeChannel = "RoosterTeeth"; 
                    var rt_videos = await YouTubeChannels.GetYoutubeChannel("http://gdata.youtube.com/feeds/base/users/" + YoutubeChannel + "/uploads?alt=rss&v=2&orderby=published&start-index=" + index + "&max-results=" + max_results); 
                    RoosterTeethHubSection.DataContext = rt_videos;
                    RoosterTeethHubSection.Header = "Rooster Teeth"; 

                    /* Achievement Hunter */
                    YoutubeChannel = "LetsPlay";
                    var lp_videos = await YouTubeChannels.GetYoutubeChannel("http://gdata.youtube.com/feeds/base/users/" + YoutubeChannel + "/uploads?alt=rss&v=2&orderby=published&start-index=" + index + "&max-results=" + max_results);
                    LetsPlayHubSection.DataContext = lp_videos;
                    LetsPlayHubSection.Header = "Let's Play";

                    /* The Know */

                    /* Community Hunter */ 
                } 
                else 
                { 
                    MessageDialog message = new MessageDialog("You're not connected to Internet!"); 
                    await message.ShowAsync(); 
                } 
            } 
            catch {  } 
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="Common.NavigationHelper.LoadState"/>
        /// and <see cref="Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </summary>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
    }
}
