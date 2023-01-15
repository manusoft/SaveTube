using Microsoft.UI.Xaml.Media;

namespace SaveTube.Models;

public class Download : ObservableObject
{
    public string Id { get; set; }
    public string Name { get; set; }      
    public string AudioCodec { get; set; }
    public string Bitrate { get; set; }
    public string VideoCodec { get; set; }
    public string Quality { get; set; }
    public string Resolution { get; set; }
    public string Size { get; set; }
    public string YoutubeUrl { get; set; }
    public string DownloadUrl { get; set; }
    public ImageSource Image { get; set; }
    public double Progress { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }

}
