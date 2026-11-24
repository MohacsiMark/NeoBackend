namespace NeotonFamiliaAPI.DTOs;

public class AlbumDTO
{
    public string Cim { get; set; } = "";
    public int Ev { get; set; }
}

public class AlbumValaszDTO
{
    public int Id { get; set; }
    public string Cim { get; set; } = "";
    public int Ev { get; set; }
    public int? MegtekintesekSzama { get; set; }
}
