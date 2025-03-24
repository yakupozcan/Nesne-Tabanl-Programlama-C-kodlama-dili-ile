using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace nesne1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int secimNo = 0; //ilk index 0'dır, sayı arttıkça aşağıdaki seçnekleri seçilidir
            int secimNoEski = -1; //başlangıçta eski seçim yok, imkansız değer atandı
            MenuOlustur();

            void MenuOlustur() //menüyü ve davranışları bu metotun içinde
            {
                Console.Title = "NTP1"; //başlığı değiştirir
                Console.Clear(); //tertemiz ekranla işe başlayalım
                int consoleWidth, consoleHeight;
                consoleHeight = Console.BufferHeight; // konsola yazılabilecek karakter yüksekliğini alır
                consoleWidth = Console.BufferWidth;   //konsola yazılabilecek karakter genişliğini alır

                Console.CursorVisible = false; //seçim menüsündeyken işaretçinin görünürlüğünü kapatır

                // Menü seçeneklerinin yazıları buradan belirlenir
                string[] menuSecenekler = { " Konuma yazdırma ", //index 0
                                            " Dört İşlem ",      //index 1
                                            " SEÇENEK b ",
                                            " SEÇENEK c ",
                                            " SEÇENEK d ",
                                            " SEÇENEK e ",
                                            " SEÇENEK f ",      //index 6
                                            " SEÇENEK g ", };   //index 7


                //topMargin değişkeni yukardan kaç birim boşluk bıraklılacağını belirleyecek
                int topMargin = consoleHeight - menuSecenekler.Length;// seçenekler + menü yazısı eksiltilir
                if (topMargin % 2 == 1) { topMargin--; } //eğer 2ye tam bölünmüyorsa 1 eksilt
                topMargin = topMargin / 2; //kalan toplam boşluk 2 ye bölünür

                Console.CursorTop = topMargin; // işaretçinin yüksekliği hesaplamalara göre değişir

                Console.CursorLeft = (consoleWidth - 12) / 2; //başlık yazısının çıkarılıp 2 ye bölünür
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("SEÇİM MENÜSÜ"); // tam ortaya "SEÇİM MENÜSÜ" yazdır
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(); // 1 satır boşluk

                int[,] menuSecenekKonum = new int[menuSecenekler.Length, 2]; // hangi seçenek hangi konuma yazıldıysa buraya kaydedilecek



                for (int i = 0; i <= menuSecenekler.Length - 1; i++) //tüm seçenekleri yazdıran döngü
                {
                    int leftMargin = consoleWidth - menuSecenekler[i].Length;
                    if (leftMargin % 2 == 1) { leftMargin--; }
                    leftMargin = leftMargin / 2;  // buraya kadar yazının uzunluğuna göre ortalanma hesaplamaları yapıldı
                    Console.CursorLeft = leftMargin; // işaretçi yatay hizalama yapıldı
                    menuSecenekKonum[i, 0] = Console.CursorLeft; //yazılan seçeneğin yatay konumu i'ninci indexte kaydedildi
                    menuSecenekKonum[i, 1] = Console.CursorTop; // yazılan seçeneğin dikey konumu i'ninci indexte kaydedildi
                    Console.WriteLine(menuSecenekler[i]);  //i'ninci indexteki seçeneği yazdır
                }

                void secimDegistir() //yün tuşlarına basıldığında tetiklenir
                {
                    Console.CursorLeft = menuSecenekKonum[secimNo, 0];
                    Console.CursorTop = menuSecenekKonum[secimNo, 1]; //yeni seçimin konumuna git
                    Console.BackgroundColor = ConsoleColor.DarkGray; //yazı arkaplan rengini değiştir
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(menuSecenekler[secimNo]);    //seçimi tekrar yazdır
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;  //arkaplan rengini eski heline getir

                    if (secimNoEski != -1) //kodda verdiğimiz -1 değeri duruyorsa bir şey yapma
                    {                           // yeni seçimin arkapalnı değiştiği gibi eski seçimin arkaplanı normal hale gelir
                        Console.CursorLeft = menuSecenekKonum[secimNoEski, 0];
                        Console.CursorTop = menuSecenekKonum[secimNoEski, 1];
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.WriteLine(menuSecenekler[secimNoEski]);
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                }

                secimDegistir(); //program ilk çalıştığında ilk seçeneği vurgulayacak
                ConsoleKeyInfo tus; //basılan tuşu değişkende tut
                while (true)
                {
                    tus = Console.ReadKey(); //basılan tuşu kaydet
                    switch (tus.Key)
                    {
                        case ConsoleKey.UpArrow: //yukarı ok basıldıysa 
                            secimNoEski = secimNo; //değişiklik yapmadan önce eski değeri seçimi kaydet
                            if (secimNo == 0) //zaten en yukardaysa en aşağıdaki seçeneği seç
                            {
                                secimNo = menuSecenekler.Length - 1;
                            }
                            else //yukardaki seçenek ayarlanır
                            {
                                secimNo--;
                            }

                            break;
                        case ConsoleKey.DownArrow: //aşağı ok basıldıysa
                            secimNoEski = secimNo; //değişiklik yapmadan önce eski değeri seçimi kaydet
                            if (secimNo == menuSecenekler.Length - 1) //zaten en aşağıdaysa ilk değeri seç
                            {
                                secimNo = 0;
                            }
                            else //aşağıdaki seçeneği seç
                            {
                                secimNo++;
                            }
                            break;
                        case ConsoleKey.Enter: // Enter basılırsa 
                            Console.CursorVisible = true; //işaretçi görünrülüğünü aç
                            Console.Clear(); //ekranı temizle
                            Console.Title = menuSecenekler[secimNo]; //başlığı değiştirir
                            switch (secimNo) //hangi seçimdeyken enter basıldıysa ilgili kodlara yönlendirme yap
                            {
                                case 0:  //index 0
                                    konumaYazdir(); 
                                    break;
                                case 1:  //index 1
                                    dortIslem();
                                    break;
                            }
                            MenuOlustur();
                            break;
                    }
                    secimDegistir();
                }
            }
            
            Console.ReadLine();
        }
        

        static void konumaYazdir() //girilen konuma göre yazı yazdırılabilen program
        {
            int xMax = Console.BufferWidth-1; //maksimum girilebilecek x değerini bul
            int yMax = Console.BufferHeight-1; //maksimum girilebilecek y değerini bul

            Console.Write("X(0-{0}):",xMax); //kullanıcıdan x değerini iste
            int sayix = Convert.ToInt32(Console.ReadLine()); //girilen x değerini değişkene alır
            Console.Write("Y(0-{0}):",yMax); //kullanıcıdan y değerini iste
            int sayiy = Convert.ToInt32(Console.ReadLine()); //girilen y değerini değişkene alır
            Console.Clear() ; //ekranı temizle
            Console.CursorLeft = sayix; //işaretçinin konumları değişkenlere göre ayarlanır
            Console.CursorTop = sayiy;

            Console.ReadKey(); //herhangi tuşa basıldığında metot biter
        }

        static void dortIslem() //dört işlem yapan basit bir program
        {
            char sec; //hangi işemin yapılacağı bu değişkende tutulacak
            double sayi1, sayi2, sonuc = 0; //hesappamalarla ilgili diğer değişkenler tanımlandı
            do
            {
                Console.Clear(); //ekranı temizle
                Console.WriteLine("1-Toplama\n2-Çıkarma\n3-Çarpma\n4-Bölme\n5-ÇIKIŞ\n"); //seçenekleri yazdır
                Console.Write("seçeneğiniz: ");
                sec = Console.ReadKey().KeyChar; //kullanıcının seçiimini alır

                if (sec == '1' || sec == '2' || sec == '3' || sec == '4') //sadece 1 2 3 4 den birisi basıldığında çalışan kodlar
                {
                    Console.WriteLine(); //1 satır atla
                    Console.Write("1. Sayıyı giriniz: "); //kullanıcıdan 1. sayıyı ister
                    sayi1 = Convert.ToDouble(Console.ReadLine()); //1. sayı değişkene kaydedilir
                    Console.Write("2. Sayıyı giriniz: "); //kullanıcıdan 2. sayıyı ister
                    sayi2 = Convert.ToDouble(Console.ReadLine()); //2. sayı değişkene kaydedilir


                    switch (sec) //seçilem işleme göre ilgili işlemi yapan switch yapısı
                    {
                        case '1': //toplama
                            sonuc = sayi1 + sayi2;
                            break;
                        case '2': //çıkarma
                            sonuc = sayi1 - sayi2;
                            break;
                        case '3': //çarpma
                            sonuc = sayi1 * sayi2;
                            break;
                        case '4': //bölme
                            sonuc = sayi1 / sayi2;
                            break;
                    }
                    Console.WriteLine("Sonuç:{0}", sonuc); //sonuç yadırılır
                    Console.ReadKey();
                }
            } while (sec != '5'); //5 tuşuna basılmadıysa bu yapıdaki kodlar tekrar çalışır
        }
    }
}
