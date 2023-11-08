namespace kunze_prüfer.Models
{
    public static class NettoBrutto
    {
        private static double mwst = 0.19; // Hier später aus DB auslesen.
        static public double ZuNetto(double brutto)
        {
            return brutto / (1 + mwst);
        }
        static public double ZuBrutto(double netto)
        {
            return netto * (1 + mwst);
        }
    }
}