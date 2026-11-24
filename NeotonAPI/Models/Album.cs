using System.Text.Json.Serialization;

namespace NeotonFamiliaAPI.Models;

public class Album
{
    public int Id { get; set; }
    public string Cim { get; set; } = "";
    public int Ev { get; set; }

    [JsonIgnore]
    public Megtekintes? Megtekintes { get; set; }
}
