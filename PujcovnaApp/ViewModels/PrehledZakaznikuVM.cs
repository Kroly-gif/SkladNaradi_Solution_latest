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
    public class PrehledZakaznikuVM : BaseVM
    {
        public ObservableCollection<Zakaznik> SeznamZakazniku { get; set; } = new();
        public Zakaznik? VybranyZakaznik { get; set; }

        // Hledaný text
        public string HledanyText { get; set; } = string.Empty;

        public ICommand UlozitCommand { get; }
        public ICommand PridatCommand { get; }
        public ICommand SmazatCommand { get; }
        public ICommand FiltrovatCommand { get; }

        public PrehledZakaznikuVM()
        {
            NacistData();
            UlozitCommand = new RelayCommand(_ => Globals.UlozitData());
            PridatCommand = new RelayCommand(_ => PridatZaznam());
            SmazatCommand = new RelayCommand(_ => SmazatZaznam());
            FiltrovatCommand = new RelayCommand(_ => AplikovatFiltr());
        }

        private void NacistData()
        {
            if (Globals.Context == null) return;
            Globals.Context.Zakaznici.Load();
            SeznamZakazniku = new ObservableCollection<Zakaznik>(Globals.Context.Zakaznici.Local.ToList());
        }

        private void AplikovatFiltr()
        {
            if (string.IsNullOrWhiteSpace(HledanyText))
            {
                NacistData();
            }
            else
            {
                // Filtr podle Jména NEBO Příjmení
                var vyfiltrovano = Globals.Context.Zakaznici.Local
                    .Where(z => z.Prijmeni.ToLower().Contains(HledanyText.ToLower()) ||
                                z.Jmeno.ToLower().Contains(HledanyText.ToLower()))
                    .ToList();
                SeznamZakazniku = new ObservableCollection<Zakaznik>(vyfiltrovano);
            }
        }

        private void PridatZaznam()
        {
            var novy = new Zakaznik
            {
                Jmeno = "Nový",
                Prijmeni = "Zákazník"
            };
            Globals.Context.Zakaznici.Add(novy);
            VybranyZakaznik = novy;
            HledanyText = "";
            NacistData();
        }

        private void SmazatZaznam()
        {
            if (VybranyZakaznik == null) return;

            if (MessageBox.Show($"Smazat zákazníka {VybranyZakaznik.Prijmeni}?", "Dotaz", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Globals.Context.Zakaznici.Remove(VybranyZakaznik);
                AplikovatFiltr();
            }
        }
    }
}