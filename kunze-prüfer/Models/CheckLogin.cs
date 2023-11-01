namespace kunze_prüfer.Models
{
    public static class CheckLogin
    { 

        public static bool ValidateLogin(int mitnr, string password)
        {
            if (mitnr != null && password != null || password != "") ;
            { 
                // do something
                return true;
            }
        }
        
    }
}