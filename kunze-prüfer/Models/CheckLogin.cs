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
                        var result = await db.Mitarbeiter.AnyAsync(x => x.M_nr == mitnr && x.M_pass == password);
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
        
    }
}