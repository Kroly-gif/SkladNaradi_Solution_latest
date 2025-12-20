using PropertyChanged;
using PujcovnaApp.Helpers;
using System.Windows.Input;

namespace PujcovnaApp.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainViewModel : BaseVM
    {
        // Vlastnost, která určuje, co je právě vidět (Nářadí / Zákazníci / Výpůjčky)
        public object AktualniPohled { get; set; }

        // Definice příkazů pro tlačítka v menu
        public ICommand OtevritNaradiCommand { get; }
        public ICommand OtevritZakaznikyCommand { get; }
        public ICommand OtevritVypujckyCommand { get; }

        public MainViewModel()
        {
            // 1. Výchozí pohled při startu aplikace (Nářadí)
            AktualniPohled = new PrehledNaradiVM();

            // 2. Tlačítko Nářadí - přepne na PrehledNaradiVM
            OtevritNaradiCommand = new RelayCommand(_ =>
            {
                AktualniPohled = new PrehledNaradiVM();
            });

            // 3. Tlačítko Zákazníci - přepne na PrehledZakaznikuVM
            OtevritZakaznikyCommand = new RelayCommand(_ =>
            {
                AktualniPohled = new PrehledZakaznikuVM();
            });

            // 4. Tlačítko Výpůjčky - přepne na VypujckyVM (NOVÉ)
            OtevritVypujckyCommand = new RelayCommand(_ =>
            {
                AktualniPohled = new VypujckyVM();
            });
        }
    }
}