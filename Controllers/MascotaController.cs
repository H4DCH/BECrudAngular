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
                var mascota =await _context.Mascotas.FindAsync(Id);
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
    }
}
