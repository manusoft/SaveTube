namespace SaveTube.Models;

// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);


public class VideoResult
{
    [JsonPropertyName("items")]
    public List<Item> Items { get; set; }
}

public class Item
{
    [JsonPropertyName("id")]
    public Id Id { get; set; }

    [JsonPropertyName("snippet")]
    public Snippet Snippet { get; set; }
}

public class Id
{
    [JsonPropertyName("videoId")]
    public string VideoId { get; set; }
}

public class Snippet
{
    [JsonPropertyName("publishedAt")]
    public DateTime PublishedAt { get; set; }

    [JsonPropertyName("channelId")]
    public string ChannelId { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("thumbnails")]
    public Thumbnails Thumbnails { get; set; }

    [JsonPropertyName("channelTitle")]
    public string ChannelTitle { get; set; }

    [JsonPropertyName("publishTime")]
    public DateTime PublishTime { get; set; }
}

public class Thumbnails
{
    [JsonPropertyName("medium")]
    public Thumbnail Medium { get; set; }

    [JsonPropertyName("high")]
    public Thumbnail High { get; set; }
}

public class Thumbnail
{
    [JsonPropertyName("url")]
    public string Url { get; set; }
}







