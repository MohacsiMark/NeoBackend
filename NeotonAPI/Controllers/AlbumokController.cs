using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeotonFamiliaAPI.DTOs;
using NeotonFamiliaAPI.Models;

namespace NeotonFamiliaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlbumokController : ControllerBase
{
    private readonly NeotonFamiliaContext _context;

    public AlbumokController(NeotonFamiliaContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var eredmeny = await _context.Albumok
                .OrderBy(x => x.Ev)
                .Select(x => new AlbumValaszDTO
                {
                    Id = x.Id,
                    Cim = x.Cim,
                    Ev = x.Ev,
                    MegtekintesekSzama = x.Megtekintes == null ? null : x.Megtekintes.Darab
                })
                .ToListAsync();
            return Ok(new { message = "Albumok betöltve.", result = eredmeny });
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "Hiba történt az albumok lekérésekor.", result = (object?)null });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var album = await _context.Albumok
                .Where(x => x.Id == id)
                .Select(x => new AlbumValaszDTO
                {
                    Id = x.Id, Cim = x.Cim, Ev = x.Ev,
                    MegtekintesekSzama = x.Megtekintes == null ? null : x.Megtekintes.Darab
                })
                .FirstOrDefaultAsync();
            if (album == null) return NotFound(new { message = "Az album nem található.", result = (object?)null });
            return Ok(new { message = "Album betöltve.", result = album });
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "Hiba történt az album lekérésekor.", result = (object?)null });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(AlbumDTO adat)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(adat.Cim)) return BadRequest(new { message = "Az album címe kötelező.", result = (object?)null });
            var album = new Album { Cim = adat.Cim, Ev = adat.Ev };
            _context.Albumok.Add(album);
            await _context.SaveChangesAsync();
            _context.Megtekintesek.Add(new Megtekintes { AlbumId = album.Id, Darab = null });
            await _context.SaveChangesAsync();
            return Ok(new { message = "Album hozzáadva.", result = album.Id });
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "Hiba történt az album mentésekor.", result = (object?)null });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, AlbumDTO adat)
    {
        try
        {
            var album = await _context.Albumok.FindAsync(id);
            if (album == null) return NotFound(new { message = "Az album nem található.", result = (object?)null });
            album.Cim = adat.Cim;
            album.Ev = adat.Ev;
            await _context.SaveChangesAsync();
            return Ok(new { message = "Album módosítva.", result = album.Id });
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "Hiba történt az album módosításakor.", result = (object?)null });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var album = await _context.Albumok.FindAsync(id);
            if (album == null) return NotFound(new { message = "Az album nem található.", result = (object?)null });
            _context.Albumok.Remove(album);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Album törölve.", result = id });
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "Hiba történt az album törlésekor.", result = (object?)null });
        }
    }
}
