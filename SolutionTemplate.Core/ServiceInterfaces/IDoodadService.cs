using SolutionTemplate.BusinessModel;
using System.Collections.Generic;

namespace SolutionTemplate.Core.ServiceInterfaces
{
    public interface IDoodadService
    {
        List<DoodadGet> GetDoodads(int widgetId);

        DoodadGet GetDoodad(int id);

        DoodadGet CreateDoodad(DoodadPost Doodad);

        DoodadGet UpdateDoodad(int id, DoodadPut Doodad);

        void DeleteDoodad(int id);
    }
}