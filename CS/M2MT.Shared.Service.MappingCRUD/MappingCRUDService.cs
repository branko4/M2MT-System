using M2MT.Shared.Exceptions;
using M2MT.Shared.IRepository.InformationModel;
using M2MT.Shared.IRepository.Mapping;
using M2MT.Shared.IService.Mapping;
using M2MT.Shared.Model.Mapping;
using System;
using System.Threading.Tasks;
using IModel = M2MT.Shared.Model.InformationModel.Model;

namespace M2MT.Shared.Service.Mapping
{
    public class MappingCRUDService : MappingReadService, IMappingCRUDService
    {
        private IMappingCRUDRepository _repository;
        private IInformationModelReadRepository _modelRepository;

        public MappingCRUDService(IMappingCRUDRepository repository, IInformationModelReadRepository modelRepository) : base(repository)
        {
            _repository = repository;
            _modelRepository = modelRepository;
        }

        public async Task<MappingModel> Create(MappingModel mapping)
        {
            if (Guid.Empty.Equals(mapping.modelFrom) || Guid.Empty.Equals(mapping.modelTo)) throw new ArgumentException("model reference can not be null");
            if (mapping.modelFrom.Equals(mapping.modelTo)) throw new ArgumentException("model to and from can not be the same model");
            if (String.IsNullOrEmpty(mapping.Name)) throw new ArgumentException("Name can not be null or empty");
            if (mapping.Name.Length > 100) throw new ArgumentException("Name can not be longer then 100 characters");
            var modelFrom = await this._modelRepository.Excists(mapping.modelFrom);
            if (!modelFrom) 
                throw new NotFoundException<IModel>("model is not found");
            if (!await this._modelRepository.Excists(mapping.modelTo)) throw new NotFoundException<IModel>("model is not found");
            mapping.ID = Guid.NewGuid();
            return await _repository.Create(mapping);
        }

        public async Task<MappingModel> Remove(Guid mapping)
        {
            if (Guid.Empty.Equals(mapping)) throw new ArgumentException("Mapping reference is null");
            if (!await this._repository.Excists(mapping)) throw new NotFoundException<MappingModel>("mapping is not found");
            return await _repository.Remove(mapping);
        }
    }
}
