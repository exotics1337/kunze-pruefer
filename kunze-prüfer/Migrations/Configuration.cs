
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace kunze_prüfer.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<kunze_prüfer.DataBase.KunzeDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    } 
}