using SharpRepository.Repository;
using SharpRepository.Repository.FetchStrategies;
using SolutionTemplate.BusinessModel;
using SolutionTemplate.Core.Claims;
using SolutionTemplate.Core.Exceptions;
using SolutionTemplate.Core.ModelMaps;
using SolutionTemplate.DataModel;
using SolutionTemplate.Service.Core.Interfaces;
using System.Collections.Generic;

namespace SolutionTemplate.Service
{
    public class DoodadService : IDoodadService
    {
        private readonly IClaims _claims;
        private readonly IRepository<Doodad, int> _doodadRepo;

        public DoodadService(IClaims claims, IRepository<Doodad, int> doodadRepo)
        {
            _claims = claims;
            _doodadRepo = doodadRepo;
        }

        public List<DoodadGet> FindDoodads(int widgetId)
        {
            var doodads = _doodadRepo.FindAll(x => x.WidgetId == widgetId);

            return doodads.ToBusinessModels();
        }

        public DoodadGet GetDoodad(int id)
        {
            var doodad = _doodadRepo.Get(id,
                new GenericFetchStrategy<Doodad>()
                    .Include(x => x.Widget));

            if (doodad == null)
            {
                throw new NotFoundException();
            }

            return doodad.ToBusinessModel();
        }

        public DoodadGet CreateDoodad(DoodadPost doodad)
        {
            var dataDoodad = doodad.ToDataModel();

            _doodadRepo.Add(dataDoodad);

            var result = dataDoodad.ToBusinessModel();

            return result;
        }

        public DoodadGet UpdateDoodad(int id, DoodadPut doodad)
        {
            var dataDoodad = _doodadRepo.Get(id);

            if (dataDoodad == null)
            {
                throw new NotFoundException();
            }

            dataDoodad = doodad.ToDataModel(dataDoodad);

            _doodadRepo.Update(dataDoodad);

            var result = dataDoodad.ToBusinessModel();

            return result;
        }

        public void DeleteDoodad(int id)
        {
            if (!_doodadRepo.Exists(id))
            {
                throw new NotFoundException();
            }

            _doodadRepo.Delete(id);
        }
    }
}