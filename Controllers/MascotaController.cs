using AutoMapper;
using BE_CRUDNET.Models;
using BE_CRUDNET.Models.DTO;
using BE_CRUDNET.Models.Repository;
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
        
        private readonly IMapper _mapper;
        private readonly IMascotaRepository _repository;
        public MascotaController(IMapper mapper,IMascotaRepository mascotaRepository)
        {
            _mapper = mapper;   
            _repository = mascotaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> obtenerNOmbres()
        {
            try
            {
                var listaMascotas = await _repository.GetListMascotas();
                var listMacotasDTO = _mapper.Map<IEnumerable<MascotaDTO>>(listaMascotas);
                return Ok(listMacotasDTO);

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(int Id)
        {
            try
            {
                var mascota = await _repository.GetMascota(Id);
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
                var mascota = await _repository.GetMascota(Id);
                if (mascota == null)
                {
                    return NotFound();

                 }
                await _repository.DeleteMascota(mascota);
                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }


        }
        [HttpPost]
        public async Task<IActionResult> Post(MascotaDTO mascotaDTO)
        {
            try
            {
                var mascota = _mapper.Map<Mascota>(mascotaDTO);

                mascota.fechaCreacion = DateTime.Now;

                mascota = await _repository.addMascota(mascota);
                 
                var mascotaItemDTO = _mapper.Map<MascotaDTO>(mascota);
                return CreatedAtAction("Get", new { id = mascotaDTO.Id }, mascotaDTO);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> Put(int Id, MascotaDTO mascotaDTO)
        { 
            try
            {
                var mascota = _mapper.Map<Mascota>(mascotaDTO);
                if (Id != mascota.Id)
                {
                    return BadRequest();
                }
                var mascotaItem = await _repository.GetMascota(Id);

                if (mascotaItem == null)
                {
                    return NotFound();
                }
                await _repository.UpdateMascota(mascota);
                return NoContent();
            }
            catch (NotSupportedException ex)
            {

                return BadRequest(ex);
            }

        }
    }
}
