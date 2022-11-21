using System.Threading.Tasks;

namespace M2MT.Shared.IRepository
{
    public interface ICRUDRepository<Model> : IReadRepository<Model>
    {
        public Task<Model> Create(Model mapping);
        public Task<Model> Remove(Model mapping);
    }
}
