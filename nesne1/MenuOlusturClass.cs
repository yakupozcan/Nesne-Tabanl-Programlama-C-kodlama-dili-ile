using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nesne1
{
    internal class MenuOlusturClass
    {
        public int secimNo = 1; //ilk index 0'dır, sayı arttıkça aşağıdaki seçnekleri seçilidir
        public int secimNoEski = 1; //başlangıçta eski seçim yok, imkansız değer atandı
        public string[] menuSecenekler;
        public Action[] Eylemler {  get; set; }
        
        public void MenuOlustur() //menüyü ve davranışları bu metotun içinde
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
            int leftMarginMenuKutu = consoleWidth / 2; // menü kutusu için
            int lengthMenuKutu = 0; //menu kutusu için


            for (int i = 0; i <= menuSecenekler.Length - 1; i++) //tüm seçenekleri yazdıran döngü
            {
                int leftMargin = consoleWidth - menuSecenekler[i].Length;
                if (leftMargin % 2 == 1) { leftMargin--; }
                leftMargin = leftMargin / 2;  // buraya kadar yazının uzunluğuna göre ortalanma hesaplamaları yapıldı
                if (leftMarginMenuKutu > leftMargin) //menünün üstüne çizilecek kutu için en uzun isimli seçenekten ölçü alan kodlar
                { leftMarginMenuKutu = leftMargin; lengthMenuKutu = menuSecenekler[i].Length; }
                Console.CursorLeft = leftMargin; // işaretçi yatay hizalama yapıldı
                menuSecenekKonum[i, 0] = Console.CursorLeft; //yazılan seçeneğin yatay konumu i'ninci indexte kaydedildi
                menuSecenekKonum[i, 1] = Console.CursorTop; // yazılan seçeneğin dikey konumu i'ninci indexte kaydedildi
                Console.WriteLine(menuSecenekler[i]);  //i'ninci indexteki seçeneği yazdır
            }
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            var kutu = new KutuOlusturClass();
            kutu.basla(leftMarginMenuKutu - 2, topMargin - 1, leftMarginMenuKutu + lengthMenuKutu + 1, topMargin + menuSecenekler.Length + 2);
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
                        { secimNo = menuSecenekler.Length - 1; } //zaten en yukardaysa en aşağıdaki seçeneği seç
                        else
                        { secimNo--; }
                        break; //yukardaki seçenek ayarlanır
                    case ConsoleKey.DownArrow: //aşağı ok basıldıysa
                        secimNoEski = secimNo; //değişiklik yapmadan önce eski değeri seçimi kaydet
                        if (secimNo == menuSecenekler.Length - 1)
                        { secimNo = 0; } //zaten en aşağıdaysa ilk değeri seç
                        else
                        { secimNo++; }
                        break;//aşağıdaki seçeneği seç
                    case ConsoleKey.Enter:
                        secimYap(); break;
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

            if (Eylemler != null && secimNo < Eylemler.Length)
                Eylemler[secimNo]?.Invoke();
            Console.ReadLine();
            MenuOlustur();
        }
        
    }
}
