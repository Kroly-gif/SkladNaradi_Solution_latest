using System.Windows;
using DataEntity;
using Microsoft.EntityFrameworkCore;
using PujcovnaApp.Helpers;

namespace PujcovnaApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Globals.Context = new PujcovnaContext();

            // 1. NEJDŘÍVE SMAŽEME STAROU (Vyřeší tvou chybu "Database already exists")
            // Pozor: Toto smaže všechna data při každém startu. 
            // Až budeš chtít data uchovávat, tento řádek smaž nebo zakomentuj (//).
            Globals.Context.Database.EnsureDeleted();

            // 2. PAK VYTVOŘÍME NOVOU ČISTOU
            Globals.Context.Database.EnsureCreated();

            // 3. A NAPLNÍME JI DATY
            DatabaseSeeder.NaplnitData(Globals.Context);
        }
    }
}