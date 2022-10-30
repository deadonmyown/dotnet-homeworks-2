using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Hw7.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hw7.MyHtmlServices;

public static class HtmlHelperExtensions
{
    public static IHtmlContent MyEditorForModel(this IHtmlHelper helper)
    {
        TagBuilder builder = new TagBuilder("div");
        IHtmlContent br = (new TagBuilder("br")).RenderStartTag();
        var model = helper.ViewData.Model;
        foreach (var property in helper.ViewData.ModelMetadata.ModelType.GetProperties())
        {
            TagBuilder div = new TagBuilder("div");
            string name = GetDisplayName(property);
            GetLabelData(property, name, div, br);
            GetInputData(property, name, div, br);
            if(model != null)
                ValidateData(property, model, div, br);
            builder.InnerHtml.AppendHtml(div);
        }
        return builder;
    }

    private static string GetDisplayName(PropertyInfo propertyInfo)
    {
        var attribute = propertyInfo.GetCustomAttribute<DisplayAttribute>();
        return attribute != null ? attribute.Name! : ParseName(propertyInfo);
    }

    private static string ParseName(PropertyInfo propertyInfo)
    {
        var words = Regex.Matches(propertyInfo.Name, "(^[a-z]+|[A-Z]+(?![a-z])|[A-Z][a-z]+)").Select(m => m.Value);
        return string.Join(" ", words);
    }

    private static void GetLabelData(PropertyInfo property, string propertyName, TagBuilder div, IHtmlContent? br = null)
    {
        TagBuilder label = new TagBuilder("label");
        label.MergeAttribute("for", property.Name);
        label.InnerHtml.AppendHtml(propertyName);
        div.InnerHtml.AppendHtml(label);
        if(br != null) div.InnerHtml.AppendHtml(br);
    }

    private static void GetInputData(PropertyInfo property, string propertyName, TagBuilder div, IHtmlContent br)
    {
        var type = property.PropertyType;
        if (type.IsEnum)
        {
            TagBuilder select = new TagBuilder("select");
            foreach (var value in Enum.GetValues(type))
                select.InnerHtml.AppendHtmlLine($"<option>{value}</option>");
            
            select.MergeAttribute("type", property.Name);
            div.InnerHtml.AppendHtml(select);
        }
        else
        {
            TagBuilder input = new TagBuilder("input");
            input.MergeAttribute("id", property.Name);
            input.MergeAttribute("type", type == typeof(int) ? "number" : "text");
            div.InnerHtml.AppendHtml(input);
        }
        div.InnerHtml.AppendHtml(br);
    }

    private static void ValidateData(PropertyInfo property, object? model, TagBuilder div, IHtmlContent br)
    {
        var attributes = property.GetCustomAttributes<ValidationAttribute>();
        var value = property.GetValue(model);
        foreach (var attr in attributes)
        {
            if (!attr.IsValid(value))
            {
                //GetLabelData(property, String.Empty, div);
                TagBuilder span = new TagBuilder("span");
                span.InnerHtml.AppendHtml(attr.ErrorMessage);
                div.InnerHtml.AppendHtml(span);
                div.InnerHtml.AppendHtml(br);
            }
        }
    }
} 