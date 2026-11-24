using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Autokolcsonzo
{
    class Auto
    {
        public string Rendszam;
        public string Marka;
        public string Modell;
        public int Evjarat;
        public int Ar;

        public Auto(string rendszam, string marka, string modell, int evjarat, int ar)
        {
            Rendszam = rendszam;
            Marka = marka;
            Modell = modell;
            Evjarat = evjarat;
            Ar = ar;
        }

        public void Kiir()
        {
            Console.WriteLine($"{Rendszam} {Marka} {Modell} {Evjarat} {Ar} Ft");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List<Auto> autok = new List<Auto>();

            string[] sorok = File.ReadAllLines("autok.txt");

            for (int i = 1; i < sorok.Length; i++)
            {
                string[] adatok = sorok[i].Split(';');

                Auto auto = new Auto(
                    adatok[0],
                    adatok[1],
                    adatok[2],
                    int.Parse(adatok[3]),
                    int.Parse(adatok[4])
                );

                autok.Add(auto);
            }

            Console.WriteLine("1. Összes autó:");

            foreach (Auto auto in autok)
            {
                auto.Kiir();
            }

            Console.WriteLine("\n2. Keresés rendszám alapján:");
            Console.Write("Add meg a rendszámot: ");
            string rendszam = Console.ReadLine();

            Auto keresett = autok.FirstOrDefault(x => x.Rendszam == rendszam);

            if (keresett != null)
            {
                keresett.Kiir();
            }
            else
            {
                Console.WriteLine("Nincs ilyen autó!");
            }

            Console.WriteLine("\n3. Legolcsóbb autó:");

            Auto legolcsobb = autok.OrderBy(x => x.Ar).First();
            legolcsobb.Kiir();

            Console.WriteLine("\n4. Legdrágább autó:");

            Auto legdragabb = autok.OrderByDescending(x => x.Ar).First();
            legdragabb.Kiir();

            Console.WriteLine("\n5. Átlagár:");

            double atlag = autok.Average(x => x.Ar);
            Console.WriteLine($"Átlagos napi ár: {atlag} Ft");

            Console.WriteLine("\n6. Évjárat szerinti keresés:");
            Console.Write("Adj meg egy évjáratot: ");
            int ev = int.Parse(Console.ReadLine());

            foreach (Auto auto in autok.Where(x => x.Evjarat == ev))
            {
                auto.Kiir();
            }

            Console.WriteLine("\n7. Márka szerinti keresés:");
            Console.Write("Add meg a márkát: ");
            string marka = Console.ReadLine();

            foreach (Auto auto in autok.Where(x => x.Marka.ToLower() == marka.ToLower()))
            {
                auto.Kiir();
            }

            Console.WriteLine("\n8. Autók száma:");

            Console.WriteLine($"Összesen {autok.Count} autó található.");

            Console.ReadKey();
        }
    }
}