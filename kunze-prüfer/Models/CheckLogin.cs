using System;
using System.Data.Entity;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using kunze_prüfer.DataBase;

namespace kunze_prüfer.Models
{
    public static class CheckLogin
    { 

        public static async Task<bool> ValidateLogin(int mitnr, string password)
        {
            using (KunzeDB db = new KunzeDB())
            {
                if (mitnr != null && password != null || password != "")
                {
                    try
                    {
                        var result = await db.Mitarbeiter.AnyAsync(x => x.M_nr == mitnr && x.M_pass == password && x.M_geloescht == false);
                        return result;
                    }
                    catch (Exception ex)
                    {
                        ErrorLogger.Log(ex);
                        return false;
                    }

                }
                else
                {
                    return false;
                }
            }
        }
        
        public static async Task<bool> IsAdmin(int mitnr)
        {
            using (KunzeDB db = new KunzeDB())
            {
                try
                {
                    var result = await db.Mitarbeiter.AnyAsync(x => x.M_nr == mitnr && x.M_admin == true);
                    return result;
                }
                catch (Exception ex)
                {
                    ErrorLogger.Log(ex);
                    return false;
                }
            }
        }
        
        public static async Task<string> GetName(int mitnr)
        {
            using (KunzeDB db = new KunzeDB())
            {
                try
                {
                    var result = await db.Mitarbeiter.Where(x => x.M_nr == mitnr).Select(x => x.M_vname + " " + x.M_nname).FirstOrDefaultAsync();
                    return result;
                }
                catch (Exception ex)
                {
                    ErrorLogger.Log(ex);
                    return null;
                }
            }
        }
    }
}