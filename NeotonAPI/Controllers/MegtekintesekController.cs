using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeotonFamiliaAPI.DTOs;
using NeotonFamiliaAPI.Models;

namespace NeotonFamiliaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MegtekintesekController : ControllerBase
{
    private readonly NeotonFamiliaContext _context;

    public MegtekintesekController(NeotonFamiliaContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var eredmeny = await _context.Megtekintesek
                .Include(x => x.Album)
                .OrderBy(x => x.AlbumId)
                .Select(x => new MegtekintesValaszDTO
                {
                    Id = x.Id, AlbumId = x.AlbumId, AlbumCim = x.Album!.Cim, Darab = x.Darab
                })
                .ToListAsync();
            return Ok(new { message = "Megtekintések betöltve.", result = eredmeny });
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "Hiba történt a megtekintések lekérésekor.", result = (object?)null });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var adat = await _context.Megtekintesek
                .Include(x => x.Album)
                .Where(x => x.Id == id)
                .Select(x => new MegtekintesValaszDTO
                {
                    Id = x.Id, AlbumId = x.AlbumId, AlbumCim = x.Album!.Cim, Darab = x.Darab
                })
                .FirstOrDefaultAsync();
            if (adat == null) return NotFound(new { message = "A megtekintés nem található.", result = (object?)null });
            return Ok(new { message = "Megtekintés betöltve.", result = adat });
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "Hiba történt a megtekintés lekérésekor.", result = (object?)null });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(MegtekintesDTO adat)
    {
        try
        {
            var album = await _context.Albumok.FindAsync(adat.AlbumId);
            if (album == null) return BadRequest(new { message = "A megadott album nem létezik.", result = (object?)null });
            var letezik = await _context.Megtekintesek.AnyAsync(x => x.AlbumId == adat.AlbumId);
            if (letezik) return BadRequest(new { message = "Ehhez az albumhoz már tartozik megtekintésszám.", result = (object?)null });
            if (adat.Darab < 0) return BadRequest(new { message = "A megtekintésszám nem lehet negatív.", result = (object?)null });
            var megtekintes = new Megtekintes { AlbumId = adat.AlbumId, Darab = adat.Darab };
            _context.Megtekintesek.Add(megtekintes);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Megtekintésszám hozzáadva.", result = megtekintes.Id });
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "Hiba történt a megtekintésszám mentésekor.", result = (object?)null });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, MegtekintesDTO adat)
    {
        try
        {
            var megtekintes = await _context.Megtekintesek.FindAsync(id);
            if (megtekintes == null) return NotFound(new { message = "A megtekintés nem található.", result = (object?)null });
            var album = await _context.Albumok.FindAsync(adat.AlbumId);
            if (album == null) return BadRequest(new { message = "A megadott album nem létezik.", result = (object?)null });
            if (adat.Darab < 0) return BadRequest(new { message = "A megtekintésszám nem lehet negatív.", result = (object?)null });
            var masik = await _context.Megtekintesek.AnyAsync(x => x.AlbumId == adat.AlbumId && x.Id != id);
            if (masik) return BadRequest(new { message = "Ehhez az albumhoz már tartozik megtekintésszám.", result = (object?)null });
            megtekintes.AlbumId = adat.AlbumId;
            megtekintes.Darab = adat.Darab;
            await _context.SaveChangesAsync();
            return Ok(new { message = "Megtekintésszám módosítva.", result = megtekintes.Id });
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "Hiba történt a megtekintésszám módosításakor.", result = (object?)null });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var megtekintes = await _context.Megtekintesek.FindAsync(id);
            if (megtekintes == null) return NotFound(new { message = "A megtekintés nem található.", result = (object?)null });
            _context.Megtekintesek.Remove(megtekintes);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Megtekintésszám törölve.", result = id });
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "Hiba történt a megtekintésszám törlésekor.", result = (object?)null });
        }
    }
}
