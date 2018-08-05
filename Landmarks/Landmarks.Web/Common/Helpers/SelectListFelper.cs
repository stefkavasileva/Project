using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Landmarks.Web.Common.Helpers
{
    public static class SelectListFelper
    {
        public static IHtmlContent SelectItemFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression,IEnumerable<SelectListItem> list)
        {
            using (var writer = new StringWriter())
            {
                var label = htmlHelper.LabelFor(expression, new { @class = "col-sm-2 col-form-label text-right" });
                var dropDown = htmlHelper.DropDownListFor(expression, list, new {@class = "btn btn-blue-grey dropdown-toggle col-md-9" });
                var validationMessage = htmlHelper.ValidationMessageFor(expression, null,
                    new { @class = "text-danger col-sm-2 col-form-label text-right" });

                writer.Write("<div class=\"form-group row\">");
                label.WriteTo(writer, HtmlEncoder.Default);
                dropDown.WriteTo(writer, HtmlEncoder.Default);
                validationMessage.WriteTo(writer, HtmlEncoder.Default);
                writer.Write("</div>");

                return new HtmlString(writer.ToString());
            }
        }
    }
}
