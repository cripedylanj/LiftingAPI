using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiftingAPI.Helpers
{
    public interface IUpdateModel<T> where T : class
    {
        T CopyNonNullItems(T copyModel, T originalModel);
    }
}
