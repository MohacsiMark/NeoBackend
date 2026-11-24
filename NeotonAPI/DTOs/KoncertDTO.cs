namespace NeotonFamiliaAPI.DTOs;

public class KoncertDTO
{
    public DateTime Datum { get; set; }
    public string Helyszin { get; set; } = "";
}

public class KoncertValaszDTO
{
    public int Id { get; set; }
    public DateTime Datum { get; set; }
    public string Helyszin { get; set; } = "";
}
