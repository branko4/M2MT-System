using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace M2MT.Shared.IRepository
{
    public interface IReadRepository<Model>
    {
        public Task<IEnumerable<Model>> GetAll();
        public Task<Model> GetOne(Guid id);
    }
}
