﻿using Microsoft.EntityFrameworkCore;

namespace BE_CRUDNET.Models.Repository
{
    public class MascotaRepository : IMascotaRepository
    {
        private readonly Context _context;

        public MascotaRepository(Context context)
        {
            _context = context; 
        
        }

        public async Task<Mascota> addMascota(Mascota mascota)
        {
            _context.Add(mascota);
            await _context.SaveChangesAsync();

            return mascota;
        }

        public async Task DeleteMascota(Mascota mascota)
        {
            _context.Mascotas.Remove(mascota);
            await _context.SaveChangesAsync();

        }

        public async Task<List<Mascota>> GetListMascotas()
        {
          return await _context.Mascotas.ToListAsync();
        }

        public async Task<Mascota> GetMascota(int Id)
        {
          return await _context.Mascotas.FindAsync(Id);
        }

        public async Task UpdateMascota(Mascota mascota)
        {
            var mascotaItem = await _context.Mascotas.FirstOrDefaultAsync(x => x.Id == mascota.Id);

            if (mascotaItem!=null)
            {
                mascotaItem.Nombre = mascota.Nombre;
                mascotaItem.Raza = mascota.Raza;
                mascotaItem.Edad = mascota.Edad;
                mascotaItem.Peso = mascota.Peso;
                mascotaItem.Color = mascota.Color;

                await _context.SaveChangesAsync();

            }


        }
    }
}
