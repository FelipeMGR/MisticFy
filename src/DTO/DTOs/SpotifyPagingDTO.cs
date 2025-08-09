namespace MisticFy.src.DTO.DTO;

public class SpotifyPagingDTO<T>
{
    public string Href { get; set; }
    public List<T> Items { get; set; }
    public int Limit { get; set; }
    public string Next { get; set; }
    public int Offset { get; set; }
    public string Previous { get; set; }
    public int Total { get; set; }
}
