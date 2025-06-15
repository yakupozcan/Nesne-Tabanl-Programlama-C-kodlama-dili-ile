using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nesne1
{
    internal class CalısanMetotClass
    {
        public string isim;
        int[] cursorXY = new int[2];
        ConsoleColor consoleColor = Console.ForegroundColor;
        public void yaz()
        {
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
        
    }
}
