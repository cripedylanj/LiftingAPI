using LiftingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace LiftingAPI.Helpers
{
    public class UpdateModel<T> : IUpdateModel<T> where T : class
    {
        public T CopyNonNullItems(T copyModel, T originalModel)
        {
            if (copyModel == null || originalModel == null)
            {
                return null;
            }
            T updatedModel = copyModel;
            int count = 0;
            int othercounter = 0;
            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                var checkProperty = property.GetValue(updatedModel);
                var updateProperty = property.GetValue(originalModel);
                if (checkProperty == null)
                {
                    othercounter++;
                    property.SetValue(updatedModel, updateProperty);
                }
                count++;
            }
            if (count != othercounter)
            {
                return updatedModel;
            }
            return null;
        }
    }
}
