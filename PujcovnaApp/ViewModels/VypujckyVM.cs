using DataEntity.Data;
using Microsoft.EntityFrameworkCore;
using PropertyChanged;
using PujcovnaApp.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PujcovnaApp.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class VypujckyVM : BaseVM
    {
        public ObservableCollection<Vypujcka> SeznamVypujcek { get; set; } = new();
        public ObservableCollection<Zakaznik> SeznamZakazniku { get; set; } = new();
        public ObservableCollection<Naradi> DostupneNaradi { get; set; } = new();

        // Výběry v tabulce a formuláři
        public Vypujcka? VybranaVypujcka { get; set; }
        public Zakaznik? VybranyZakaznikCmb { get; set; }
        public Naradi? VybraneNaradiCmb { get; set; }

        // --- DATA PRO FORMULÁŘE ---
        // 1. Pro nové půjčení
        public DateTime DatumVypujckyVyber { get; set; } = DateTime.Today;
        public DateTime DatumVraceniPlan { get; set; } = DateTime.Today.AddDays(1);

        // 2. Pro vracení (NOVÉ)
        public DateTime DatumVraceniSkutecneVyber { get; set; } = DateTime.Today;

        public string HledanyText { get; set; } = string.Empty;

        public ICommand VypujcitCommand { get; }
        public ICommand VratitCommand { get; }
        public ICommand FiltrovatCommand { get; }
        public ICommand ResetCommand { get; }

        public VypujckyVM()
        {
            NacistData();
            VypujcitCommand = new RelayCommand(_ => VytvoritVypujcku());
            VratitCommand = new RelayCommand(_ => VratitVypujcku());
            FiltrovatCommand = new RelayCommand(_ => AplikovatFiltr());
            ResetCommand = new RelayCommand(_ => ResetovatZmeny());
        }

        private void NacistData()
        {
            if (Globals.Context == null) return;

            Globals.Context.Vypujcky
                .Include(v => v.Zakaznik)
                .Include(v => v.Naradi)
                .Load();

            SeznamVypujcek = new ObservableCollection<Vypujcka>(Globals.Context.Vypujcky.Local.ToList());

            Globals.Context.Zakaznici.Load();
            SeznamZakazniku = Globals.Context.Zakaznici.Local.ToObservableCollection();

            Globals.Context.Naradi.Load();
            AktualizovatDostupneNaradi();
        }

        private void AktualizovatDostupneNaradi()
        {
            var vsechno = Globals.Context.Naradi.Local.ToList();
            var dostupne = vsechno.Where(n => n.Dostupnost == true).ToList();
            DostupneNaradi = new ObservableCollection<Naradi>(dostupne);
        }

        private void AplikovatFiltr()
        {
            if (string.IsNullOrWhiteSpace(HledanyText))
            {
                NacistData();
            }
            else
            {
                var vyfiltrovano = Globals.Context.Vypujcky.Local
                    .Where(v => v.Zakaznik.Prijmeni.ToLower().Contains(HledanyText.ToLower()) ||
                                v.Naradi.Nazev.ToLower().Contains(HledanyText.ToLower()))
                    .ToList();
                SeznamVypujcek = new ObservableCollection<Vypujcka>(vyfiltrovano);
            }
        }

        private void ResetovatZmeny()
        {
            // 1. Vyčistit formulář nahoře
            VybranyZakaznikCmb = null;
            VybraneNaradiCmb = null;
            DatumVypujckyVyber = DateTime.Today;
            DatumVraceniPlan = DateTime.Today.AddDays(1);

            // 2. Vyčistit výběr v tabulce a datum vracení
            VybranaVypujcka = null;
            DatumVraceniSkutecneVyber = DateTime.Today;

            // 3. Vymazat filtr a obnovit data
            HledanyText = "";
            NacistData();
        }

        private void VytvoritVypujcku()
        {
            if (VybranyZakaznikCmb == null || VybraneNaradiCmb == null)
            {
                MessageBox.Show("Vyberte zákazníka a nářadí!", "Chyba");
                return;
            }

            if (VybranyZakaznikCmb.Ban)
            {
                MessageBox.Show("Tento zákazník má BAN a nemůže si půjčovat!", "Zamítnuto");
                return;
            }

            if (DatumVraceniPlan < DatumVypujckyVyber)
            {
                MessageBox.Show("Datum vrácení nemůže být dříve než datum vypůjčení.", "Chyba data");
                return;
            }

            var pocetDni = (DatumVraceniPlan - DatumVypujckyVyber).Days;
            if (pocetDni < 1) pocetDni = 1;

            decimal cenaCelkem = pocetDni * VybraneNaradiCmb.CenaZaDen;

            var novaVypujcka = new Vypujcka
            {
                Zakaznik = VybranyZakaznikCmb,
                Naradi = VybraneNaradiCmb,
                DatumVypujcky = DatumVypujckyVyber,
                DatumVraceniPlan = DatumVraceniPlan,
                DatumVraceniSkutecne = null,
                Cena = cenaCelkem,
                Penale = 0
            };

            VybraneNaradiCmb.Dostupnost = false;

            Globals.Context.Vypujcky.Add(novaVypujcka);
            Globals.UlozitData();

            ResetovatZmeny();
            MessageBox.Show($"Půjčeno! Cena: {cenaCelkem:N0} Kč", "Hotovo");
        }

        private void VratitVypujcku()
        {
            if (VybranaVypujcka == null)
            {
                MessageBox.Show("Vyberte v tabulce výpůjčku, kterou chcete vrátit.");
                return;
            }
            if (VybranaVypujcka.DatumVraceniSkutecne != null)
            {
                MessageBox.Show("Tato výpůjčka už je vrácena.");
                return;
            }

            // --- ZDE JE ZMĚNA ---
            // Použijeme datum, které uživatel vybral dole v DatePickeru
            VybranaVypujcka.DatumVraceniSkutecne = DatumVraceniSkutecneVyber;

            // Kontrola data vrácení vůči datu půjčení (logická kontrola)
            if (VybranaVypujcka.DatumVraceniSkutecne < VybranaVypujcka.DatumVypujcky)
            {
                MessageBox.Show("Pozor: Datum vrácení je dříve než datum půjčení! (Opravte datum dole)", "Varování");
                // Resetujeme zpět na null, aby se neuložil nesmysl
                VybranaVypujcka.DatumVraceniSkutecne = null;
                return;
            }

            // Výpočet penále (Datum skutečného vrácení > Datum plánovaného vrácení)
            if (VybranaVypujcka.DatumVraceniSkutecne.Value.Date > VybranaVypujcka.DatumVraceniPlan.Date)
            {
                var dnyNavic = (VybranaVypujcka.DatumVraceniSkutecne.Value.Date - VybranaVypujcka.DatumVraceniPlan.Date).Days;
                if (dnyNavic > 0)
                {
                    VybranaVypujcka.Penale = dnyNavic * (VybranaVypujcka.Naradi.CenaZaDen * 2);
                    MessageBox.Show($"Pozdě vráceno ({dnyNavic} dnů navíc)!\nPenále: {VybranaVypujcka.Penale:N0} Kč", "Penále");
                }
            }
            else
            {
                // Pokud vrátil včas, penále je 0 (pro jistotu, kdyby tam něco viselo)
                VybranaVypujcka.Penale = 0;
            }

            VybranaVypujcka.Naradi.Dostupnost = true;

            Globals.UlozitData();
            AplikovatFiltr(); // Refresh tabulky
        }
    }
}