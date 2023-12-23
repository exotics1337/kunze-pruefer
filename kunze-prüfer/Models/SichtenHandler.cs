using System.Threading.Tasks;
using kunze_prüfer.DataBase;

namespace kunze_prüfer.Models
{
    public class SichtenHandler
    {
      DBQ db = new DBQ(); 
      public async Task<bool> NeuerAuftrag(int id)
      {
          try
          {
              Auftrag auftrag = new Auftrag();
              auftrag.Auf_nr = id;
              await db.AddAsync(auftrag);
              return true;
          }
          catch
          {
              return false;
          }
            
      }
    }
}