using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using HSS_desktop.foodDrink;

namespace HSS_desktop
{
    public partial class FoodDrinkListView : UserControl
    {
        public event Action<string> OnTitleChanged;

        public ObservableCollection<MakananMinuman> FoodDrinkList { get; set; }
        private MakananMinumanRepository repo = new MakananMinumanRepository();

        public FoodDrinkListView()
        {
            InitializeComponent();
            Loaded += FoodDrinkListView_Loaded;
            LoadFoodDrink();
            this.DataContext = this;
        }

        private void LoadFoodDrink()
        {
            var listFromDb = repo.GetMakananMinuman();

            int no = 1;
            foreach (var item in listFromDb)
            {
                item.No = no++; // nomor urut
            }

            FoodDrinkList = new ObservableCollection<MakananMinuman>(listFromDb);
            DataContext = this;
        }

        private void FoodDrinkListView_Loaded(object sender, RoutedEventArgs e)
        {
            OnTitleChanged?.Invoke("Halaman Makanan & Minuman");
        }

        private void TambahFoodDrink_Click(object sender, RoutedEventArgs e)
        {
            var modal = new TambahFoodDrink();
            modal.Owner = Window.GetWindow(this);
            modal.ShowDialog();
            LoadFoodDrink();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var mm = btn?.DataContext as MakananMinuman;

            if (mm != null)
            {
                var modal = new UpdateMakananMinuman(mm);
                modal.Owner = Window.GetWindow(this);
                if (modal.ShowDialog() == true)
                {
                    LoadFoodDrink();
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var item = btn?.DataContext as FoodDrink;

            if (item != null)
            {
                var result = MessageBox.Show(
                    $"Apakah yakin mau hapus {item.Nama}?",
                    "Konfirmasi Hapus",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        repo.DeleteMakananMinuman(item.Id ?? 0);
                        LoadFoodDrink();
                        MessageBox.Show($"{item.Nama} berhasil dihapus!",
                                        "Sukses",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Terjadi kesalahan: {ex.Message}",
                                        "Error",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error);
                    }
                }
            }
        }
    }

    public class FoodDrink
    {
        public int No { get; set; }

        public long? Id { get; set; }
        public string? Kategori { get; set; } = "";
        public string? Foto { get; set; } = "";
        public string? Nama { get; set; } = "";
        public decimal? Harga { get; set; }
    }
}
