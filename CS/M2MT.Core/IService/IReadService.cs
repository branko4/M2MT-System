using System.Collections.Generic;
using System.Threading.Tasks;

namespace M2MT.Shared.IService
{
    public interface IReadService<Model>
    {
        public Task<IEnumerable<Model>> GetAll();
    }
}
