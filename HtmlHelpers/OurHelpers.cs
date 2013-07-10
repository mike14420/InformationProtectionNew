using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace IpRequestMvc2.HtmlHelpers
{
    public static class OurHelpers
    {

        public static string RadioButtonList<T>(this HtmlHelper helper, IEnumerable<T> list, string name, string value, string text)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("name cannot be null or empty");
            }

            var sb = new StringBuilder();
            var enumerator = list.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var obj = enumerator.Current;
                var v = obj.GetType().GetProperty(value);
                var t = obj.GetType().GetProperty(text);

                sb.Append(System.Web.Mvc.Html.InputExtensions.RadioButton(helper, name, v.GetValue(obj, null)));
                sb.Append(t.GetValue(obj, null));
            }

            return sb.ToString();
        }

    }
}