using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WindowsPhoneRTSample.Common;
using WindowsPhoneRTSample.DataServices;
using WindowsPhoneRTSample.Models.Request;
using WindowsPhoneRTSample.Models.Response;

namespace WindowsPhoneRTSample.ViewModels
{
    public class MainViewModel : BindableBase
    {

        #region Fields

        private readonly FlickrService _flickrService;
        #endregion

        #region Properties

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        private string _busyMessage;
        public string BusyMessage
        {
            get { return _busyMessage; }
            set { SetProperty(ref _busyMessage, value); }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { SetProperty(ref _errorMessage, value); }
        }


        private readonly ObservableCollection<Photo> _photosCollection = new ObservableCollection<Photo>();
        public ObservableCollection<Photo> PhotosCollection
        {
            get { return _photosCollection; }
        }
        #endregion

        #region Initialization
        public MainViewModel(FlickrService flickrService)
        {
            _flickrService = flickrService;
            LoadInitialPhotosCommand = new ExtendedCommand(LoadInitialPhotos);
        }

        #endregion

        #region Commands
        /// <summary>
        /// Command for loading first page of photos.
        /// </summary>
        public ExtendedCommand LoadInitialPhotosCommand { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// This method will load first page of photos.
        /// </summary>
        /// <returns></returns>
        private async void LoadInitialPhotos()
        {
            //Disable command until executing is finished
            LoadInitialPhotosCommand.CanExecute = false;

            //Show progress to user
            IsBusy = true;
            BusyMessage = "Loading";

            //Clear previous error
            ErrorMessage = null;
            try
            {
                //Set different query paramters
                var extras = new List<string> { RecentPhotosExtras.Geo, RecentPhotosExtras.Description };
                var parameters = new RecentQueryParamters()
                {
                    Extras = extras,
                    Page = 1,
                    PerPage = 40
                };

                var response = await _flickrService.GetRecentPhotosAsync(parameters);
                if (response.ResponseStatus == ResponseStatus.SuccessWithResult && response.Result.Photos != null)
                {
                    PhotosCollection.Clear();
                    var list = response.Result.Photos.List;
                    foreach (var photo in list)
                    {
                        PhotosCollection.Add(photo);
                    }
                }
                else if (response.ResponseStatus == ResponseStatus.NoInternet)
                {
                    ErrorMessage = "No Internet";
                }
                else
                {
                    ErrorMessage = "Some error happened";
                }
            }
            catch (Exception)
            {
                ErrorMessage = "Some error happened";
            }
            finally
            {
                LoadInitialPhotosCommand.CanExecute = true;
                IsBusy = false;
            }

        }
        #endregion

    }
}
