using SolutionTemplate.BusinessModel;
using System.Collections.Generic;

namespace SolutionTemplate.Core.ServiceInterfaces
{
    public interface IWidgetService
    {
        List<WidgetGet> GetWidgets(string sort = "Id", int pageNumber = 1, int pageSize = 10);

        WidgetGet GetWidget(int id);

        WidgetGet CreateWidget(WidgetPost widget);

        WidgetGet UpdateWidget(int id, WidgetPut widget);

        void DeleteWidget(int id);
    }
}