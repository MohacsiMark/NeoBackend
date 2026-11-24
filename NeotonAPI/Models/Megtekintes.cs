using System.Text.Json.Serialization;

namespace NeotonFamiliaAPI.Models;

public class Megtekintes
{
    public int Id { get; set; }
    public int AlbumId { get; set; }
    public int? Darab { get; set; }

    [JsonIgnore]
    public Album? Album { get; set; }
}
