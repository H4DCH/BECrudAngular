using BE_CRUDNET.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BE_CRUDNET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MascotaController : ControllerBase
    {
        private readonly Context _context;
        public MascotaController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> obtenerNOmbres()
        {
            try
            {
                var listaMascotas = await _context.Mascotas.ToListAsync();
                return Ok(listaMascotas);

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int Id) {
            try
            {
                var mascota = await _context.Mascotas.FindAsync(Id);
                if (mascota == null)
                {
                    return NotFound();

                }
                return Ok(mascota);

            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var mascota = await _context.Mascotas.FindAsync(Id);
                if (mascota == null)
                {
                    return NotFound();

                }
                _context.Mascotas.Remove(mascota);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
            return NoContent();

        }
        [HttpPost]
        public async Task<IActionResult> Post(Mascota mascota) {
            try
            {
                mascota.fechaCreacion = DateTime.Now;
                _context.Add(mascota);
                await _context.SaveChangesAsync();
                return CreatedAtAction("Get", new { id = mascota.Id }, mascota);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> Put(int id,Mascota mascota) 
        {
            try
            {
                if (id != mascota.Id)
                {
                    return BadRequest();
                }
                var mascotaItem = await _context.Mascotas.FindAsync(id);
                if (mascotaItem == null) 
                {
                    return NotFound();  
                
                }
                 mascotaItem.Nombre = mascota.Nombre;
                mascotaItem.Raza = mascota.Raza;
                mascotaItem.Edad = mascota.Edad;
                mascotaItem.Peso = mascota.Peso;
                mascotaItem.Color = mascota.Color;

                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        
        }
    }
}
