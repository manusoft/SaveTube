namespace SaveTube.Services;

public class YoutubeService : IDisposable
{
    HttpClient httpClient;  

    public async Task<VideoResult> GetVideoInfo(string videoId)
    {     
        using (httpClient = new HttpClient())
        {
            var resourceUri = $"{Constants.ApiServiceUrl}search?part=snippet&q={videoId}&key={Constants.ApiKey}";

            var response = await httpClient.GetAsync(resourceUri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                // Debug.WriteLine(content);

                VideoResult videoInfo = JsonConvert.DeserializeObject<VideoResult>(content);

                Debug.WriteLine("Loading video information completed.");

                return videoInfo;
                //VideoResult myDeserializedClass = JsonSerializer.Deserialize<VideoResult>(content);
            }

            return null;
        }
    }

    public void Dispose()
    {
        httpClient?.Dispose();
    }
}
