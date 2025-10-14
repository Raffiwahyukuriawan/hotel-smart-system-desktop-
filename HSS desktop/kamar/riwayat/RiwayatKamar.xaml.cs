using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using HSS_desktop.kamar.riwayat;

namespace HSS_desktop
{
    public partial class RiwayatKamarUserControl : UserControl
    {
        public event Action<string>? OnTitleChanged;

        public ObservableCollection<RiwayatKamarViewModel> RiwayatList { get; set; }
        private RiwayatKamarRepository repo = new RiwayatKamarRepository();

        public RiwayatKamarUserControl()
        {
            InitializeComponent();
            Loaded += RiwayatKamarUserControl_Loaded;
            LoadRiwayat();
            this.DataContext = this;
        }

        private void LoadRiwayat()
        {
            var riwayatFromDb = repo.GetRiwayat(); // ini sekarang List<RiwayatKamarViewModel>

            int no = 1;
            foreach (var r in riwayatFromDb)
            {
                r.No = no++; // nomor urut
            }

            RiwayatList = new ObservableCollection<RiwayatKamarViewModel>(riwayatFromDb);
            DataContext = this;
        }


        private void RiwayatKamarUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            OnTitleChanged?.Invoke("Data Riwayat Kamar");
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var riwayat = btn?.DataContext as RiwayatKamarViewModel;

            if (riwayat != null)
            {
                var modal = new UpdateRiwayat(riwayat);
                modal.Owner = Window.GetWindow(this);
                if (modal.ShowDialog() == true)
                {
                    LoadRiwayat();
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var riwayat = btn?.DataContext as RiwayatKamarViewModel;

            if (riwayat != null)
            {
                var result = MessageBox.Show(
                    $"Apakah yakin mau hapus riwayat dengan ID {riwayat.Id}?",
                    "Konfirmasi Hapus",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        repo.DeleteRiwayat(riwayat.Id); // panggil repository
                        LoadRiwayat(); // refresh data
                        MessageBox.Show($"Riwayat ID {riwayat.Id} berhasil dihapus!",
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
}
