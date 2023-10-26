using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Constants
{
    public static class EnumHelper
    {
        public static string DisplayName(Enum enumValue)
        {
            var displayAttribute = enumValue.GetType()
            .GetMember(enumValue.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>();

            return displayAttribute?.Name ?? enumValue.ToString();
        }
    }
}
