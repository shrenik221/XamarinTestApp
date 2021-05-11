using Plugin.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.ViewModels;
using Xamarin.Forms;

namespace TestApp.Views
{
    public partial class Registration : ContentPage
    {
        static byte[] imageArray;
        public Registration()
        {
            InitializeComponent();
            
        }

        private async void SaveData(object sender, EventArgs e)
        {
            var name = UsernameEntry.Text;
            var gender = picker.SelectedItem.ToString();
            await RegistrationViewModel.AddUser(name, gender, imageArray);
            await Navigation.PushAsync(new Display());
        }

        private async void TapTakeImage(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (file == null)
                return;

            await DisplayAlert("File Location", file.Path, "OK");

            RetrievedImage.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });

            GetImageBytes(file.GetStream());
        }

        private async void TapUploadImage(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                DisplayAlert("No gallery suport", ":( No gallery support availaible.", "OK");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 800,
                SaveMetaData = false
            });

            if (file == null)
                return;

            await DisplayAlert("File Location", file.Path, "OK");

            RetrievedImage.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });

            GetImageBytes(file.GetStream());
        }

        private void GetImageBytes(System.IO.Stream stream)
        {
            using (var memoryStream = new System.IO.MemoryStream())
            {
                stream.CopyTo(memoryStream);
                imageArray = memoryStream.ToArray();
            }
        }
    }
}
