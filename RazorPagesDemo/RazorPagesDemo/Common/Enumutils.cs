using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RazorPagesDemo.Common
{
    public static class Enumutils
    {
        public static IEnumerable<SelectListItem> EnumToSelectListItems<TEnum>()
        {
            return GetValues<TEnum>().Cast<int>().Select(i => new SelectListItem(Enum.GetName(typeof(TEnum), i), i.ToString()));
        }

        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
