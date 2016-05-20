using SolutionTemplate.BusinessModel;
using System.Collections.Generic;

namespace SolutionTemplate.Core.ServiceInterfaces
{
    public interface IWidgetService
    {
        List<Widget> GetActiveWidgets();
    }
}