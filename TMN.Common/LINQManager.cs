using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace TMN
{
    public class LINQManager
    {
        public void GenericDetach<T>(T entity) where T : class
        {
            foreach (PropertyInfo pi in entity.GetType().GetProperties())
            {
                if (pi.GetCustomAttributes(typeof(System.Data.Linq.Mapping.AssociationAttribute), false).Length > 0)
                {
                    // Property is associated to another entity
                    Type propType = pi.PropertyType;
                    // Invoke Empty contructor (set to default value)
                    ConstructorInfo ci = propType.GetConstructor(new Type[0]);
                    pi.SetValue(entity, ci.Invoke(null), null);
                }
            }
        }


        public T GenericCompleteDetach<T>(T entity) where T : class
        {
            Type entityType = entity.GetType();
            ConstructorInfo eci = entityType.GetConstructor(new Type[0]);
            var newentity = eci.Invoke(null);
            foreach (PropertyInfo pi in entity.GetType().GetProperties())
            {
                if (pi.GetCustomAttributes(typeof(System.Data.Linq.Mapping.AssociationAttribute), false).Length == 0)
                {
                    pi.SetValue(newentity, pi.GetValue(entity, null), null);
                    // Property is associated to another entity
                    //Type propType = pi.PropertyType;
                    // Invoke Empty contructor (set to default value)
                    //ConstructorInfo ci = propType.GetConstructor(new Type[0]);
                    //pi.SetValue(entity, ci.Invoke(null), null);
                }
            }
            return (T)newentity;
        }
    }
}
