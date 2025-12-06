-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Generation Time: Dec 06, 2025 at 03:14 AM
-- Server version: 8.0.30
-- PHP Version: 8.3.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `booking-hotel`
--

-- --------------------------------------------------------

--
-- Table structure for table `about_hsses`
--

CREATE TABLE `about_hsses` (
  `id` bigint UNSIGNED NOT NULL,
  `nama_hotel` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `foto` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
  `alamat` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `no_telp` varchar(20) COLLATE utf8mb4_unicode_ci NOT NULL,
  `email` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `kelas` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `deskripsi` text CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `about_hsses`
--

INSERT INTO `about_hsses` (`id`, `nama_hotel`, `foto`, `alamat`, `no_telp`, `email`, `kelas`, `deskripsi`, `created_at`, `updated_at`) VALUES
(1, 'Hotel Smart System', 'https://images.unsplash.com/photo-1625244724120-1fd1d34d00f6?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&ixid=M3w3Nzg4Nzd8MHwxfHNlYXJjaHwxfHxsdXh1cnklMjBob3RlbCUyMGxvYmJ5JTIwZWxlZ2FudHxlbnwxfHx8fDE3NTgxNjk3NTh8MA&ixlib=rb-4.1.0&q=80&w=1080&utm_source=figma&utm_medium=referral', 'Alamat,Jl. Merdeka No.45,Jakarta', '62 21 1234 5678', 'info@hss.com', '★★★★★', 'Hotel Smart System menyediakan fasilitas seperti kolam renang, spa, restoran, dan ruang meeting. Kami berkomitmen memberikan pengalaman menginap yang tak terlupakan bagi setiap tamu.', '2025-10-03 16:16:10', '2025-10-03 16:16:10');

-- --------------------------------------------------------

--
-- Table structure for table `cache`
--

CREATE TABLE `cache` (
  `key` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `value` mediumtext COLLATE utf8mb4_unicode_ci NOT NULL,
  `expiration` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `cache_locks`
--

CREATE TABLE `cache_locks` (
  `key` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `owner` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `expiration` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `failed_jobs`
--

CREATE TABLE `failed_jobs` (
  `id` bigint UNSIGNED NOT NULL,
  `uuid` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `connection` text COLLATE utf8mb4_unicode_ci NOT NULL,
  `queue` text COLLATE utf8mb4_unicode_ci NOT NULL,
  `payload` longtext COLLATE utf8mb4_unicode_ci NOT NULL,
  `exception` longtext COLLATE utf8mb4_unicode_ci NOT NULL,
  `failed_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `jobs`
--

CREATE TABLE `jobs` (
  `id` bigint UNSIGNED NOT NULL,
  `queue` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `payload` longtext COLLATE utf8mb4_unicode_ci NOT NULL,
  `attempts` tinyint UNSIGNED NOT NULL,
  `reserved_at` int UNSIGNED DEFAULT NULL,
  `available_at` int UNSIGNED NOT NULL,
  `created_at` int UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `job_batches`
--

CREATE TABLE `job_batches` (
  `id` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `name` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `total_jobs` int NOT NULL,
  `pending_jobs` int NOT NULL,
  `failed_jobs` int NOT NULL,
  `failed_job_ids` longtext COLLATE utf8mb4_unicode_ci NOT NULL,
  `options` mediumtext COLLATE utf8mb4_unicode_ci,
  `cancelled_at` int DEFAULT NULL,
  `created_at` int NOT NULL,
  `finished_at` int DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `kamars`
--

CREATE TABLE `kamars` (
  `id` bigint UNSIGNED NOT NULL,
  `nama_kamar` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `kategori_id` bigint UNSIGNED NOT NULL,
  `foto_kamar` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `status` enum('terisi','kosong') COLLATE utf8mb4_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `kamars`
--

INSERT INTO `kamars` (`id`, `nama_kamar`, `kategori_id`, `foto_kamar`, `status`, `created_at`, `updated_at`) VALUES
(2, 'Deluxe Room', 1, 'https://images.unsplash.com/photo-1611892440504-42a792e24d32?w=400&h=300&fit=crop', 'terisi', '2025-10-02 21:31:17', '2025-10-11 20:45:40'),
(3, 'Suite Room', 1, 'https://images.unsplash.com/photo-1582719478250-c89cae4dc85b?w=400&h=300&fit=crop', 'terisi', '2025-10-02 21:26:27', '2025-10-16 19:58:57'),
(4, 'Standard Room', 2, 'https://images.unsplash.com/photo-1586023492125-27b2c045efd7?w=400&h=300&fit=crop', 'kosong', '2025-10-02 21:32:21', '2025-10-15 21:39:17');

-- --------------------------------------------------------

--
-- Table structure for table `kategori_kamars`
--

CREATE TABLE `kategori_kamars` (
  `id` bigint UNSIGNED NOT NULL,
  `nama` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `kapasitas` int NOT NULL,
  `Harga` int NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `kategori_kamars`
--

INSERT INTO `kategori_kamars` (`id`, `nama`, `kapasitas`, `Harga`, `created_at`, `updated_at`) VALUES
(1, 'deluxe', 4, 100000, NULL, NULL),
(2, 'Suite Room', 2, 200000, NULL, NULL),
(3, 'Standard Room', 5, 500000, NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `makanan_minumans`
--

CREATE TABLE `makanan_minumans` (
  `id` bigint UNSIGNED NOT NULL,
  `kategori` enum('makanan','minuman') COLLATE utf8mb4_unicode_ci NOT NULL,
  `foto` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `nama` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `harga` int NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `makanan_minumans`
--

INSERT INTO `makanan_minumans` (`id`, `kategori`, `foto`, `nama`, `harga`, `created_at`, `updated_at`) VALUES
(1, 'minuman', 'https://d1vbn70lmn1nqe.cloudfront.net/prod/wp-content/uploads/2024/07/16074631/Daftar-Makanan-Enak-Asal-Indonesia-yang-Mendunia.jpg', 'Nasi Goreng', 100000, NULL, NULL),
(2, 'makanan', 'https://www.teakpalace.com/image/catalog/artikel/gambar-makanan-paling-enak-sate-kambing.jpg', 'Sate', 150000, NULL, NULL),
(3, 'minuman', 'https://asset.kompas.com/crops/PdRBq63giN_h8a7s3jecy1NmMCc=/0x0:780x520/1200x800/data/photo/2020/11/19/5fb5de3b29211.jpg', 'Kopi', 90000, NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `mejas`
--

CREATE TABLE `mejas` (
  `id` bigint UNSIGNED NOT NULL,
  `nama_meja` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `kapasitas` int NOT NULL DEFAULT '1',
  `status` enum('kosong','terisi','dipesan') COLLATE utf8mb4_unicode_ci NOT NULL DEFAULT 'kosong',
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `mejas`
--

INSERT INTO `mejas` (`id`, `nama_meja`, `kapasitas`, `status`, `created_at`, `updated_at`) VALUES
(1, 'Meja 1', 4, 'dipesan', '2025-10-03 16:36:23', '2025-10-16 20:01:20'),
(2, 'meja 2', 5, 'dipesan', '2025-10-03 16:38:01', '2025-10-16 20:00:04'),
(3, 'Meja 3', 5, 'dipesan', '2025-10-03 16:38:01', '2025-10-11 19:25:33');

-- --------------------------------------------------------

--
-- Table structure for table `migrations`
--

CREATE TABLE `migrations` (
  `id` int UNSIGNED NOT NULL,
  `migration` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `batch` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `migrations`
--

INSERT INTO `migrations` (`id`, `migration`, `batch`) VALUES
(1, '0001_01_01_000000_create_users_table', 1),
(2, '0001_01_01_000001_create_cache_table', 1),
(3, '0001_01_01_000002_create_jobs_table', 1),
(4, '2025_07_23_140808_create_tests_table', 1),
(5, '2025_08_01_062529_create_personal_access_tokens_table', 1),
(6, '2025_09_09_040810_create_kategori_kamars_table', 1),
(7, '2025_09_09_040812_create_kamars_table', 1),
(8, '2025_09_09_053834_create_tamus_table', 1),
(9, '2025_09_09_072822_create_riwayat_kamars_table', 1),
(10, '2025_09_11_014735_create_mejas_table', 1),
(11, '2025_09_11_023832_create_makanan_minumen_table', 1),
(12, '2025_09_11_023915_create_temp_makanan_minumen_table', 1),
(13, '2025_09_11_024033_create_riwayat_pesanan_makanan_minumen_table', 1),
(14, '2025_09_11_025321_create_layanan_resepsionis_table', 1),
(15, '2025_09_11_025455_create_riwayat_layanan_resepsionis_table', 1),
(16, '2025_09_11_025534_create_about_hsses_table', 1),
(17, '2025_10_03_141146_create_tagihans_table', 2),
(18, '2025_10_10_015008_riwayat_pesan_makanan_minuman', 3);

-- --------------------------------------------------------

--
-- Table structure for table `password_reset_tokens`
--

CREATE TABLE `password_reset_tokens` (
  `email` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `token` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `personal_access_tokens`
--

CREATE TABLE `personal_access_tokens` (
  `id` bigint UNSIGNED NOT NULL,
  `tokenable_type` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `tokenable_id` bigint UNSIGNED NOT NULL,
  `name` text COLLATE utf8mb4_unicode_ci NOT NULL,
  `token` varchar(64) COLLATE utf8mb4_unicode_ci NOT NULL,
  `abilities` text COLLATE utf8mb4_unicode_ci,
  `last_used_at` timestamp NULL DEFAULT NULL,
  `expires_at` timestamp NULL DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `personal_access_tokens`
--

INSERT INTO `personal_access_tokens` (`id`, `tokenable_type`, `tokenable_id`, `name`, `token`, `abilities`, `last_used_at`, `expires_at`, `created_at`, `updated_at`) VALUES
(1, 'App\\Models\\User', 3, 'flutter-token', '745eb3876371dcca5242a09cad3e6a4eae349c0d4d27e585459d87ba0611ff52', '[\"*\"]', NULL, NULL, '2025-10-03 09:07:55', '2025-10-03 09:07:55'),
(2, 'App\\Models\\User', 4, 'flutter-token', '1a3a038dde2f51135e06f56e8254773fa5b0ff91d9ebc4351a484ba44dc79ab9', '[\"*\"]', NULL, NULL, '2025-10-11 17:13:48', '2025-10-11 17:13:48'),
(3, 'App\\Models\\User', 4, 'flutter-token', 'd2b7118aac54f0a5126c6f220755268ba457a93b0da5517c76fa1cc51c1e131a', '[\"*\"]', NULL, NULL, '2025-10-11 17:23:12', '2025-10-11 17:23:12'),
(4, 'App\\Models\\User', 4, 'flutter-token', 'e2640a7b86cb03fd8109f436b63aacad0b267fc98b427ae04add7109781a4d04', '[\"*\"]', NULL, NULL, '2025-10-11 18:54:43', '2025-10-11 18:54:43'),
(5, 'App\\Models\\User', 4, 'flutter-token', '186dde7eafec1f8cfac79326e3991ffaff65a53556efc1f43713246d7ee56076', '[\"*\"]', NULL, NULL, '2025-10-11 19:10:23', '2025-10-11 19:10:23'),
(6, 'App\\Models\\User', 4, 'flutter-token', '48dd0fdcd4c50b4fca7c8f9578b43635a649a74c56afa859827b5df6f9d4c852', '[\"*\"]', NULL, NULL, '2025-10-11 19:44:52', '2025-10-11 19:44:52'),
(7, 'App\\Models\\User', 4, 'flutter-token', '88ebd8586d3c264ef74bdc0806427f2368c7822fc5cae45f8884c39074e73041', '[\"*\"]', NULL, NULL, '2025-10-11 20:17:52', '2025-10-11 20:17:52'),
(8, 'App\\Models\\User', 4, 'flutter-token', '3de20ef969d572bc34456626ebe812ab33c8f2649377ed0d1efd220ff5334b0e', '[\"*\"]', NULL, NULL, '2025-10-11 20:20:51', '2025-10-11 20:20:51'),
(9, 'App\\Models\\User', 4, 'flutter-token', '25412529b2fd772b88ee415449dca11e5a4b3655ae64fbf95ca33ade70a9a79a', '[\"*\"]', NULL, NULL, '2025-10-11 20:33:41', '2025-10-11 20:33:41'),
(10, 'App\\Models\\User', 4, 'flutter-token', 'f2238cec0c8c28dbded77d7d333d418d4e29de995a40c8e1244337687eab3322', '[\"*\"]', NULL, NULL, '2025-10-11 21:38:07', '2025-10-11 21:38:07'),
(11, 'App\\Models\\User', 4, 'flutter-token', '55300a2a5daaa09d4611163e75998a81a352c05f6b339333b905112400076b17', '[\"*\"]', NULL, NULL, '2025-10-11 21:42:21', '2025-10-11 21:42:21'),
(12, 'App\\Models\\User', 4, 'flutter-token', '503477cd41d89da0fd624519230caa6e90c4a92141fbd8cf5122228cf8dbb9ad', '[\"*\"]', NULL, NULL, '2025-10-11 21:49:42', '2025-10-11 21:49:42'),
(13, 'App\\Models\\User', 4, 'flutter-token', '12a7a3445a31f820e5b8295102427b6fb6ae512edd3e0920d21aad3b0584ef9f', '[\"*\"]', NULL, NULL, '2025-10-11 21:54:29', '2025-10-11 21:54:29'),
(14, 'App\\Models\\User', 5, 'flutter-token', 'd0137cbc694a6f717d4b32b304e2324ff43537aa7eec8242d7f1e6aa34d60cc8', '[\"*\"]', NULL, NULL, '2025-10-11 21:55:48', '2025-10-11 21:55:48'),
(15, 'App\\Models\\User', 4, 'flutter-token', '35fc231a928bfecd50c191cea3d14c087082c14ec1f99ab3085220e7c05ca9ba', '[\"*\"]', NULL, NULL, '2025-10-15 21:37:02', '2025-10-15 21:37:02'),
(16, 'App\\Models\\User', 8, 'flutter-token', '18065fb2f2aa031f60ac2bb4a3eed0867d56e3341a881cb78e3094029160590b', '[\"*\"]', NULL, NULL, '2025-10-15 21:40:44', '2025-10-15 21:40:44'),
(17, 'App\\Models\\User', 4, 'flutter-token', 'de353fac7fcdc7e57701303096a5fa3a461bbbce95340f08c53e7c94e17b0202', '[\"*\"]', NULL, NULL, '2025-10-16 19:49:08', '2025-10-16 19:49:08'),
(18, 'App\\Models\\User', 10, 'flutter-token', '3cac8bb62437b6acecd95130bb56323809981bee6e982204772c074cf1d5e5f5', '[\"*\"]', NULL, NULL, '2025-10-16 19:57:38', '2025-10-16 19:57:38'),
(19, 'App\\Models\\User', 4, 'flutter-token', 'd57c7480fdac0015acb97f589b958d71a052635808b2e07eff1f0840f5acd88a', '[\"*\"]', NULL, NULL, '2025-12-05 19:56:28', '2025-12-05 19:56:28');

-- --------------------------------------------------------

--
-- Table structure for table `riwayat_kamars`
--

CREATE TABLE `riwayat_kamars` (
  `id` bigint UNSIGNED NOT NULL,
  `kamar_id` bigint UNSIGNED NOT NULL,
  `tamu_id` bigint UNSIGNED NOT NULL,
  `catatan_khusus` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `check_in` date DEFAULT NULL,
  `check_out` date DEFAULT NULL,
  `status` enum('ditempati','selesai','dibatalkan') COLLATE utf8mb4_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `riwayat_kamars`
--

INSERT INTO `riwayat_kamars` (`id`, `kamar_id`, `tamu_id`, `catatan_khusus`, `check_in`, `check_out`, `status`, `created_at`, `updated_at`) VALUES
(1, 2, 1, 'aa', '2025-10-07', '2025-10-07', 'ditempati', '2025-10-03 07:00:38', '2025-10-03 07:00:38'),
(3, 3, 3, 'tambah sesuatu', '2025-10-03', '2025-10-04', 'ditempati', '2025-10-03 07:46:02', '2025-10-03 07:46:02'),
(4, 3, 4, 'tambah sesuatu', '2025-10-03', '2025-10-04', 'ditempati', '2025-10-03 07:47:00', '2025-10-03 07:47:00'),
(5, 3, 5, 'no', '2025-10-03', '2025-10-04', 'ditempati', '2025-10-03 07:51:22', '2025-10-03 07:51:22'),
(8, 2, 8, 'no no', '2025-10-03', '2025-10-08', 'ditempati', '2025-10-03 08:01:18', '2025-10-03 08:01:18'),
(9, 4, 11, 'kamar nyaman', '2025-10-04', '2025-10-06', 'ditempati', '2025-10-03 10:10:54', '2025-10-03 10:10:54'),
(10, 3, 13, 'tidak', '2025-10-12', '2025-10-12', 'ditempati', '2025-10-11 17:26:20', '2025-10-11 17:26:20'),
(11, 4, 14, 'tambal tvnya', '2025-10-12', '2025-10-14', 'ditempati', '2025-10-11 19:29:32', '2025-10-11 19:29:32'),
(12, 3, 12, 'gsschu', '2025-10-12', '2025-10-30', 'selesai', '2025-10-11 20:43:17', '2025-10-11 20:43:17'),
(13, 2, 12, 'ac extra', '2025-10-12', '2025-10-31', 'selesai', '2025-10-11 20:45:40', '2025-10-11 20:45:40'),
(14, 4, 12, 'kamar lantai 5, tambah bed', '2025-10-16', '2025-10-18', 'ditempati', '2025-10-15 21:39:17', '2025-10-15 21:39:17'),
(15, 3, 16, 'tambah bed', '2025-10-16', '2025-10-18', 'selesai', '2025-10-15 21:45:34', '2025-10-15 21:45:34'),
(16, 3, 18, 'tambah selimut', '2025-10-17', '2025-10-30', 'ditempati', '2025-10-16 19:58:57', '2025-10-16 19:58:57');

-- --------------------------------------------------------

--
-- Table structure for table `riwayat_pesanan_makanan_minuman`
--

CREATE TABLE `riwayat_pesanan_makanan_minuman` (
  `id` bigint UNSIGNED NOT NULL,
  `nama_tamu_id` bigint UNSIGNED NOT NULL,
  `makanan_minuman_id` bigint UNSIGNED NOT NULL,
  `tanggal` date DEFAULT NULL,
  `jam_makan` time DEFAULT NULL,
  `meja_id` bigint UNSIGNED NOT NULL,
  `jumlah_tamu` int NOT NULL DEFAULT '1',
  `jumlah_dipesan` int DEFAULT NULL,
  `total_harga` int DEFAULT NULL,
  `biaya_layanan` int DEFAULT NULL,
  `status` enum('dikirim','diproses','dibatalkan') COLLATE utf8mb4_unicode_ci NOT NULL DEFAULT 'diproses',
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `riwayat_pesanan_makanan_minuman`
--

INSERT INTO `riwayat_pesanan_makanan_minuman` (`id`, `nama_tamu_id`, `makanan_minuman_id`, `tanggal`, `jam_makan`, `meja_id`, `jumlah_tamu`, `jumlah_dipesan`, `total_harga`, `biaya_layanan`, `status`, `created_at`, `updated_at`) VALUES
(1, 2, 2, '2025-10-10', '02:10:00', 3, 2, 1, 165000, 15000, 'diproses', '2025-10-09 23:19:00', '2025-10-09 23:19:00'),
(2, 1, 3, '2025-10-10', '02:10:00', 3, 2, 1, 105000, 15000, 'diproses', '2025-10-09 23:19:02', '2025-10-09 23:19:02'),
(3, 1, 1, '2025-10-10', '08:24:00', 2, 7, 2, 215000, 15000, 'diproses', '2025-10-09 23:25:07', '2025-10-09 23:25:07'),
(4, 1, 3, '2025-10-10', '13:26:00', 1, 3, 1, 105000, 15000, 'diproses', '2025-10-09 23:26:34', '2025-10-09 23:26:34'),
(5, 1, 1, '2025-10-10', '13:29:00', 3, 5, 3, 315000, 15000, 'diproses', '2025-10-09 23:29:41', '2025-10-09 23:29:41'),
(6, 2, 1, '2025-10-10', '13:29:00', 3, 8, 3, 315000, 15000, 'diproses', '2025-10-09 23:31:30', '2025-10-09 23:31:30'),
(7, 1, 2, '2025-10-10', '13:31:00', 2, 10, 1, 165000, 15000, 'diproses', '2025-10-09 23:32:06', '2025-10-09 23:32:06'),
(8, 12, 1, '2025-10-12', '09:10:00', 2, 3, 1, 115000, 15000, 'diproses', '2025-10-11 19:10:50', '2025-10-11 19:10:50'),
(9, 12, 1, '2025-10-12', '09:25:00', 3, 7, 2, 215000, 15000, 'diproses', '2025-10-11 19:25:32', '2025-10-11 19:25:32'),
(10, 12, 3, '2025-10-12', '09:25:00', 3, 7, 1, 105000, 15000, 'dikirim', '2025-10-11 19:25:33', '2025-10-11 19:25:33'),
(11, 12, 1, '2025-10-12', '10:59:00', 2, 4, 1, 115000, 15000, 'dikirim', '2025-10-11 20:59:48', '2025-10-11 20:59:48'),
(12, 18, 1, '2025-10-17', '10:55:00', 2, 4, 1, 115000, 15000, 'diproses', '2025-10-16 20:00:03', '2025-10-16 20:00:03'),
(13, 18, 3, '2025-10-17', '10:55:00', 2, 4, 1, 105000, 15000, 'diproses', '2025-10-16 20:00:04', '2025-10-16 20:00:04'),
(14, 18, 2, '2025-10-17', '10:01:00', 1, 4, 1, 165000, 15000, 'diproses', '2025-10-16 20:01:20', '2025-10-16 20:01:20');

-- --------------------------------------------------------

--
-- Table structure for table `sessions`
--

CREATE TABLE `sessions` (
  `id` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `user_id` bigint UNSIGNED DEFAULT NULL,
  `ip_address` varchar(45) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `user_agent` text COLLATE utf8mb4_unicode_ci,
  `payload` longtext COLLATE utf8mb4_unicode_ci NOT NULL,
  `last_activity` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `sessions`
--

INSERT INTO `sessions` (`id`, `user_id`, `ip_address`, `user_agent`, `payload`, `last_activity`) VALUES
('GxA3yPNnljx9COaOwpeZghuoQlu9ZdUcfrqqF5qG', NULL, '192.168.255.202', 'PostmanRuntime/7.45.0', 'YTozOntzOjY6Il90b2tlbiI7czo0MDoiSjZNeHRPUGFZNm1QbldIYVpvSlNXYmJ4TElyNkt5OFNNUzlVS3JhbiI7czo5OiJfcHJldmlvdXMiO2E6MTp7czozOiJ1cmwiO3M6Mjc6Imh0dHA6Ly8xOTIuMTY4LjI1NS4yMDI6ODAwMCI7fXM6NjoiX2ZsYXNoIjthOjI6e3M6Mzoib2xkIjthOjA6e31zOjM6Im5ldyI7YTowOnt9fX0=', 1759499865);

-- --------------------------------------------------------

--
-- Table structure for table `tagihan_kamars`
--

CREATE TABLE `tagihan_kamars` (
  `id` bigint UNSIGNED NOT NULL,
  `riwayat_kamar_id` bigint UNSIGNED NOT NULL,
  `total_harga` decimal(12,2) NOT NULL,
  `status_pembayaran` enum('belum_dibayar','dibayar') COLLATE utf8mb4_unicode_ci NOT NULL DEFAULT 'belum_dibayar',
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `tagihan_kamars`
--

INSERT INTO `tagihan_kamars` (`id`, `riwayat_kamar_id`, `total_harga`, `status_pembayaran`, `created_at`, `updated_at`) VALUES
(1, 4, '0.00', 'belum_dibayar', '2025-10-03 07:47:00', '2025-10-03 07:47:00'),
(2, 5, '0.00', 'belum_dibayar', '2025-10-03 07:51:22', '2025-10-03 07:51:22'),
(5, 8, '500000.00', 'belum_dibayar', '2025-10-03 08:01:18', '2025-10-03 08:01:18'),
(6, 9, '400000.00', 'belum_dibayar', '2025-10-03 10:10:54', '2025-10-03 10:10:54'),
(7, 10, '100000.00', 'belum_dibayar', '2025-10-11 17:26:21', '2025-10-11 17:26:21'),
(8, 11, '400000.00', 'belum_dibayar', '2025-10-11 19:29:32', '2025-10-11 19:29:32'),
(9, 12, '1800000.00', 'belum_dibayar', '2025-10-11 20:43:17', '2025-10-11 20:43:17'),
(10, 13, '1900000.00', 'belum_dibayar', '2025-10-11 20:45:40', '2025-10-11 20:45:40'),
(11, 14, '400000.00', 'belum_dibayar', '2025-10-15 21:39:17', '2025-10-15 21:39:17'),
(12, 15, '200000.00', 'belum_dibayar', '2025-10-15 21:45:34', '2025-10-15 21:45:34'),
(13, 16, '1300000.00', 'belum_dibayar', '2025-10-16 19:58:57', '2025-10-16 19:58:57');

-- --------------------------------------------------------

--
-- Table structure for table `tagihan_makanans`
--

CREATE TABLE `tagihan_makanans` (
  `id` bigint UNSIGNED NOT NULL,
  `tamu_id` bigint UNSIGNED NOT NULL,
  `total_harga` decimal(12,2) NOT NULL,
  `status_pembayaran` enum('belum_dibayar','dibayar') COLLATE utf8mb4_unicode_ci NOT NULL DEFAULT 'belum_dibayar',
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `tamus`
--

CREATE TABLE `tamus` (
  `id` bigint UNSIGNED NOT NULL,
  `user_id` bigint UNSIGNED NOT NULL,
  `nama_tamu` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `jumlah_tamu` int DEFAULT NULL,
  `no_telp` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `tamus`
--

INSERT INTO `tamus` (`id`, `user_id`, `nama_tamu`, `jumlah_tamu`, `no_telp`, `created_at`, `updated_at`) VALUES
(1, 1, 'dd', 34, '0987654321', '2025-10-03 07:00:38', '2025-10-03 07:00:38'),
(2, 1, NULL, 4, '0987654321', '2025-10-03 07:01:57', '2025-10-03 07:01:57'),
(3, 1, NULL, 3, '09876543221', '2025-10-03 07:46:02', '2025-10-03 07:46:02'),
(4, 1, NULL, 3, '09876543221', '2025-10-03 07:47:00', '2025-10-03 07:47:00'),
(5, 1, NULL, 1, '0123456789', '2025-10-03 07:51:22', '2025-10-03 07:51:22'),
(6, 1, NULL, 3, '0978563423', '2025-10-03 07:57:44', '2025-10-03 07:57:44'),
(7, 1, NULL, 4, '0978563423', '2025-10-03 07:58:46', '2025-10-03 07:58:46'),
(8, 1, NULL, 4, '0978563423', '2025-10-03 08:01:18', '2025-10-03 08:01:18'),
(10, 3, 'dd', 2, '0987654321', '2025-10-03 09:07:25', '2025-10-11 03:01:09'),
(11, 3, NULL, 2, '0284649294', '2025-10-03 10:10:54', '2025-10-03 10:10:54'),
(12, 4, 'Xpander', 2, '089745213', '2025-10-11 17:13:10', '2025-10-15 21:39:17'),
(13, 4, NULL, 2, '089745213', '2025-10-11 17:26:20', '2025-10-15 21:39:17'),
(14, 4, NULL, 2, '089745213', '2025-10-11 19:29:32', '2025-10-15 21:39:17'),
(16, 8, 'oni', 4, '089745562', '2025-10-15 21:40:20', '2025-10-15 21:45:34'),
(17, 9, 'raffi24', NULL, NULL, '2025-10-16 19:56:30', '2025-10-16 19:56:30'),
(18, 10, 'raffi', 4, '08965412360', '2025-10-16 19:57:17', '2025-10-16 19:58:57');

-- --------------------------------------------------------

--
-- Table structure for table `temp_makanan_minuman`
--

CREATE TABLE `temp_makanan_minuman` (
  `id` bigint UNSIGNED NOT NULL,
  `makanan_minuman_id` bigint UNSIGNED NOT NULL,
  `harga` decimal(10,2) NOT NULL,
  `jumlah` int NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `tests`
--

CREATE TABLE `tests` (
  `id` bigint UNSIGNED NOT NULL,
  `judul` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `isi` text COLLATE utf8mb4_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `id` bigint UNSIGNED NOT NULL,
  `username` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `password` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `role` enum('admin','tamu') COLLATE utf8mb4_unicode_ci NOT NULL,
  `remember_token` varchar(100) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`id`, `username`, `password`, `role`, `remember_token`, `created_at`, `updated_at`) VALUES
(1, 'tamu', '123', 'tamu', 'fsbhs', NULL, NULL),
(3, 'saya', '$2y$12$j1bWSyPwiTzY1/dcnzRfOuG9gmugPi8fxtx..LDvyFuftB.qkYVLW', 'tamu', NULL, '2025-10-03 09:07:25', '2025-10-11 03:01:09'),
(4, 'xpander', '$2y$12$X0OlLX8aEO7khwgguthtUuZwtVCGkYp29yMJ8eWp.o3V/TsoliSUi', 'tamu', NULL, '2025-10-11 17:13:10', '2025-10-11 21:39:28'),
(5, 'xenia', '$2y$12$4VCnCJzaRdQx2FD.6vTgmum4p9L2WMcKjO.KnVe8Puzvo5Q8CXdPi', 'tamu', NULL, '2025-10-11 21:55:29', '2025-10-11 21:55:29'),
(7, 'admin', '123', 'admin', NULL, NULL, NULL),
(8, 'sheani', '$2y$12$6FG4hMhlqo2QIKuiIcY44ewzgFhDJdraUiduCimWXyKkrxky48/6S', 'tamu', NULL, '2025-10-15 21:40:20', '2025-10-15 21:40:20'),
(9, 'raffi25', '$2y$12$N1SR6p0UU1zinXcRZYDBiuXET1VBQEjy34R7dkVV9Eabz2VCNJ8IC', 'tamu', NULL, '2025-10-16 19:56:30', '2025-10-16 19:56:30'),
(10, 'raffii', '$2y$12$yLwvDm5mny/unCitZjyX7OTY1UORLXXJFj8R9DcB0q1.Ex.12ilsS', 'tamu', NULL, '2025-10-16 19:57:17', '2025-10-16 19:57:17');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `about_hsses`
--
ALTER TABLE `about_hsses`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `cache`
--
ALTER TABLE `cache`
  ADD PRIMARY KEY (`key`);

--
-- Indexes for table `cache_locks`
--
ALTER TABLE `cache_locks`
  ADD PRIMARY KEY (`key`);

--
-- Indexes for table `failed_jobs`
--
ALTER TABLE `failed_jobs`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `failed_jobs_uuid_unique` (`uuid`);

--
-- Indexes for table `jobs`
--
ALTER TABLE `jobs`
  ADD PRIMARY KEY (`id`),
  ADD KEY `jobs_queue_index` (`queue`);

--
-- Indexes for table `job_batches`
--
ALTER TABLE `job_batches`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `kamars`
--
ALTER TABLE `kamars`
  ADD PRIMARY KEY (`id`),
  ADD KEY `kamars_kategori_id_foreign` (`kategori_id`);

--
-- Indexes for table `kategori_kamars`
--
ALTER TABLE `kategori_kamars`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `makanan_minumans`
--
ALTER TABLE `makanan_minumans`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `mejas`
--
ALTER TABLE `mejas`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `migrations`
--
ALTER TABLE `migrations`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `password_reset_tokens`
--
ALTER TABLE `password_reset_tokens`
  ADD PRIMARY KEY (`email`);

--
-- Indexes for table `personal_access_tokens`
--
ALTER TABLE `personal_access_tokens`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `personal_access_tokens_token_unique` (`token`),
  ADD KEY `personal_access_tokens_tokenable_type_tokenable_id_index` (`tokenable_type`,`tokenable_id`),
  ADD KEY `personal_access_tokens_expires_at_index` (`expires_at`);

--
-- Indexes for table `riwayat_kamars`
--
ALTER TABLE `riwayat_kamars`
  ADD PRIMARY KEY (`id`),
  ADD KEY `riwayat_kamars_kamar_id_foreign` (`kamar_id`),
  ADD KEY `riwayat_kamars_tamu_id_foreign` (`tamu_id`);

--
-- Indexes for table `riwayat_pesanan_makanan_minuman`
--
ALTER TABLE `riwayat_pesanan_makanan_minuman`
  ADD PRIMARY KEY (`id`),
  ADD KEY `riwayat_pesanan_makanan_minuman_nama_tamu_id_foreign` (`nama_tamu_id`),
  ADD KEY `riwayat_pesanan_makanan_minuman_makanan_minuman_id_foreign` (`makanan_minuman_id`),
  ADD KEY `riwayat_pesanan_makanan_minuman_meja_id_foreign` (`meja_id`);

--
-- Indexes for table `sessions`
--
ALTER TABLE `sessions`
  ADD PRIMARY KEY (`id`),
  ADD KEY `sessions_user_id_index` (`user_id`),
  ADD KEY `sessions_last_activity_index` (`last_activity`);

--
-- Indexes for table `tagihan_kamars`
--
ALTER TABLE `tagihan_kamars`
  ADD PRIMARY KEY (`id`),
  ADD KEY `tagihan_kamars_riwayat_kamar_id_foreign` (`riwayat_kamar_id`);

--
-- Indexes for table `tagihan_makanans`
--
ALTER TABLE `tagihan_makanans`
  ADD PRIMARY KEY (`id`),
  ADD KEY `tagihan_makanans_tamu_id_foreign` (`tamu_id`);

--
-- Indexes for table `tamus`
--
ALTER TABLE `tamus`
  ADD PRIMARY KEY (`id`),
  ADD KEY `tamus_user_id_foreign` (`user_id`);

--
-- Indexes for table `temp_makanan_minuman`
--
ALTER TABLE `temp_makanan_minuman`
  ADD PRIMARY KEY (`id`),
  ADD KEY `temp_makanan_minuman_makanan_minuman_id_foreign` (`makanan_minuman_id`);

--
-- Indexes for table `tests`
--
ALTER TABLE `tests`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `users_username_unique` (`username`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `about_hsses`
--
ALTER TABLE `about_hsses`
  MODIFY `id` bigint UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `failed_jobs`
--
ALTER TABLE `failed_jobs`
  MODIFY `id` bigint UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `jobs`
--
ALTER TABLE `jobs`
  MODIFY `id` bigint UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `kamars`
--
ALTER TABLE `kamars`
  MODIFY `id` bigint UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `kategori_kamars`
--
ALTER TABLE `kategori_kamars`
  MODIFY `id` bigint UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `makanan_minumans`
--
ALTER TABLE `makanan_minumans`
  MODIFY `id` bigint UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `mejas`
--
ALTER TABLE `mejas`
  MODIFY `id` bigint UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `migrations`
--
ALTER TABLE `migrations`
  MODIFY `id` int UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;

--
-- AUTO_INCREMENT for table `personal_access_tokens`
--
ALTER TABLE `personal_access_tokens`
  MODIFY `id` bigint UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;

--
-- AUTO_INCREMENT for table `riwayat_kamars`
--
ALTER TABLE `riwayat_kamars`
  MODIFY `id` bigint UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

--
-- AUTO_INCREMENT for table `riwayat_pesanan_makanan_minuman`
--
ALTER TABLE `riwayat_pesanan_makanan_minuman`
  MODIFY `id` bigint UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT for table `tagihan_kamars`
--
ALTER TABLE `tagihan_kamars`
  MODIFY `id` bigint UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT for table `tagihan_makanans`
--
ALTER TABLE `tagihan_makanans`
  MODIFY `id` bigint UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `tamus`
--
ALTER TABLE `tamus`
  MODIFY `id` bigint UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;

--
-- AUTO_INCREMENT for table `temp_makanan_minuman`
--
ALTER TABLE `temp_makanan_minuman`
  MODIFY `id` bigint UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `tests`
--
ALTER TABLE `tests`
  MODIFY `id` bigint UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `id` bigint UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `kamars`
--
ALTER TABLE `kamars`
  ADD CONSTRAINT `kamars_kategori_id_foreign` FOREIGN KEY (`kategori_id`) REFERENCES `kategori_kamars` (`id`);

--
-- Constraints for table `riwayat_kamars`
--
ALTER TABLE `riwayat_kamars`
  ADD CONSTRAINT `riwayat_kamars_kamar_id_foreign` FOREIGN KEY (`kamar_id`) REFERENCES `kamars` (`id`),
  ADD CONSTRAINT `riwayat_kamars_tamu_id_foreign` FOREIGN KEY (`tamu_id`) REFERENCES `tamus` (`id`);

--
-- Constraints for table `riwayat_pesanan_makanan_minuman`
--
ALTER TABLE `riwayat_pesanan_makanan_minuman`
  ADD CONSTRAINT `riwayat_pesanan_makanan_minuman_makanan_minuman_id_foreign` FOREIGN KEY (`makanan_minuman_id`) REFERENCES `makanan_minumans` (`id`),
  ADD CONSTRAINT `riwayat_pesanan_makanan_minuman_meja_id_foreign` FOREIGN KEY (`meja_id`) REFERENCES `mejas` (`id`),
  ADD CONSTRAINT `riwayat_pesanan_makanan_minuman_nama_tamu_id_foreign` FOREIGN KEY (`nama_tamu_id`) REFERENCES `tamus` (`id`);

--
-- Constraints for table `tagihan_kamars`
--
ALTER TABLE `tagihan_kamars`
  ADD CONSTRAINT `tagihan_kamars_riwayat_kamar_id_foreign` FOREIGN KEY (`riwayat_kamar_id`) REFERENCES `riwayat_kamars` (`id`) ON DELETE CASCADE;

--
-- Constraints for table `tagihan_makanans`
--
ALTER TABLE `tagihan_makanans`
  ADD CONSTRAINT `tagihan_makanans_tamu_id_foreign` FOREIGN KEY (`tamu_id`) REFERENCES `tamus` (`id`) ON DELETE CASCADE;

--
-- Constraints for table `tamus`
--
ALTER TABLE `tamus`
  ADD CONSTRAINT `tamus_user_id_foreign` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE;

--
-- Constraints for table `temp_makanan_minuman`
--
ALTER TABLE `temp_makanan_minuman`
  ADD CONSTRAINT `temp_makanan_minuman_makanan_minuman_id_foreign` FOREIGN KEY (`makanan_minuman_id`) REFERENCES `makanan_minumans` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
