using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nesne1
{
    internal class KutuOlusturClass
    {
        public void basla(int kose1x, int kose1y, int kose2x, int kose2y)
        {
            new CalısanMetotClass { isim = "KutuOlustur(int kose1x, int kose1y, int kose2x, int kose2y)" }.yaz();
            for (int i = kose1x; i <= kose2x; i++) //kutunun üstünü ve altını çizen döngü
            {
                Console.SetCursorPosition(i, kose1y); Console.WriteLine("═");//üst 
                Console.SetCursorPosition(i, kose2y); Console.WriteLine("═");//alt
            }
            for (int i = kose1y; i <= kose2y; i++) //kutunun yanlarını çizdiren döngü
            {
                Console.SetCursorPosition(kose1x, i); Console.WriteLine("║");//sol
                Console.SetCursorPosition(kose2x, i); Console.WriteLine("║");//sağ
            }
            Console.SetCursorPosition(kose1x, kose1y); Console.Write("╔");//kutunun köşelerini düzelten kodlar
            Console.SetCursorPosition(kose2x, kose1y); Console.Write("╗");
            Console.SetCursorPosition(kose1x, kose2y); Console.Write("╚");
            Console.SetCursorPosition(kose2x, kose2y); Console.Write("╝");
        }
                        


    }
}
