namespace NeotonFamiliaAPI.DTOs;

public class MegtekintesDTO
{
    public int AlbumId { get; set; }
    public int? Darab { get; set; }
}

public class MegtekintesValaszDTO
{
    public int Id { get; set; }
    public int AlbumId { get; set; }
    public string AlbumCim { get; set; } = "";
    public int? Darab { get; set; }
}
