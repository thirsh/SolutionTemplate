using SolutionTemplate.BusinessModel;
using System.Collections.Generic;

namespace SolutionTemplate.Core.ServiceInterfaces
{
    public interface IWidgetService
    {
        List<WidgetGet> GetWidgets();

        WidgetGet GetWidget(int id);

        WidgetGet CreateWidget(WidgetPost widget);

        WidgetGet UpdateWidget(int id, WidgetPut widget);

        void DeleteWidget(int id);
    }
}