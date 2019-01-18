using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComputerWorld.Extensions
{
    public static class IEnumerableExtensions         //this class will convert the list to enum so we can use the drop down
    {
        public static IEnumerable<SelectListItem> ToSelectListItem<T>(this IEnumerable<T> items, int selectedValue)
        {
            return from item in items
                   select new SelectListItem
                   {
                       Text = item.GetPropertyValue("Name"),
                       Value = item.GetPropertyValue("Id"),       //retrieving the value
                       Selected = item.GetPropertyValue("Id").Equals(selectedValue.ToString())  
                   };
        }
    }
}
