using System.ComponentModel;
using System.Reflection;

namespace IConText.Data
{
    public abstract class EntityBase<TEntity>
    {
        public abstract TEntity Parse(string[] properties);

        public abstract TEntity Copy(TEntity entity);
        protected TEntity ParseObjectFromProperties<TEntity>(string[] propertyKV,
                                            char split,
                                            Dictionary<string, string> propertyAliases = null)
            where TEntity : EntityBase<TEntity>, new()
        {
            TEntity obj = new TEntity();

            foreach (string attr in propertyKV)
            {
                string[] stringsplit = attr.Split(split);

                if (stringsplit.Length != 2)
                {
                    throw new ArgumentException($"Incorrect argument : {attr}");
                }

                string propKey = stringsplit[0];
                string propVal = stringsplit[1];

                PropertyInfo? pInfo = obj.GetType().GetProperty(propKey);

                // if no property was found, try the alias
                if (pInfo == null && propertyAliases != null && propertyAliases.ContainsKey(propKey))
                    pInfo = obj.GetType().GetProperty(propertyAliases[propKey]);

                if (pInfo == null)
                    throw new Exception($"No such property ({propKey}) found for type {typeof(TEntity)}!");

                TypeConverter converter = TypeDescriptor.GetConverter(pInfo.PropertyType);
                pInfo.SetValue(obj, converter.ConvertFromInvariantString(propVal));
            }

            return obj;
        }
    }
    public static class EntityBase
    {
        public static TEntity Parse<TEntity>(string[] properties) where TEntity : EntityBase<TEntity>, new()
        {
            TEntity entity = new TEntity();
            return entity.Parse(properties);
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class CanNotBeChangedByUserAttribute : Attribute
    {}
}