using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using HSS_desktop.kamar.kategori;

namespace HSS_desktop
{
    public partial class KategoriKamarUserControl : UserControl
    {
        public event Action<string> OnTitleChanged;

        public ObservableCollection<KategoriKamar> KamarList { get; set; }
        private KategoriKamarRepository repo = new KategoriKamarRepository();

        public KategoriKamarUserControl()
        {
            InitializeComponent();
            Loaded += KategoriKamarUserControl_Loaded;
            LoadKategori();
            this.DataContext = this;
        }

        private void LoadKategori()
        {
            var kategoriFromDb = repo.GetKategori();

            int no = 1;
            foreach (var k in kategoriFromDb)
            {
                k.No = no++; // nomor urut
            }

            KamarList = new ObservableCollection<KategoriKamar>(kategoriFromDb);
            DataContext = this;
        }

        private void KategoriKamarUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            OnTitleChanged?.Invoke("Halaman Kategori Kamar");
        }

        private void TambahKategori_Click(object sender, RoutedEventArgs e)
        {
            var modal = new TambahKategori();
            modal.Owner = Window.GetWindow(this);
            modal.ShowDialog();
            LoadKategori();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var kategori = btn?.DataContext as KategoriKamar;

            if (kategori != null)
            {
                var modal = new UpdateKategori(kategori);
                modal.Owner = Window.GetWindow(this);
                if (modal.ShowDialog() == true)
                {
                    LoadKategori();
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var kategori = btn?.DataContext as KategoriKamar;

            if (kategori != null)
            {
                var result = MessageBox.Show(
                    $"Apakah yakin mau hapus kategori '{kategori.Nama}'?",
                    "Konfirmasi Hapus",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        repo.DeleteKategori(kategori.Id); // panggil repository
                        LoadKategori(); // refresh data
                        MessageBox.Show($"Kategori '{kategori.Nama}' berhasil dihapus!",
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

    public class KategoriKamar
    {
        public int No { get; set; }
        public int Id { get; set; }
        public string Nama { get; set; } = "";
        public int Kapasitas { get; set; }
        public decimal Harga { get; set; }
    }
}
