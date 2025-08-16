using Travelnsight.App.Dto;
using Travelnsight.App.Interfaces;

namespace Travelnsight.App
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void MyCamera_MediaCaptured(object sender, CommunityToolkit.Maui.Core.MediaCapturedEventArgs e)
        {
            if (Dispatcher.IsDispatchRequired)
            {
                Dispatcher.Dispatch(async () =>
                {
                    MyImage.Source = ImageSource.FromStream(() => e.Media);
                    var ms = new MemoryStream();
                    await e.Media.CopyToAsync(ms);

                    var result = await IPlatformApplication.Current!.Services.GetRequiredService<ITravelnsightService>()
                    .Analyze(new VisionRequestDto
                    {
                        Image = ms.ToArray()
                    });

                    LLMResult.Text = result.Response;

                });
                return;
            }

            MyImage.Source = ImageSource.FromStream(() => e.Media);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await MyCamera.CaptureImage(CancellationToken.None);
        }
    }
}
