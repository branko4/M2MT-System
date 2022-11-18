using System.Threading.Tasks;

namespace M2MT.Shared.IService
{
    public interface ICRUDService<Model> : IReadService<Model>
    {
        public Task<Model> Create(Model mapping);
        public Task<Model> Remove(Model mapping);
    }
}
