using AutoMapper;
using BE_CRUDNET.Models;
using BE_CRUDNET.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BE_CRUDNET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MascotaController : ControllerBase
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        public MascotaController(Context context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;   
        }

        [HttpGet]
        public async Task<IActionResult> obtenerNOmbres()
        {
            try
            {
                var listaMascotas = await _context.Mascotas.ToListAsync();
                var listMacotasDTO = _mapper.Map<IEnumerable<MascotaDTO>>(listaMascotas);
                return Ok(listMacotasDTO);

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
                var mascotaDTO = _mapper.Map<MascotaDTO>(mascota);
                return Ok(mascotaDTO);

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
        public async Task<IActionResult> Post(MascotaDTO mascotaDTO) {
            try
            {
                var mascota = _mapper.Map<Mascota>(mascotaDTO);
                
                mascota.fechaCreacion = DateTime.Now;
                _context.Add(mascota);
                await _context.SaveChangesAsync();
                
                var mascotaItem = _mapper.Map<MascotaDTO>(mascota);  
                return CreatedAtAction("Get", new { id = mascotaDTO.Id }, mascotaDTO);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> Put(int id,MascotaDTO mascotaDTO) 
        {
            try
            {
                var mascota = _mapper.Map<Mascota>(mascotaDTO);
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
