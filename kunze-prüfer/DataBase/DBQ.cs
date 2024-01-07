using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using kunze_prüfer.DataBase;

namespace kunze_prüfer.DataBase

{
    using System;
    using System.ComponentModel;
    using System.Data.Entity;
    using System.Threading.Tasks;

    public class DBQ : KunzeDB
    {
        public async Task<List<T>> GetAllAsync<T>() where T : class
        {
            using (var db = new DBQ())
            {
                try
                {
                    return await db.Set<T>().ToListAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
        
        public Task<List<T>> GetAll<T>() where T : class
        {
            return Task.Run(() => GetAllAsync<T>().Result);
        }

        
        public async Task UpdateAllAsync<T>(Action<T> updateAction) where T : class
        {
            using (var db = new DBQ())
            {
                try
                {
                    var entities = await db.Set<T>().ToListAsync();
                    foreach (var entity in entities)
                    {
                        updateAction(entity);
                    }
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    // Fehlerbehandlung hier
                    throw; // oder passenden Fehler zurückgeben
                }
            }
        }

        public async Task<T> AddAsync<T>(T entity) where T : class
        {
            using (var db = new DBQ())
            {
                try
                {
                    db.Set<T>().Add(entity);
                    await db.SaveChangesAsync();
                    return entity;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
        
        public async Task<T> GetEntityByIdAsync<T, TKey>(TKey id) where T : class
        {
            using (var db = new DBQ())
            {
                try
                {
                    return await db.Set<T>().FindAsync(id);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
        
        public T GetLastEntity<T, TKey>(Func<T, TKey> keySelector) where T : class
        {
            using (var db = new DBQ())
            {
                try
                {
                    return db.Set<T>().OrderByDescending(keySelector).FirstOrDefault();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }   
        
        public void UpdateById<T, TKey>(TKey id, Action<T> updateAction, Func<T, TKey> idSelector) where T : class
        {
            using (var db = new DBQ())
            {
                try
                {
                    var entity = db.Set<T>().SingleOrDefault(e => idSelector(e).Equals(id)); 
                    if (entity != null)
                    {
                        updateAction(entity);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
              
              
            }
            /*
            Implmentation
            UpdateById<Kunde, int>(
                123, // Die Kunden-ID
                kunde => kunde.Name = "Neuer Name", // Die Update-Logik
                kunde => kunde.KundenId // Die Funktion, um die Kunden-ID zu extrahieren
                );
             */
        }
        
        public async Task<List<T>> GetFilteredAsync<T>(string query) where T : class
        {
            using (var db = new DBQ())
            {
                // Get all entities of type T
                var allEntities = await db.Set<T>().ToListAsync();

                // Filter entities based on the query
                var filteredEntities = allEntities.Where(entity =>
                {
                    // Check each string property to see if it contains the query
                    foreach (var prop in typeof(T).GetProperties())
                    {
                        if (prop.PropertyType == typeof(string))
                        {
                            var value = prop.GetValue(entity) as string;
                            if (!string.IsNullOrEmpty(value) && value.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                return true; // If any property contains the query, return true
                            }
                        }
                    }
                    return false; // If no properties contain the query, return false
                }).ToList();

                return filteredEntities;
            }
        }
        
    }
}