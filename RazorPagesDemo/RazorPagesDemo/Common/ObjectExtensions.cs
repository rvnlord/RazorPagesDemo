using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace RazorPagesDemo.Common
{
    public static class ObjectExtensions
    {
        public static string GetDisplayName<TModel, TProperty>(this TModel model, Expression<Func<TModel, TProperty>> expression)
        {
            var type = typeof(TModel);
            var memberExpression = (MemberExpression)expression.Body;
            var propertyName = memberExpression.Member is PropertyInfo ? memberExpression.Member.Name : null;

            if (string.IsNullOrEmpty(propertyName))
                return string.Empty;

            var attr = (DisplayNameAttribute)type.GetProperty(propertyName)?.GetCustomAttributes(typeof(DisplayNameAttribute), true).SingleOrDefault();


            if (attr != null) 
                return attr.DisplayName;

            var metadataType = (MetadataTypeAttribute)type.GetCustomAttributes(typeof(MetadataTypeAttribute), true).FirstOrDefault();

            if (metadataType == null)
                return propertyName;

            var property = metadataType.MetadataClassType.GetProperty(propertyName);
            if (property != null)
                attr = (DisplayNameAttribute)property.GetCustomAttributes(typeof(DisplayNameAttribute), true).SingleOrDefault();

            return attr != null ? attr.DisplayName : propertyName;
        }
    }
}
