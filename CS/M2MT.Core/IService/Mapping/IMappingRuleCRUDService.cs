
using M2MT.Shared.Model;
using M2MT.Shared.Model.InformationModel;
using M2MT.Shared.Model.Mapping;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace M2MT.Shared.IService.Mapping
{
    public interface IMappingRuleCRUDService : ICRUDService<MappingRule>, IMappingRuleReadService
    {
        Task<MappingRule> AddElement(Guid mappingRule, Guid element);
    }
}
