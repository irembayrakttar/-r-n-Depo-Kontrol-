using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ürün_depo_kontrolü_221041075
{

    class Program
    {
        static void Main()
        {
            DepoYonetimSistemi depo = new DepoYonetimSistemi();

            Console.WriteLine("Depo Görevlisi Kullanıcı Adı: ");
            string kullaniciAdi = Console.ReadLine();

            Console.WriteLine("Şifre: ");
            string sifre = MaskedInput();

            
            Console.WriteLine($"Giriş başarılı! Kullanıcı Adı: {kullaniciAdi}, Şifre: {sifre}");

            while (true)
            {
                Console.WriteLine("\nDepo Yönetim Sistemi");
                Console.WriteLine("1. Ürün Ekle");
                Console.WriteLine("2. Ürün Çıkar");
                Console.WriteLine("3. Stok Durumu Göster");
                Console.WriteLine("4. Çıkış");

                Console.Write("Seçiminizi yapın (1-4): ");
                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        UrunEkle(depo);
                        break;
                    case "2":
                        UrunCikar(depo);
                        break;
                    case "3":
                        depo.StokDurumuGoster();
                        break;
                    case "4":
                        Console.WriteLine("Programdan çıkılıyor...");
                        return;
                    default:
                        Console.WriteLine("Geçersiz seçenek! Lütfen tekrar deneyin.");
                        break;
                }
            }
        }

        static void UrunEkle(DepoYonetimSistemi depo)
        {
            Urun yeniUrun = new Urun();

            Console.Write("Ürün Adı: ");
            yeniUrun.UrunAdi = Console.ReadLine();

            Console.Write("Stok Miktarı: ");
            yeniUrun.StokMiktari = Convert.ToInt32(Console.ReadLine());

            depo.UrunEkle(yeniUrun);
        }

        static void UrunCikar(DepoYonetimSistemi depo)
        {
            Console.Write("Ürün ID: ");
            if (int.TryParse(Console.ReadLine(), out int urunID))
            {
                Console.Write("Çıkartılacak Miktar: ");
                if (int.TryParse(Console.ReadLine(), out int miktar))
                {
                    depo.UrunCikar(urunID, miktar);
                }
                else
                {
                    Console.WriteLine("Geçersiz miktar formatı!");
                }
            }
            else
            {
                Console.WriteLine("Geçersiz ürün ID formatı!");
            }
        }

        static string MaskedInput()
        {
            string sifre = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                
                if (key.Key == ConsoleKey.Backspace && sifre.Length > 0)
                {
                    sifre = sifre.Substring(0, (sifre.Length - 1));
                    Console.Write("\b \b");
                }
                
                else if (char.IsLetterOrDigit(key.KeyChar))
                {
                    sifre += key.KeyChar;
                    Console.Write("*");
                }
            }
            while (key.Key != ConsoleKey.Enter);

            Console.WriteLine(); 

            return sifre;
        }
    }

    class Urun
    {
        private static int nextUrunID = 1;

        public int UrunID { get; private set; }
        public string UrunAdi { get; set; }
        public int StokMiktari { get; set; }

        public Urun()
        {
            UrunID = nextUrunID++;
        }
    }

    class DepoYonetimSistemi
    {
        private List<Urun> urunler;

        public DepoYonetimSistemi()
        {
            urunler = new List<Urun>();
        }

        public void UrunEkle(Urun urun)
        {
            urunler.Add(urun);
            Console.WriteLine($"{urun.UrunAdi} (ID:{urun.UrunID}) depoya eklendi.");
        }

        public void UrunCikar(int urunID, int miktar)
        {
            Urun urun = urunler.Find(u => u.UrunID == urunID);

            if (urun != null)
            {
                if (urun.StokMiktari >= miktar)
                {
                    urun.StokMiktari -= miktar;
                    Console.WriteLine($"{miktar} adet {urun.UrunAdi} (ID:{urun.UrunID}) depodan çıkarıldı.");
                }
                else
                {
                    Console.WriteLine("Yetersiz stok miktarı!");
                }
            }
            else
            {
                Console.WriteLine("Ürün bulunamadı!");
            }
        }

        public void StokDurumuGoster()
        {
            Console.WriteLine("Depo Stok Durumu:");
            foreach (var urun in urunler)
            {
                Console.WriteLine($"{urun.UrunAdi} (ID:{urun.UrunID}): {urun.StokMiktari} adet");
                Console.ReadKey();
            }
        }
    }
}
// kullanıcı adı ve yıldızlı şifre girdikten sonra depo yönetimi sistemine giriş yapılır.
// Sistemde ürün adı girdikten sonra ürüne özel bir ID numarası atanır ve ürün çıkarma işlemini bu ID numarası üzerinden yapılır. 
