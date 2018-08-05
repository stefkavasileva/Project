using System;
using System.IO;
using System.Linq.Expressions;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Landmarks.Web.Common.Helpers
{
    public static class FormGroupFelper
    {
        public static IHtmlContent FormGroupFor<TModel, TResult>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TResult>> expression)
        {
            using (var writer = new StringWriter())
            {
                var label = htmlHelper.LabelFor(expression, new { @class = "col-sm-2 col-form-label text-right" });
                var editor = htmlHelper.EditorFor(expression, new { htmlAttributes = new { @class = "form-control col-md-10" } });
                var validationMessage = htmlHelper.ValidationMessageFor(expression, null,
                    new { @class = "text-danger col-sm-2 col-form-label text-right" });

                writer.Write("<div class=\"form-group row\">");
                label.WriteTo(writer, HtmlEncoder.Default);
                writer.Write("<div class=\"col-sm-10\">");
                writer.Write("<div class=\"md-form mt-0\">");
                editor.WriteTo(writer, HtmlEncoder.Default);
                writer.Write("</div>");
                writer.Write("</div>");
                validationMessage.WriteTo(writer, HtmlEncoder.Default);
                writer.Write("</div>");

                return new HtmlString(writer.ToString());
            }
        }
    }
}
