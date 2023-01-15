using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using SaveTube.Models;
using SaveTube.Services;
using System.IO;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace SaveTube.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    YoutubeService youtubeService;

    string[] videoQuality = { "1080p", "720p", "480p", "360p", "240p", "144p" };

    private IEnumerable<MuxedStreamInfo> streamInfo;
    public List<string> VideoQualityList { get; set; } = new List<string>();

    public ObservableCollection<Download> DownloadList { get; set; } = new ObservableCollection<Download>();

    [ObservableProperty]
    double progressChanged;

    [ObservableProperty]
    string selectedQuality = "720p";

    [ObservableProperty]
    Download selectedItem;

    [ObservableProperty]
    string youtubeUrl;

    [ObservableProperty]
    string videoTitle;

    [ObservableProperty]
    ImageSource thumbnailUrl;

    public MainViewModel()
    {
        youtubeService = App.Current.Services.GetService<YoutubeService>();
        VideoQualityList.AddRange(videoQuality);
    }

    [RelayCommand]
    async void GetInfo()
    {
        if (string.IsNullOrEmpty(YoutubeUrl) || YoutubeUrl.Length == 0) { return; }

        IsAnalyze = true;

        try
        {
            var youtube = new YoutubeClient();
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync($"{YoutubeUrl}");
            // Get highest quality muxed stream
            streamInfo = streamManifest.GetMuxedStreams();

            var videoPlayerStream = streamInfo.First(video => video.VideoQuality.Label == $"{SelectedQuality}"); // $"{Quality}");// is "1080p" or "720p" or "480p" or "360p");

            Debug.WriteLine(videoPlayerStream.Url);
            Debug.WriteLine(videoPlayerStream.VideoQuality);

            // Get video information
            var result = await youtubeService.GetVideoInfo(YoutubeUrl);

            Download download = new Download();

            if (result != null)
            {
                foreach (var item in result.Items)
                {
                    var exist = DownloadList.Contains(DownloadList.FirstOrDefault(list => list.Id == item.Id.VideoId));

                    if (!exist)
                    {
                        download.Id = item.Id.VideoId;
                        download.Name = item.Snippet.Title;
                        download.AudioCodec = videoPlayerStream.AudioCodec;
                        download.Bitrate = videoPlayerStream.Bitrate.ToString();
                        download.VideoCodec = videoPlayerStream.VideoCodec;
                        download.Quality = videoPlayerStream.VideoQuality.ToString();
                        download.Resolution = videoPlayerStream.VideoResolution.ToString();
                        download.Size = videoPlayerStream.Size.ToString();
                        download.Image = new BitmapImage(new Uri(item.Snippet.Thumbnails.Medium.Url));
                        download.YoutubeUrl = YoutubeUrl;
                        download.DownloadUrl = videoPlayerStream.Url;
                        download.Progress = 0;
                        download.IsCompleted = false;
                        download.CreatedAt = DateTime.Now;

                        DownloadList.Add(download);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            IsAnalyze = false;
            YoutubeUrl = null;
            DownloadAsync();
        }
    }

    [RelayCommand]
    async void DownloadAsync()
    {
        if ((DownloadList.Count == 0) || (IsBusy)) return;

        IsBusy= true;

		foreach (var item in DownloadList)
		{
			if (!item.IsCompleted)
			{
                var downloadFileUrl = item.DownloadUrl;
                var destinationFilePath = Path.GetFullPath($"d:/{item.Id}.mp4");

                using (var client = new DownloadService(downloadFileUrl, destinationFilePath))
                {
                    client.ProgressChanged += (totalFileSize, totalBytesDownloaded, progressPercentage) => {
                        Debug.WriteLine($"{progressPercentage}% ({totalBytesDownloaded}/{totalFileSize})");
                        ProgressChanged = (double)progressPercentage;

                        var found = DownloadList.FirstOrDefault(x => x.Id == item.Id);
                        //int i = DownloadList.IndexOf(found);                        
                        found.Progress = progressChanged;
                        //DownloadList[i] = found;
                    };

                    await client.StartDownload();
                }
            }
		}
	}

    public void DataGridSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
		SelectedItem = e.AddedItems[0] as Download;

		if(SelectedItem!= null)
		{
			VideoTitle = SelectedItem.Name;
			ThumbnailUrl = SelectedItem.Image;
        }
    }
}
