using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsPhoneRTSample.DataServices;

namespace WindowsPhoneRTSample.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            //You should use dependency injection to resolve viewmodels and other classes
            //Example:Autofac library
        }

        public MainViewModel MainViewModel
        {
            get { return new MainViewModel(new FlickrService()); }
        }
    }
}
