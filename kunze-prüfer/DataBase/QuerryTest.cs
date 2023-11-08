namespace kunze_prüfer.DataBase
{
    public class QuerryTest : KunzeDB
    {
        void test()
        {
            using (var db = new QuerryTest())
            {
                var kunde = new Kunde {k_name = "Sergio"};

                db.Kunden.Add(kunde);
                db.SaveChanges();
                
            }
            
        } 
    }
}