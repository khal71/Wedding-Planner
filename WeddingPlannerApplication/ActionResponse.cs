using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingPlannerApplication
{
    public class ActionResponse<TModel>
    {

        public TModel? Model { get; private set; }
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }

        public ActionResponse() { }

        public ActionResponse(TModel resource)
        {
            IsSuccess = true;
            Model = resource;
        }

        public ActionResponse(string errorMessage)
        {
            IsSuccess = false;
            Message = errorMessage;
            Model = default;
        }

    }
}

