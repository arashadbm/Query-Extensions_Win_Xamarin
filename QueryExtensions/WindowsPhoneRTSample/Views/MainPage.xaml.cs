using System;
using System.Collections.Generic;
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
using WindowsPhoneRTSample.ViewModels;

namespace WindowsPhoneRTSample
{

    public sealed partial class MainPage : Page
    {
        private readonly MainViewModel _mainViewModel;
        public MainPage()
        {
            this.InitializeComponent();
            _mainViewModel = (MainViewModel)DataContext;
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _mainViewModel.LoadInitialPhotosCommand.Execute(null);
        }

        private void WrapGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!(e.NewSize.Width > 0)) return;
            ItemsWrapGrid itemsWrapGrid = sender as ItemsWrapGrid;
            if (itemsWrapGrid == null) return;
            var width = (e.NewSize.Width);
            itemsWrapGrid.ItemWidth = width / 2;
            itemsWrapGrid.ItemHeight = itemsWrapGrid.ItemWidth;
        }
    }
}
