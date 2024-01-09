namespace kunze_prüfer.DataBase
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Threading.Tasks;

    public class DataAccessService : DBQ

    {
        private DBQ _db;

        public DataAccessService()
        {
            _db = new DBQ();
        }

        public IEnumerable<T> GetEntities<T>() where T : class
        {
            return _db.Set<T>().ToList();
        }

        // public void UpdateEntities<T>(IEnumerable<T> entities, string keyName) where T : class
        // {
        //     foreach (var entity in entities)
        //     {
        //         var primaryKey = GetPrimaryKey(entity, keyName);
        //         if (primaryKey == null || Convert.ToInt32(primaryKey) >= 0 && entity == null)
        //         {
        //             _db.Set<T>().Add(entity);
        //         }
        //         else
        //         {
        //             var entityInDb = _db.Set<T>().Find(primaryKey);
        //             if (entityInDb == null)
        //             {
        //                 if (_db.Entry(entity) == null)
        //                 {
        //                     _db.Set<T>().Add(entity);
        //                 }
        //                 else
        //                 {
        //                     _db.Entry(entity).State = EntityState.Modified;
        //                     Console.WriteLine("TEETET");
        //                 }
        //             }
        //             else
        //             {
        //                 _db.Entry(entityInDb).CurrentValues.SetValues(entity);
        //             }    
        //         }
        //         
        //     }
        //     try
        //     {
        //         _db.SaveChanges();
        //     }
        //     catch (Exception e)
        //     {
        //         Console.WriteLine(e);
        //         throw;
        //     }
        // }
        //
        // private bool IsEntityWithIdExists<T>(object id) where T : class
        // {
        //     return _db.Set<T>().Find(id) != null;
        // }
        //
        //
        // private object GetPrimaryKey<T>(T entity, string keyName) where T : class
        // {
        //     if (entity == null)
        //     {
        //         throw new ArgumentNullException(nameof(entity));
        //     }
        //
        //     var keyProperty = entity.GetType().GetProperty(keyName);
        //     if (keyProperty != null)
        //     {
        //         return keyProperty.GetValue(entity);
        //     }
        //
        //     throw new InvalidOperationException("Primärschlüssel nicht gefunden.");
        // }
        
        private IDictionary<string, object> GetPrimaryKey<T>(T entity) where T : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var entityType = typeof(T);
            var objectContext = ((IObjectContextAdapter)_db).ObjectContext;
            var entitySet = objectContext.CreateObjectSet<T>().EntitySet;
            var keyNames = entitySet.ElementType.KeyMembers.Select(k => k.Name).ToList();

            var keys = new Dictionary<string, object>();
            foreach (var keyName in keyNames)
            {
                var keyProperty = entityType.GetProperty(keyName);
                if (keyProperty != null)
                {
                    keys.Add(keyName, keyProperty.GetValue(entity));
                }
            }

            if (keys.Count == 0)
            {
                throw new InvalidOperationException("Primärschlüssel nicht gefunden.");
            }

            return keys;
        }


        public void UpdateEntities<T>(IEnumerable<T> entities) where T : class
        {
            foreach (var entity in entities)
            {
                var primaryKeys = GetPrimaryKey(entity);
                var entityInDb = _db.Set<T>().Find(primaryKeys.Values.ToArray());
        
                if (entityInDb == null)
                {
                    _db.Set<T>().Add(entity);
                }
                else
                {
                    _db.Entry(entityInDb).CurrentValues.SetValues(entity);
                }
            }

            try
            {
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}