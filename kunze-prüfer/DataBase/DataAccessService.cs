namespace kunze_prüfer.DataBase
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class DataAccessService
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
        
        public void UpdateEntities<T>(IEnumerable<T> entities) where T : class
        {
            foreach (var entity in entities)
            {
                _db.Entry(entity).State = EntityState.Modified;
            }
        
            _db.SaveChanges();
        }
        
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}