
using M2MT.Shared.IRepository.InformationModel;
using M2MT.Shared.IService.InformationModel;
using M2MT.Shared.Model;
using M2MT.Shared.Model.InformationModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace M2MT.Shared.Service.Model
{
    public class ElementReadService : IElementReadService
    {
        private IElementReadRepository _repository;

        public ElementReadService(IElementReadRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Element>> GetAll()
        {
            return _repository.GetAll();
        }

        public Task<IEnumerable<Element>> GetAllElementsOfModel(Guid refTo)
        {
            return _repository.GetAllElementsOfModel(refTo);
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
