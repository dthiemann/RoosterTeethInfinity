using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
using RoosterTeethInfinity;

// The Settings Flyout item template is documented at http://go.microsoft.com/fwlink/?LinkId=273769

namespace RoosterTeethInfinity
{
    public sealed partial class SettingsFlyout1 : SettingsFlyout
    {
        public SettingsFlyout1()
        {
            this.InitializeComponent();
        }

        private void hideLP_toggled(object sender, RoutedEventArgs e) {
            
        }

        private void hideRT_toggled(object sender, RoutedEventArgs e) {

        }

    }
}
