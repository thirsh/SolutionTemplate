using SolutionTemplate.BusinessModel;
using System.Collections.Generic;

namespace SolutionTemplate.Service.Core.Interfaces
{
    public interface IDoodadService
    {
        List<DoodadGet> FindDoodads(int widgetId);

        DoodadGet GetDoodad(int id);

        DoodadGet CreateDoodad(DoodadPost doodad);

        DoodadGet UpdateDoodad(int id, DoodadPut doodad);

        void DeleteDoodad(int id);
    }
}