namespace kunze_prüfer.DataBase
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class QuerryTest : KunzeDB
    {
        public void test()
        {
            using (var db = new QuerryTest())
            {
                var kunde = new Kunde {k_name = "Sergio"};

                db.Kunden.Add(kunde);
                db.SaveChanges();
                
            }
        }

        public List<T> GetAll<T>() where T : class
        {
            using (var db = new QuerryTest())
            {
                return db.Set<T>().ToList();
            }
        }
        
public void UpdateAll<T>(Action<T> updateAction) where T : class
{
    using (var db = new QuerryTest())
    {
        var entities = db.Set<T>().ToList();
        foreach (var entity in entities)
        {
            updateAction(entity);
        }
        db.SaveChanges();
    }
}
        public T GetEntityById<T, TKey>(TKey id) where T : class
        {
            using (var db = new QuerryTest())
            {
                var entity = db.Set<T>().Find(id);
                return entity;
            }
        }

        public void mesto()
        {
            QuerryTest lol = new QuerryTest();
            Console.WriteLine(lol.GetAll<Kunde>());
            List<Kunde> bomba = lol.GetAll<Kunde>();
            foreach (var VARIABLE in bomba)
            {
                Console.WriteLine(VARIABLE.k_nr);
            }

            Console.WriteLine(lol.GetEntityById<Kunde, int>(1));
            //Console.WriteLine(lol.GetEntityById<Kunde, string>("Sergio"));
        }
    }
}