using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace nesne1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CalısanMetot("Main(string[] args)");
            int secimNo = 1; //ilk index 0'dır, sayı arttıkça aşağıdaki seçnekleri seçilidir
            int secimNoEski = 1; //başlangıçta eski seçim yok, imkansız değer atandı
            
            // Menü seçeneklerinin yazıları buradan belirlenir  ******************************************************************
            string[] menuSecenekler = { " Konuma yazdırma ",    //index 0  
                                        " Dört İşlem ",         //index 1
                                        " Fibonacci ",
                                        " parametre aktarma yöntemleri ",
                                        " Kutu çiz ",
                                        " Ters Yaz zaY sreT ", //index 5 
                                        " Koleksiyonlar ",
                                        " Class (Sınıflar) "};
            //********************************************************************************************************************
            MenuOlustur();
            void MenuOlustur() //menüyü ve davranışları bu metotun içinde
            {
                Console.Title = "NTP1"; //başlığı değiştirir
                Console.Clear(); //tertemiz ekranla işe başlayalım
                int consoleWidth, consoleHeight;
                consoleHeight = Console.BufferHeight; // konsola yazılabilecek karakter yüksekliğini alır
                consoleWidth = Console.BufferWidth;   //konsola yazılabilecek karakter genişliğini alır

                Console.CursorVisible = false; //seçim menüsündeyken işaretçinin görünürlüğünü kapatır

                

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
                int leftMarginMenuKutu = consoleWidth/2; // menü kutusu için
                int lengthMenuKutu = 0; //menu kutusu için


                for (int i = 0; i <= menuSecenekler.Length - 1; i++) //tüm seçenekleri yazdıran döngü
                {
                    int leftMargin = consoleWidth - menuSecenekler[i].Length;
                    if (leftMargin % 2 == 1) { leftMargin--; }
                    leftMargin = leftMargin / 2;  // buraya kadar yazının uzunluğuna göre ortalanma hesaplamaları yapıldı
                    if (leftMarginMenuKutu> leftMargin) //menünün üstüne çizilecek kutu için en uzun isimli seçenekten ölçü alan kodlar
                    { leftMarginMenuKutu= leftMargin; lengthMenuKutu = menuSecenekler[i].Length; }
                    Console.CursorLeft = leftMargin; // işaretçi yatay hizalama yapıldı
                    menuSecenekKonum[i, 0] = Console.CursorLeft; //yazılan seçeneğin yatay konumu i'ninci indexte kaydedildi
                    menuSecenekKonum[i, 1] = Console.CursorTop; // yazılan seçeneğin dikey konumu i'ninci indexte kaydedildi
                    Console.WriteLine(menuSecenekler[i]);  //i'ninci indexteki seçeneği yazdır
                }
                Console.ForegroundColor= ConsoleColor.DarkYellow;
                KutuOlustur(leftMarginMenuKutu-2,topMargin-1, leftMarginMenuKutu + lengthMenuKutu+1,topMargin+menuSecenekler.Length+2);
                Console.ForegroundColor = ConsoleColor.White;


                void secimDegistir() //yün tuşlarına basıldığında tetiklenir
                {
                    Console.CursorLeft = menuSecenekKonum[secimNoEski, 0];
                    Console.CursorTop = menuSecenekKonum[secimNoEski, 1];
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine(menuSecenekler[secimNoEski]);

                    Console.CursorLeft = menuSecenekKonum[secimNo, 0];
                    Console.CursorTop = menuSecenekKonum[secimNo, 1]; //yeni seçimin konumuna git
                    Console.BackgroundColor = ConsoleColor.DarkGray; //yazı arkaplan rengini değiştir
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(menuSecenekler[secimNo]);    //seçimi tekrar yazdır
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;  //arkaplan rengini eski heline getir

                    if (secimNoEski != -1) //kodda verdiğimiz -1 değeri duruyorsa bir şey yapma
                    {                           // yeni seçimin arkapalnı değiştiği gibi eski seçimin arkaplanı normal hale gelir
                        
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
                            if (secimNo == 0) 
                            { secimNo = menuSecenekler.Length - 1;} //zaten en yukardaysa en aşağıdaki seçeneği seç
                            else 
                            { secimNo--;} break; //yukardaki seçenek ayarlanır
                        case ConsoleKey.DownArrow: //aşağı ok basıldıysa
                            secimNoEski = secimNo; //değişiklik yapmadan önce eski değeri seçimi kaydet
                            if (secimNo == menuSecenekler.Length - 1) 
                            {secimNo = 0;} //zaten en aşağıdaysa ilk değeri seç
                            else 
                            { secimNo++;}break;//aşağıdaki seçeneği seç
                        case ConsoleKey.Enter: 
                            secimYap();break;
                        case ConsoleKey.Spacebar: 
                            secimYap(); break;    // enter veya boşluk basılması seçimi onaylar
                    }
                    secimDegistir();
                }
               
            }
            void secimYap()
            {
                Console.CursorVisible = true; //işaretçi görünrülüğünü aç
                Console.Clear(); //ekranı temizle
                Console.Title = menuSecenekler[secimNo]; //başlığı değiştirir
                switch (secimNo) //hangi seçimdeyken enter basıldıysa ilgili kodlara yönlendirme yap
                {
                    case 0: konumaYazdir();//index 0
                        break; 
                    case 1: dortIslem();   //index 1
                        break;  
                    case 2: fibonacci2();
                        break;
                    case 3: parametreAktarma_Ornekler();
                        break;
                    case 4: kutuciz();
                        break;
                    case 5: tersyaz();   
                        break;
                    case 6: koleksiyonlar();
                        break;
                    case 7: sinif();
                        break;

                }
                Console.ReadLine();
                MenuOlustur();
            }
            Console.ReadLine();
        }
        static void CalısanMetot(string isim) //sağ üst köşeye çalışmakta olan metot isimini yazdıracak kodlar
        {
            int[] cursorXY = new int[2]; 
            ConsoleColor consoleColor = Console.ForegroundColor; 
            cursorXY[0] = Console.CursorLeft; cursorXY[1] = Console.CursorTop; // işlem yapıladan önceki işaretçi konumu ve renkler kaydedildi
            Console.CursorLeft = Console.BufferWidth - isim.Length; 
            Console.CursorTop = 0; // işaretçi sağ üst köşeey taşındı 

            Random rnd = new Random(); 
            ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
            Console.ForegroundColor = colors[rnd.Next(colors.Length)]; //Rastgele yazı rengi seçildi ve ayarlandı
            Console.WriteLine(isim); //metota verilen isim değerini yazdır
            Console.ForegroundColor = consoleColor; //yazı rengini eski rengine ayarla
            Console.SetCursorPosition(cursorXY[0], cursorXY[1]); //işaretçi konumubu eski yerine konumlandır
        }
        static void konumaYazdir()// girilen konuma göre yazı yazdırılabilen program
        {
            CalısanMetot("konumaYazdir()");
            int xMax = Console.BufferWidth - 1; //maksimum girilebilecek x değerini bul
            int yMax = Console.BufferHeight - 1; //maksimum girilebilecek y değerini bul

            Console.Write("X(0-{0}):", xMax); //kullanıcıdan x değerini iste
            int sayix = Convert.ToInt32(Console.ReadLine()); //girilen x değerini değişkene alır
            Console.Write("Y(0-{0}):", yMax); //kullanıcıdan y değerini iste
            int sayiy = Convert.ToInt32(Console.ReadLine()); //girilen y değerini değişkene alır
            Console.Clear(); //ekranı temizle
            Console.CursorLeft = sayix; //işaretçinin konumları değişkenlere göre ayarlanır
            Console.CursorTop = sayiy;
        }
        static void dortIslem()// dört işlem yapan basit bir program
        {
            char sec; //hangi işemin yapılacağı bu değişkende tutulacak
            double sayi1, sayi2, sonuc = 0; //hesappamalarla ilgili diğer değişkenler tanımlandı
            do
            {
                Console.Clear(); //ekranı temizle
                CalısanMetot("dortIslem()");
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
        static void fibonacci()// bu benim yaptığım metotda sınırın devamnı gelebiliyor, öyle hatası var
        {
            CalısanMetot("fibonacci()");
            int sayiA = 1, sayiB = 1, son; //döngüde kullanılacak değişkenler
            Console.WriteLine("Hangi sayıya kadar hesaplansın?"); 
            Console.Write("-->");
            son = Convert.ToInt32(Console.ReadLine()); 
            while ( son > sayiB || son > sayiA)//sınıra ulaşılıp ulaşılmadığını kontrol eder
            {
                Console.WriteLine(sayiB);
                sayiA = sayiA + sayiB;
                Console.WriteLine(sayiA);
                sayiB = sayiA + sayiB; //burayı neden böyle yazdım... açıklaması zor geldi(bana özelden sor anlatayım)
            }
        }
        static void fibonacci2()// başka yerden aldığım fibonacci kodları
        {
            CalısanMetot("fibonacci2()");
            int id, sd;
            Console.Write("İlk değeri giriniz: ");
            id = int.Parse(Console.ReadLine());
            Console.Write("Son değeri giriniz: ");
            sd = int.Parse(Console.ReadLine()); //ilk ve son değerlerin alınması

            for (int a = 0, b = 1, c = 1; c <= sd; a = b, b = c, c = a + b)
            {
                if (c >= id) Console.Write(c + " - ");
            }
            /* a b c değişkenleri sırayla yer değiştire değiştire yazdırılıyorlar
             *1- a+b sonucu c ye kaydediliyor
             *2- b -> a , c -> b değişimleri yapılıyor
             *3- 1. adım yapılarak döngü devam ediyor
             */

        }
        static void kutuciz()// KutuOlustur metodunun kullanıcının isteğine göre çalışabileceği girdi alma metodu
        {
            CalısanMetot("kutuciz()");
            int kose1x, kose1y, kose2x, kose2y;
            int xMax = Console.BufferWidth - 1; //maksimum girilebilecek x değerini bul
            int yMax = Console.BufferHeight - 1; //maksimum girilebilecek y değerini bul
            Console.Write("Köşe 1 X(0-{0}): ", xMax);
            kose1x = Convert.ToInt32(Console.ReadLine());
            Console.Write("Köşe 1 Y(0-{0}): ", yMax);
            kose1y = Convert.ToInt32(Console.ReadLine()); // ilk köşenin x y'si kullanıcıdan alındı
            Console.Clear(); 
            Console.SetCursorPosition(kose1x,kose1y);
            Console.Write("*"); // ekran temizlenir ve ilk köşe konumuna * işareti konur
            Console.SetCursorPosition(0,0);
            Console.Write("Köşe 2 X({1}-{0}): ", xMax, kose1x);
            kose2x = Convert.ToInt32(Console.ReadLine());
            Console.Write("Köşe 2 Y({1}-{0}): ", yMax, kose1y);
            kose2y = Convert.ToInt32(Console.ReadLine());  // 2. köşenin x y'si kullanıcıdan alındı
            Console.SetCursorPosition(kose2x, kose2y);
            Console.Write("*"); //2. köşe * ile işaretlenir
            Console.ReadKey(); 
            

            KutuOlustur(kose1x, kose1y, kose2x, kose2y); //kutu oluşturma metotuna köşe bilgileriyle gönderilir
        }
        static void KutuOlustur(int kose1x, int kose1y, int kose2x, int kose2y)// gelen konumlara göre kutu çizdiren metot
        {
            CalısanMetot("KutuOlustur(int kose1x, int kose1y, int kose2x, int kose2y)");
            for (int i = kose1x; i <= kose2x; i++) //kutunun üstünü ve altını çizen döngü
            {
                Console.SetCursorPosition(i, kose1y); Console.WriteLine("═");//üst 
                Console.SetCursorPosition(i, kose2y); Console.WriteLine("═");//alt
            }
            for (int i = kose1y; i <= kose2y; i++) //kutunun yanlarını çizdiren döngü
            {
                Console.SetCursorPosition(kose1x, i); Console.WriteLine("║");//sol
                Console.SetCursorPosition(kose2x, i);Console.WriteLine("║");//sağ
            }
            Console.SetCursorPosition(kose1x, kose1y); Console.Write("╔");//kutunun köşelerini düzelten kodlar
            Console.SetCursorPosition(kose2x, kose1y); Console.Write("╗");
            Console.SetCursorPosition(kose1x, kose2y); Console.Write("╚");
            Console.SetCursorPosition(kose2x, kose2y); Console.Write("╝");
        }
        static void KutuyuSil(int kose1x, int kose1y, int kose2x, int kose2y)  // aynı değerler girilirse çizilen kutuyu siler
        {
            CalısanMetot("KutuyuSil(int kose1x, int kose1y, int kose2x, int kose2y)");
            for (int i = kose1x; i <= kose2x; i++) //kutunun üstünü ve altını silen döngü
            {
                Console.SetCursorPosition(i, kose1y); Console.WriteLine(" ");//üst 
                Console.SetCursorPosition(i, kose2y); Console.WriteLine(" ");//alt
            }
            for (int i = kose1y; i <= kose2y; i++) //kutunun yanlarını silen döngü
            {
                Console.SetCursorPosition(kose1x, i); Console.WriteLine(" ");//sol
                Console.SetCursorPosition(kose2x, i); Console.WriteLine(" ");//sağ
            }
        }
        static void parametreAktarma_Ornekler()
        {
            //***********************************************************************************************************
            //===========================================================================================================
            // ÖRNEK 1 -- Değer  Yolu ile Parametre Aktarmak
            void BirEkle(int x)
            {
                CalısanMetot("BirEkle(int x)");
                // Bu metot çağırıldığında x isimli değişken RAM'de oluşur, verilen değer x'im içine kaydedilir
                x++; //xin değeri 1 artar
            }
            void ornek1_main()
            {
                CalısanMetot("ornek1_main()");
                int k = 7;
                BirEkle(k);// 7 değeri diğer metota gönderilir  ^^^^
                Console.WriteLine(k); //7 değeri diğer metotta kullanıldı ama burdaki akışta herhangi etkisi olmadı
                //                                                          bu nedenle ekrana 7 yazdırdı
                Console.ReadLine();
            }


            //***********************************************************************************************************
            //===========================================================================================================
            // ÖRNEK 2 -- Referans Yolu ile Parametre Aktarmak
            void BirEkle_ref(ref int x)
            {
                CalısanMetot("BirEkle_ref(ref int x)");
                /*bu örnekte 1. örnekten farklı olarak ramde yeni değişken oluşmak yerine 
                 * başka değişkenin referansını tutan  değişken oluşturuldu
                */
                x++; // referansta belirtilen değeri 1 arttır yani k değişkeni 1 arttıldı
            }
            void ornek2_main()
            {
                CalısanMetot("ornek2_main()");
                int k = 7;
                BirEkle_ref(ref k);
                Console.WriteLine(k); // 8 değeri yazdırılır
                Console.ReadLine();
            }


            //***********************************************************************************************************
            //===========================================================================================================
            // ÖRNEK 3 -- Output Yolu ile Parametre Aktarmak
            void KaresiniVer(int a, out int sonuc)
            {
                CalısanMetot("KaresiniVer(int a, out int sonuc)");
                sonuc = a * a; // sonuc değişkeninde yapılan değişiklik olduğu gibi "karesi" isimli değişkeni etkiledi
            }
            void ornek3_main()
            {
                CalısanMetot("ornek3_main()");
                int kok = 5;
                int karesi;
                KaresiniVer(kok, out karesi); //kok-->a  |  out karesi-->out sonuc

                Console.WriteLine("5'in karesi: {0}",karesi);
                Console.ReadLine();
            }


            //***********************************************************************************************************
            //===========================================================================================================
            // ÖRNEK 4 -- Params anahter kelimesi ile
            void parametreAdedi(params int[] dizi)
            {
                CalısanMetot("parametreAdedi(params int[] dizi)");
                foreach (int i in dizi) {  Console.Write(i+" - " ); } //bu parametrenin alabileceği dizi adedi esnektir
                Console.WriteLine(dizi.Length + "adet girildi");
            }
            void ornek4_main()
            {
                CalısanMetot("ornek4_main()");
                parametreAdedi(3, 4, 1, 8, 3, 8, 7, 7); //metoda her uzunluktaki dizi hatasız gönderilebilir
                Console.ReadLine() ;                    // params anahate kelimesinin güzelliği budur :)
            }


            //***********************************************************************************************************
            //===========================================================================================================
            // ÖRNEK 5 -- Özyineleme(Recursive) Metotlar
            void tekrarlanan_Metot(int a)
            {
                CalısanMetot("tekrarlanan_Metot(int a)");
                Console.WriteLine(a); //kendisi çağrıldığı gibi gelen değeri yazar
                if (a > 1) //gelen değer 1 den büyükse
                {
                    a--;
                    tekrarlanan_Metot(a); //1 eksiltilerek tekrar oluştur
                }
                //if koşulu sağlanmadığında, yani a parametresi 1 ile geldiğinde if bloğu çalışmayacak
                // bu sayede sonsuz kez kendini çağırmasının bittiği kısıma geliyoruz
            }
            void ornek5_main() //kullanıcının girdiği sayıyı 1 olana kadar eksilterek yazdıran örnek
            {
                CalısanMetot("ornek5_main()");
                Console.Write("Örnek 5 için Sayı giriniz: ");
                tekrarlanan_Metot(Convert.ToInt32(Console.ReadLine()));
                Console.ReadKey();
            }


            //***********************************************************************************************************
            //===========================================================================================================
            // ÖRNEK 6 -- Ekstra örnek: Özyinelemeli Metotlarla faktöriyel hesabı yapma
            int faktoriyel(int sayi, ref int sıra)
            {
                    /* faktöriyel metot    */ KutuOlustur(sıra * 5,(sıra * 1)+2, (Console.BufferWidth-(sıra *4))-2,( Console.BufferHeight-(sıra*1))-2); //Her kutu sıra çarpanı sayesinede farklı boyutlanabiliyor
                    /* kutusunu çizen      */ Console.SetCursorPosition((sıra * 5)+2, (sıra * 1) + 2); // işaretçi oluşturulan kutunun sol üst köşesine taşınır
                    /* ve çarpım sayısını  */ Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("faktöriyel(sayi:{0},sıra:{1})", sayi, sıra); Console.ForegroundColor = ConsoleColor.White; //Çağırılan Metodu paremetreleriyle yazdırır
                    /* yazan kodlar        */ Console.SetCursorPosition((sıra * 5) + 1, Console.BufferHeight/2); Console.Write(sayi); Console.CursorLeft = (sıra * 5) + 3; Console.Write("*"); //"sayı *" yazısını yazar "7 *" gibi

                if (sayi == 1)// faktöriyel metodu sonsuza kadar kendini çağırmasın diye durdurma koşulu burada yer alıyor
                { 
                    /* Return yazdır       */ Console.ForegroundColor = ConsoleColor.Magenta; Console.Write(">>>Return: 1<<<");Console.ReadKey(); //Yazı rengini ayarladı ve ">>>Return: 1<<<" 
                    /* Temizle             */ KutuyuSil(sıra * 5, (sıra * 1) + 2, (Console.BufferWidth - (sıra * 4)) - 2, (Console.BufferHeight - (sıra * 1)) - 2); //En içerde kalan kutu silinir
                    return 1; //Return metodu çalıştığında aşağıdaki kodlar çalışmaz
                }
                // sayı 1 olmadığı için aşağıdaki kodlar çalışacaktır
                sıra++; Console.ReadKey(); //bir sonraki tekrarda farklı kutu boyutu olabilmesi için sıra değişkeni güncellendi
                int genelCarpim = sayi * faktoriyel(sayi - 1, ref sıra); //gelen sayıyı çarpımda tutar ve sayının 1 küçüğünün tekrar çağırılmasını yapar
                
                    /* Kutuyu Kaldır       */ sıra--; KutuyuSil(sıra * 5, (sıra * 1) + 2, (Console.BufferWidth - (sıra * 4)) - 2, (Console.BufferHeight - (sıra * 1)) - 2); 
                    /* Eski yazıları kaldır*/ for (int i = ((sayi+sıra + 4) * 5) - 1; i > ((sıra + 2) * 5)-11; i--) { Console.SetCursorPosition(i, Console.BufferHeight / 2); Console.Write(" ");}
                    /* Return yazdır       */ Console.WriteLine(genelCarpim + "(Return)"); Console.ReadKey();
                
                return genelCarpim; //çarpım işlemi sonucunu gönderir
            }
            void ornek6_main()
            {
                int kutusırası = 0, sayi = 7; // sayı değişkeni faktöriyel sayısını belirler, fazla büyük sayı girilirse iç içe kutulardan alan kalmaycaktır
                // kutusırası değişleni 1 kez oluşturulur ve refarans yoluyla aynı değişken defalarca kullanılabilir
                int faktoriyelHesapla= faktoriyel(sayi,ref kutusırası); //geriye 
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("{0} faktöriyel {1} eder",sayi,faktoriyelHesapla);
                Console.ReadKey();
            }
            //===========================================================================================================
            // Program akışında örnekleri görebilmeniz için yönlendirme kodları:
            
            char ornekSecim; 
            do
            {
                Console.Clear();
                CalısanMetot("parametreAktarma_Ornekler()");
                Console.WriteLine("ÖRNEK 1 -- Değer  Yolu ile Parametre Aktarmak\n" +
                                  "ÖRNEK 2 -- Referans Yolu ile Parametre Aktarmak\n" +
                                  "ÖRNEK 3 -- Output Yolu ile Parametre Aktarmak\n" +
                                  "ÖRNEK 4 -- Params anahter kelimesi ile\n" +
                                  "ÖRNEK 5 -- Özyineleme(Recursive) Metotlar\n" +
                                  "ÖRNEK 6 -- Ekstra örnek: Özyinelemeli Metotlarla faktöriyel hesabı yapma\n" +
                                  "\nÖrnek numarasını girerek örneği çalıştırabilirsiniz\n" +
                                  "7 çıkış yapar. Seçiminiz? (1,2,3,4,5,6,7)");
                ornekSecim = Console.ReadKey().KeyChar;
                Console.Clear() ;
                switch (ornekSecim)
                {
                    case '1': ornek1_main(); break;
                    case '2': ornek2_main(); break;
                    case '3': ornek3_main(); break;
                    case '4': ornek4_main(); break;
                    case '5': ornek5_main(); break;
                    case '6': ornek6_main(); break;
                }
            } while (ornekSecim != '7');
            
        }//Bu konuyu örnklerle anlatan metot
        static void tersyaz() //girilen yazıyı tersten yadırır
        {
            ConsoleKey c; //kontrol mekanizmasındaki tuş girdi
            do
            {
                Console.Clear ();
                string yazi = Console.ReadLine();
                Console.WriteLine();
                for(int i = yazi.Length-1;i >= 0; i--)
                {
                    Console.Write(yazi[i]);
                }
                do
                {
                    Console.WriteLine();
                    Console.Write("Tekrar? (Y/N)");
                    c = Console.ReadKey().Key;
                } while (c != ConsoleKey.Y && c !=ConsoleKey.N);
            } while (c == ConsoleKey.Y);
        }
        static void koleksiyonlar()
        {
            //|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
            //===========================================================================================================
            // ÖRNEK 1 -- 
            void ornek1_main()
            {
                CalısanMetot("ornek1_main");

                ArrayList koleksiyon = new ArrayList();
                //koleksiyon

                string[] islem = { "Capacity", "AddRange", "Add", "Clear", "Reverse", "Insert", "Remove"};
                string[] islemD = islem;
                int islemSec = 0;
                if (islem.Length % 2 == 0)
                {
                    islemSec = (islem.Length / 2) - 1;
                }
                else
                {
                    islemSec = ((islem.Length-1)/ 2) ;
                }

                int islemSecD = islemSec;

                int islemMaxLenght = 0;
                bool tekrar = true;
                ConsoleKeyInfo consoleKeyInfo = new ConsoleKeyInfo();

                foreach(string a in islem) {if (a.Length > islemMaxLenght) { islemMaxLenght = a.Length;};}
                
                void kodyaz()
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;   Console.Write("ArrayList ");
                    Console.ForegroundColor = ConsoleColor.Blue;        Console.Write("koleksiyon ");
                    Console.ForegroundColor = ConsoleColor.White;       Console.Write("= ");
                    Console.ForegroundColor = ConsoleColor.DarkBlue;    Console.Write("new ");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;   Console.Write("ArrayList");
                    Console.ForegroundColor = ConsoleColor.White;       Console.Write("();");
                    Console.ForegroundColor = ConsoleColor.Blue;        Console.Write("\nkoleksiyon");
                    Console.ForegroundColor = ConsoleColor.White;       Console.Write('.');
                    Console.ForegroundColor = ConsoleColor.Yellow;      Console.Write(islem[islemSec]);
                    Console.ForegroundColor = ConsoleColor.White;       Console.Write("();");
                    Console.ResetColor();
                }

                Console.CursorVisible = false;
                do
                {
                    Console.Clear();
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("Ok tuşları seçimi değiştirir, Enter Onaylar");
                    string menuyazi = "";
                    
                    for (int i = 0; i <= islem.Length - 1; i++)
                    {
                        if (islemSec == i)
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        else
                        {
                            Console.ResetColor();
                        }
                            
                        if (islem[i].Length % 2 != 0) { islem[i] = islem[i] + " "; }
                        int bosluk = (islemMaxLenght - islem[i].Length) / 2;


                        string yazi = " <" + new string(' ', bosluk) + islem[i] + new string(' ', bosluk) + "> ";
                        menuyazi += yazi;
                        //Console.Write(yazi);
                    }


                    Console.WriteLine();
                    kodyaz();

                    string YeniMenuyazi=null;
                    consoleKeyInfo = Console.ReadKey();
                    switch (consoleKeyInfo.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            if (islemSecD == 0) { islemSecD = islemD.Length - 1; }
                            else { islemSecD--; }
                            string[] yeniDizi = new string[islem.Length];
                            yeniDizi[0] = islem[islem.Length - 1];
                            for(int i = 1;i<= yeniDizi.Length - 1; i++) { yeniDizi[i] = islem[i - 1]; }
                            islem = yeniDizi;

                            for (int i = 0; i < islemMaxLenght + 4; i++)
                            {
                                Console.SetCursorPosition(0, 1);
                                YeniMenuyazi = menuyazi.Substring(menuyazi.Length - 1, 1) + menuyazi.Substring(0, menuyazi.Length - 1);
                                menuyazi = YeniMenuyazi;
                                Console.Write(menuyazi);
                                Thread.Sleep(1);
                            }
                               

                            break;
                        case ConsoleKey.RightArrow:

                            if (islemSecD == islemD.Length - 1) { islemSecD = 0; }
                            else { islemSecD++; }
                           
                                string[] yeniDizi2 = new string[islem.Length];
                            for(int i = 0; i <= yeniDizi2.Length-2; i++) { yeniDizi2[i] = islem[i + 1]; }
                            yeniDizi2[yeniDizi2.Length - 1] = islem[0];
                            islem = yeniDizi2;

                            Console.SetCursorPosition(0, 1);
                            
                            for (int i = 0;i < islemMaxLenght + 4; i++)
                            {
                                Console.SetCursorPosition(0, 1);
                                YeniMenuyazi = menuyazi.Substring(1,menuyazi.Length - 1) + menuyazi.Substring(0,1);
                                menuyazi = YeniMenuyazi;

                                int mat = (islemMaxLenght + 4) * islemSec;
                                Console.Write(menuyazi.Substring(0, (islemMaxLenght + 4) * islemSec)); 
                                Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.White;
                                Console.Write(menuyazi.Substring((islemMaxLenght + 4) * islemSec)+1, (islemMaxLenght)); Console.ResetColor();
                                Console.Write(menuyazi.Substring((islemMaxLenght + 4) * islemSec+1) + 1, (islemMaxLenght + 4) * islemSec*islem.Length-1);
                                Thread.Sleep(1);
                            }
                            

                            break;
                        case ConsoleKey.Escape: tekrar = false; 
                            break;
                        case ConsoleKey.Enter:
                            tekrar = false;
                            break;
                    }
                } while (tekrar);
                
                Console.CursorVisible = true;
                // "Capacity", "AddRange", "Add", "Clear", "Reverse", "Insert", "Remove"
                switch (islemSecD)
                {
                    case 0:
                        Console.WriteLine("Capacity seçildi");
                        break;
                    case 1:
                        Console.WriteLine("AddRange seçildi");
                        break;
                    case 2:
                        Console.WriteLine("Add seçilid");
                        break;
                    case 3:
                        Console.WriteLine("Clear seçildi");
                        break;
                    case 4:
                        Console.WriteLine("Reverse seçildi");
                        break;
                    case 5:
                        Console.WriteLine("Insert seçildi");
                        break;
                    case 6:
                        Console.WriteLine("Remove seçildi");
                        break;
                }


            }
            koleksiyonSec();


            void koleksiyonSec()
            {
                ornek1_main();
                //Console.Clear();
                //CalısanMetot("koleksiyonSec()");
                //Console.WriteLine(  "ÖRNEK 1 -- Öğrenci listele \n" +
                //                    "" +
                //                    "" +
                //                    "");
                Console.ReadKey();
            }
                
        }
        static void sinif()
        {
            Class1.clmetot();
        }
    }
    
}
