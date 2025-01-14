using System.Collections.ObjectModel;

namespace odev
{
    public partial class HavaDurumu : ContentPage
    {
        public ObservableCollection<SehirHavaDurumu> Sehirler { get; set; }

        public HavaDurumu()
        {
            InitializeComponent();
            Sehirler = new ObservableCollection<SehirHavaDurumu>();
            CitiesCollectionView.ItemsSource = Sehirler;
        }

        private async void OnAddCityButtonClicked(object sender, EventArgs e)
          {
            string sehir = await DisplayPromptAsync("Şehir:", "Şehir İsmini Yazınız", "Tamam", "İptal");

            if (!string.IsNullOrWhiteSpace(sehir))
            {
                sehir = sehir.ToUpper(System.Globalization.CultureInfo.CurrentCulture);

                sehir = sehir.Replace('Ç', 'C');
                sehir = sehir.Replace('Ğ', 'G');
                sehir = sehir.Replace('İ', 'I');
                sehir = sehir.Replace('Ö', 'O');
                sehir = sehir.Replace('Ü', 'U');
                sehir = sehir.Replace('Ş', 'S');

                Sehirler.Add(new SehirHavaDurumu() { Name = sehir });
            }
        }

        private void OnRefreshButtonClicked(object sender, EventArgs e)
        {
            CitiesCollectionView.ItemsSource = null;
            CitiesCollectionView.ItemsSource = Sehirler;
        }

        private void OnDeleteCityButtonClicked(object sender, EventArgs e)
        {
            var button = sender as ImageButton;
            var sehirHavaDurumu = button?.CommandParameter as SehirHavaDurumu;
            if (sehirHavaDurumu != null)
            {
                Sehirler.Remove(sehirHavaDurumu);
            }
        }
    }

    public class SehirHavaDurumu
    {
        public string Name { get; set; }
        public string Source => $"https://www.mgm.gov.tr/sunum/tahmin-klasik-5070.aspx?m={Name}&basla=1&bitir=5&rC=111&rZ=fff";
    }
}
