namespace BE_CRUDNET.Models.Repository
{
    public interface IMascotaRepository
    {
        Task<List<Mascota>> GetListMascotas();
        Task<Mascota> GetMascota(int Id);
        Task DeleteMascota(Mascota mascota);
        Task<Mascota> addMascota(Mascota mascota);
        Task UpdateMascota(Mascota mascota);
    }
}
