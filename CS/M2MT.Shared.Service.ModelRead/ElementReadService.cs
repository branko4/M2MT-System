
using M2MT.Shared.Exceptions;
using M2MT.Shared.IRepository.InformationModel;
using M2MT.Shared.IService.InformationModel;
using M2MT.Shared.Model;
using M2MT.Shared.Model.InformationModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IModel = M2MT.Shared.Model.InformationModel.Model;


namespace M2MT.Shared.Service.Model
{
    public class ElementReadService : IElementReadService
    {
        private IElementReadRepository _repository;
        private IInformationModelReadRepository _modelRepository;

        public ElementReadService(IElementReadRepository repository, IInformationModelReadRepository modelRepository)
        {
            _repository = repository;
            _modelRepository = modelRepository;
        }

        public Task<IEnumerable<Element>> GetAll()
        {
            return _repository.GetAll();
        }

        public async Task<IEnumerable<Element>> GetAllElementsOfModel(Guid refTo)
        {
            if (!await this._modelRepository.Excists(refTo)) throw new NotFoundException<IModel>();
            if (new Guid().Equals(refTo)) throw new ArgumentException();
            return await _repository.GetAllElementsOfModel(refTo);
        }

        public Task<IEnumerable<Element>> GetElementsMatchingID(IEnumerable<Guid> elementIDs)
        {
            return _repository.GetElementsMatchingID(elementIDs);
        }

        public async Task<IEnumerable<Element>> GetElementsWithParents(Guid refToElement)
        {
            var elements = new List<Element>();
            await GetElementWithParentsCreator(elements, refToElement);
            return elements;

        }

        private async Task GetElementWithParentsCreator(IList<Element> elements, Guid currentElement)
        {
            Element loadedElement = await _repository.GetOne(currentElement);
            elements.Add(loadedElement);
            if (loadedElement.Parent.Equals(new Guid())) return;
            await GetElementWithParentsCreator(elements, loadedElement.Parent);
        }
    }
}
