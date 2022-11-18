using M2MT.Shared.IRepository.Mapping;
using M2MT.Shared.Model.Mapping;
using System.Threading.Tasks;

namespace M2MT.Shared.Repository.Mapping
{
    public class MappingRelationCRUDRepository : MappingRelationReadRepository, IMappingRelationCRUDRepository
    {
        public Task<MappingRelation> Create(MappingRelation mapping)
        {
            throw new System.NotImplementedException();
        }

        public Task<MappingRelation> Remove(MappingRelation mapping)
        {
            throw new System.NotImplementedException();
        }
    }
}
