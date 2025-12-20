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
        public ICommand FiltrovatCommand { get; } // NOVÝ COMMAND

        public PrehledNaradiVM()
        {
            NacistData();
            UlozitCommand = new RelayCommand(_ => Globals.UlozitData());
            PridatCommand = new RelayCommand(_ => PridatNovyZaznam());
            SmazatCommand = new RelayCommand(_ => SmazatZaznam());

            // Logika filtrování
            FiltrovatCommand = new RelayCommand(_ => AplikovatFiltr());
        }

        private void NacistData()
        {
            if (Globals.Context == null) return;
            Globals.Context.Naradi.Load();
            // Načteme všechna data
            SeznamNaradi = new ObservableCollection<Naradi>(Globals.Context.Naradi.Local.ToList());
        }

        private void AplikovatFiltr()
        {
            if (string.IsNullOrWhiteSpace(HledanyText))
            {
                // Pokud je pole prázdné, načteme vše
                NacistData();
            }
            else
            {
                // Vyfiltrujeme podle Názvu NEBO Umístění (velká/malá písmena neřešíme díky ToLower)
                var vyfiltrovano = Globals.Context.Naradi.Local
                    .Where(n => n.Nazev.ToLower().Contains(HledanyText.ToLower()) ||
                                n.Umisteni.ToLower().Contains(HledanyText.ToLower()))
                    .ToList();

                SeznamNaradi = new ObservableCollection<Naradi>(vyfiltrovano);
            }
        }

        private void PridatNovyZaznam()
        {
            var noveNaradi = new Naradi
            {
                Nazev = "Nové nářadí",
                Dostupnost = true
            };

            Globals.Context.Naradi.Add(noveNaradi);
            VybraneNaradi = noveNaradi;
            // Po přidání obnovíme seznam (zrušíme filtr, aby bylo nové nářadí vidět)
            HledanyText = "";
            NacistData();
        }

        private void SmazatZaznam()
        {
            if (VybraneNaradi == null) return;

            if (MessageBox.Show($"Opravdu smazat {VybraneNaradi.Nazev}?", "Smazat", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Globals.Context.Naradi.Remove(VybraneNaradi);
                // Obnovit seznam
                AplikovatFiltr();
            }
        }
    }
}