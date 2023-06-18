using System.ComponentModel;
using System.Reflection;

namespace Messageria
{
    static class EnumsUtils
    {
        public static string GetDescription<T>(this T @enum) where T : Enum
        {
            FieldInfo field = @enum.GetType().GetField(@enum.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0) 
                return attributes[0].Description;

            return string.Empty;
        }
    }
}
