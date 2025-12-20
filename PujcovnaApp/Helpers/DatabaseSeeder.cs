using DataEntity;
using DataEntity.Data;
using System.Linq;

namespace PujcovnaApp.Helpers
{
    public static class DatabaseSeeder
    {
        public static void NaplnitData(PujcovnaContext context)
        {
            if (context.Zakaznici.Any()) return;

            // 1. ZÁKAZNÍCI
            var z1 = new Zakaznik { Jmeno = "Petr", Prijmeni = "Novák", Telefon = "777111222", Email = "petr@novak.cz", Adresa = "Plzeňská 5", Ban = false };
            var z2 = new Zakaznik { Jmeno = "Jana", Prijmeni = "Dvořáková", Telefon = "608999000", Email = "jana@seznam.cz", Adresa = "Pražská 10", Ban = false };
            var z3 = new Zakaznik { Jmeno = "Karel", Prijmeni = "Zlý", Telefon = "158", Poznamka = "Neustále dluží, nepučovat!", Ban = true };
            var z4 = new Zakaznik { Jmeno = "Tomáš", Prijmeni = "Svoboda", Telefon = "777888999", Email = "tomas@firma.cz", Adresa = "Brněnská 12", Ban = false };
            var z5 = new Zakaznik { Jmeno = "Eva", Prijmeni = "Krejčí", Telefon = "602123456", Adresa = "Lipová 8", Ban = false };
            var z6 = new Zakaznik { Jmeno = "Lukáš", Prijmeni = "Veselý", Telefon = "720555666", Email = "lukas@stavba.cz", Organizace = "Stavby s.r.o.", Ban = false };
            var z7 = new Zakaznik { Jmeno = "Martin", Prijmeni = "Černý", Telefon = "603777888", Adresa = "Lesní 99", Ban = false };
            var z8 = new Zakaznik { Jmeno = "Alena", Prijmeni = "Bílá", Telefon = "775444111", Poznamka = "VIP klient", Ban = false };
            var z9 = new Zakaznik { Jmeno = "Jakub", Prijmeni = "Rychlý", Telefon = "776999888", Organizace = "Rychlý & Syn", Ban = false };
            var z10 = new Zakaznik { Jmeno = "Monika", Prijmeni = "Malá", Telefon = "608222333", Adresa = "Dlouhá 45", Ban = false };

            context.Zakaznici.AddRange(z1, z2, z3, z4, z5, z6, z7, z8, z9, z10);

            // 2. NÁŘADÍ (Doplněny popisy, hmotnosti...)
            var n1 = new Naradi { Nazev = "Vrtací kladivo Bosch", CenaZaDen = 350, Vykon = "800W", Hmotnost = "3.2 kg", Umisteni = "Regál A1", Popis = "Profi kladivo SDS-Plus", Dostupnost = true };
            var n2 = new Naradi { Nazev = "Úhlová bruska Makita", CenaZaDen = 200, Vykon = "1200W", Hmotnost = "2.1 kg", Umisteni = "Regál A2", Popis = "Kotouč 125mm", Dostupnost = true };
            var n3 = new Naradi { Nazev = "Průmyslový vysavač", CenaZaDen = 450, Vykon = "2000W", Hmotnost = "12 kg", Umisteni = "Sklad B", Popis = "Mokré i suché sání", Poznamka = "Pravidelně čistit filtry!", Dostupnost = true };
            var n4 = new Naradi { Nazev = "Sbíječka Hilti", CenaZaDen = 800, Vykon = "3000W", Hmotnost = "29 kg", Umisteni = "Sklad B", Popis = "Bourací kladivo TE-3000", Poznamka = "Pouze pro zkušené", Dostupnost = true };
            var n5 = new Naradi { Nazev = "Aku šroubovák DeWalt", CenaZaDen = 150, Vykon = "18V", Hmotnost = "1.5 kg", Umisteni = "Regál C1", Popis = "Včetně 2 baterií", Dostupnost = true };
            var n6 = new Naradi { Nazev = "Vibrační deska", CenaZaDen = 1200, Vykon = "4.8 kW", Hmotnost = "90 kg", Umisteni = "Sklad Venku", Popis = "Hutnění podloží", Dostupnost = true };
            var n7 = new Naradi { Nazev = "Míchačka 140l", CenaZaDen = 300, Vykon = "230V", Hmotnost = "55 kg", Umisteni = "Sklad Venku", Popis = "Na 2 kolečka", Dostupnost = true };
            var n8 = new Naradi { Nazev = "Horkovzdušná pistole", CenaZaDen = 100, Vykon = "2000W", Hmotnost = "0.8 kg", Umisteni = "Regál C2", Popis = "Regulace teploty", Dostupnost = true };
            var n9 = new Naradi { Nazev = "Řezačka na dlažbu", CenaZaDen = 400, Vykon = "Ruční", Hmotnost = "15 kg", Umisteni = "Sklad B", Popis = "Max délka 60cm", Dostupnost = true };
            var n10 = new Naradi { Nazev = "Elektrocentrála Honda", CenaZaDen = 950, Vykon = "5kW", Hmotnost = "85 kg", Umisteni = "Sklad A", Popis = "Benzínová, 2x230V", Poznamka = "Vrátit s plnou nádrží", Dostupnost = true };

            context.Naradi.AddRange(n1, n2, n3, n4, n5, n6, n7, n8, n9, n10);

            context.SaveChanges();
        }
    }
}