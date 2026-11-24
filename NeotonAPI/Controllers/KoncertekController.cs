using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeotonFamiliaAPI.DTOs;
using NeotonFamiliaAPI.Models;

namespace NeotonFamiliaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KoncertekController : ControllerBase
{
    private readonly NeotonFamiliaContext _context;

    public KoncertekController(NeotonFamiliaContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var eredmeny = await _context.Koncertek
                .OrderBy(x => x.Datum)
                .Select(x => new KoncertValaszDTO { Id = x.Id, Datum = x.Datum, Helyszin = x.Helyszin })
                .ToListAsync();
            return Ok(new { message = "Koncertek betöltve.", result = eredmeny });
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "Hiba történt a koncertek lekérésekor.", result = (object?)null });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var koncert = await _context.Koncertek.FindAsync(id);
            if (koncert == null) return NotFound(new { message = "A koncert nem található.", result = (object?)null });
            return Ok(new { message = "Koncert betöltve.", result = new KoncertValaszDTO { Id = koncert.Id, Datum = koncert.Datum, Helyszin = koncert.Helyszin } });
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "Hiba történt a koncert lekérésekor.", result = (object?)null });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(KoncertDTO adat)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(adat.Helyszin)) return BadRequest(new { message = "A helyszín megadása kötelező.", result = (object?)null });
            var koncert = new Koncert { Datum = adat.Datum.Date, Helyszin = adat.Helyszin };
            _context.Koncertek.Add(koncert);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Koncert hozzáadva.", result = koncert.Id });
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "Hiba történt a koncert mentésekor.", result = (object?)null });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, KoncertDTO adat)
    {
        try
        {
            var koncert = await _context.Koncertek.FindAsync(id);
            if (koncert == null) return NotFound(new { message = "A koncert nem található.", result = (object?)null });
            koncert.Datum = adat.Datum.Date;
            koncert.Helyszin = adat.Helyszin;
            await _context.SaveChangesAsync();
            return Ok(new { message = "Koncert módosítva.", result = koncert.Id });
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "Hiba történt a koncert módosításakor.", result = (object?)null });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var koncert = await _context.Koncertek.FindAsync(id);
            if (koncert == null) return NotFound(new { message = "A koncert nem található.", result = (object?)null });
            _context.Koncertek.Remove(koncert);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Koncert törölve.", result = id });
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "Hiba történt a koncert törlésekor.", result = (object?)null });
        }
    }
}
