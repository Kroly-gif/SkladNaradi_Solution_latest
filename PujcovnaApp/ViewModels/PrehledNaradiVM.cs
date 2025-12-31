using DataEntity.Data;
using Microsoft.EntityFrameworkCore;
using PropertyChanged;
using PujcovnaApp.Helpers;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PujcovnaApp.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class PrehledNaradiVM : BaseVM
    {
        public ObservableCollection<Naradi> SeznamNaradi { get; set; } = new();
        public Naradi? VybraneNaradi { get; set; }

        // Text, který uživatel napíše do vyhledávání
        public string HledanyText { get; set; } = string.Empty;

        public ICommand UlozitCommand { get; }
        public ICommand PridatCommand { get; }
        public ICommand SmazatCommand { get; }
        public ICommand FiltrovatCommand { get; }

        public PrehledNaradiVM()
        {
            NacistData();
            // Tlačítko Uložit teď slouží hlavně pro úpravy (změna názvu, ceny existujícího nářadí)
            UlozitCommand = new RelayCommand(_ => Globals.UlozitData());
            PridatCommand = new RelayCommand(_ => PridatNovyZaznam());
            SmazatCommand = new RelayCommand(_ => SmazatZaznam());
            FiltrovatCommand = new RelayCommand(_ => AplikovatFiltr());
        }

        private void NacistData()
        {
            if (Globals.Context == null) return;

            // Načteme data z DB do paměti
            Globals.Context.Naradi.Load();

            // Zobrazíme je v seznamu
            SeznamNaradi = new ObservableCollection<Naradi>(Globals.Context.Naradi.Local.ToList());
        }

        private void AplikovatFiltr()
        {
            if (string.IsNullOrWhiteSpace(HledanyText))
            {
                NacistData();
            }
            else
            {
                // Poznámka: Pokud by 'Umisteni' neexistovalo v modelu, smaz tu část podmínky
                var vyfiltrovano = Globals.Context.Naradi.Local
                    .Where(n => (n.Nazev != null && n.Nazev.ToLower().Contains(HledanyText.ToLower())) ||
                                (n.Umisteni != null && n.Umisteni.ToLower().Contains(HledanyText.ToLower())))
                    .ToList();

                SeznamNaradi = new ObservableCollection<Naradi>(vyfiltrovano);
            }
        }

        private void PridatNovyZaznam()
        {
            var noveNaradi = new Naradi
            {
                Nazev = "Nové nářadí",
                Dostupnost = true,
                CenaZaDen = 0 // Je dobré inicializovat i cenu
            };

            // 1. Přidáme do kontextu (zatím má ID 0)
            Globals.Context.Naradi.Add(noveNaradi);

            // 2. Oprava:
            // Okamžitě uložíme do DB. Databáze vygeneruje ID a vrátí ho zpět.
            // Nářadí tak bude mít unikátní ID hned od začátku.
            Globals.Context.SaveChanges();

            VybraneNaradi = noveNaradi;

            // Vyčistíme filtr, aby bylo nové nářadí vidět
            HledanyText = "";

            // Znovunačtení není nutně potřeba (EF to aktualizuje sám), 
            // ale pro jistotu aktualizujeme seznam v okně.
            NacistData();
        }

        private void SmazatZaznam()
        {
            if (VybraneNaradi == null) return;

            if (MessageBox.Show($"Opravdu smazat {VybraneNaradi.Nazev}?", "Smazat", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Globals.Context.Naradi.Remove(VybraneNaradi);

                // I zde je dobré smazání potvrdit hned do DB
                Globals.Context.SaveChanges();

                AplikovatFiltr(); // Aktualizuje seznam (zmizí smazané)
            }
        }
    }
}