﻿using SolutionTemplate.BusinessModel;
using SolutionTemplate.Service.Core.Entities;

namespace SolutionTemplate.Service.Core.Interfaces
{
    public interface IWidgetService
    {
        PageResult<WidgetGet> GetWidgets(string sort = "Id", int pageNumber = 1, int pageSize = 10);

        PageResult<object> GetWidgets(string sort = "Id", int pageNumber = 1, int pageSize = 10, params string[] fields);

        WidgetGet GetWidget(int id);

        WidgetGet CreateWidget(WidgetPost widget);

        WidgetGet UpdateWidget(int id, WidgetPut widget);

        void DeleteWidget(int id);
    }
}