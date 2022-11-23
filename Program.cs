using System;
using System.IO;

namespace ConsoleApp18
{
    class Program
    {
        //A metódusokkal egy szinten lévő konstansokra minden metódusban lehet hivatkozni.
        //Ha a Main-ben hagytuk volna, akkor csak a Main-ben lettek volna elérhetőek.
        const char TENGER_JEL = '*';
        const char SZIGET_JEL = 'O';

        // Főprogram. Kezeli a menüpontokat, meghívja a szükséges metódusokat
        static void Main()
        {
            //char[,] tenger = new char[5, 10];
            //Alaphelyzet(tenger);
            //todo 10. feladat: Helyezze megjegyzésbe az előző két sort és helyette alkalmazza a következő sort!
            //Ha elkészül a General metódus, akkor erre kellene cserélni az előző két sort!
            char[,] tenger = General(5, 10);

            bool futasVege = false;
            do
            {
                switch (ValasztMenubol())
                {
                    case 'g':
                        //todo 2.feladat: General metódus meghívása. Előtte a szükséges adatok bekérése a mintavideó szerint.
                        
                        Console.Write("\nHány sorból álljon? : ");
                        int sor = Convert.ToInt32(Console.ReadLine());
                        Console.Write("\nHány oszlopból álljon? : ");
                        tenger = General(sor, Convert.ToInt32(Console.ReadLine()));
                        break;

                    case 'u': //pálya ürítése
                        Alaphelyzet(tenger);
                        break;

                    case 's': //szigeteket rakunk fel
                              //todo 4.feladat: A szigetek számának bekérése után hívja meg a SzigeteketRak metódust!
                        Console.Write("\nHány szigetet szeretne a tengerre? : ");
                        SzigeteketRak(tenger, Convert.ToInt32(Console.ReadLine()));
                        break;

                    //todo 5.feladat: folytassa a megjegyzésben lévő kódot!
                    case 'b': //pálya betöltése
                        Console.Write("\nKérem a tenger elérési útját és fájlnevét! :");
                        tenger = BetoltTerkepet(Console.ReadLine());
                        break;

                    //todo 12.feladat: Készítse el a mentés meghívásához szükséges kódrészletet!

                    case 'k':
                        futasVege = true;
                        break;

                    case 'm':
                        Console.Write("\nKérem a tenger elérési útját és fájlnevét! : ");
                        MentTerkepet(tenger, Console.ReadLine());
                        break;

                    case 'p':
                        Console.Write("\nAdja meg az x koordinátát: ");
                        int x = Convert.ToInt32(Console.ReadLine());
                        Console.Write("\nAdja meg az y koordinátát: ");
                        PalyaSzerkeszt(tenger, x-1, Convert.ToInt32(Console.ReadLine())-1);
                        break;
                }
                //todo 11.feladat: Érje el, hogy a következő két metódus ne legyen meghívva, ha a programból történő kilépést választották
                if(futasVege) break;
                Megjelenit(tenger, true);
                InformaciokKiirasa(tenger);
            } while (!futasVege);
        }
        /// <summary>
        /// Létrehoz a paraméterek által meghatározott méretű char típusú mátrixot (pályát), amit feltölt a tenger jelével.
        /// </summary>
        /// <param name="sorokSzama">A pálya sorainak száma</param>
        /// <param name="oszlopokSzama">A pálya oszlopainak száma</param>
        /// <returns>Visszatérési értéke a létrehozott tenger (mátrix)</returns>
        
        static char[,] General(int sorokSzama, int oszlopokSzama)
        {
            //todo 1.feladat: Új pálya (mátrix) létrehozása a paraméterek segítségével. Ezt követően feltöltés

            char[,] arr = new char[sorokSzama, oszlopokSzama];

            for(byte sor = 0; sor < sorokSzama; sor++)
                for(byte oszlop = 0; oszlop < oszlopokSzama; oszlop++)
                    arr[sor, oszlop] = TENGER_JEL;
            
            return arr;
        }

        /// <summary>
        /// A tengerrel és a szigetekkel kapcsolatos információk kigyűjtése és képernyőre írása a feladata.
        /// </summary>
        /// <param name="palya"></param>
        static void InformaciokKiirasa(char[,] palya)
        {
            Console.WriteLine("\n\nInformációk:");
            //todo 7.feladat: Interpolációs technikával készítse el a pálya méretének kiíratását a mintavideó szerint!
            Console.WriteLine($"Tenger mérete: {palya.GetLength(0)} sor x {palya.GetLength(1)} oszlop");

            //1.feladat
            //Hány sziget van a tengeren?
            Console.WriteLine($"A tengeren {SzigetekSzama(palya, SZIGET_JEL)} db sziget van!");

            //2. feladat
            //Hány sziget van tenger szélén?
            Console.WriteLine($"A tenger szélén {TengerSzelenSzigetekSzama(palya, SZIGET_JEL)} db sziget van!");
        }
        /// <summary>
        /// Betölti a megadott nevű szöveges fájlból a pályát.
        /// </summary>
        /// <param name="palyaNeve">Az állomány elérési útja és neve</param>
        /// <returns>A létrehozott és adatokkal feltöltött char mátrixot adja vissza</returns>
        static char[,] BetoltTerkepet(string palyaNeve)
        {
            string[] sorok = File.ReadAllLines(palyaNeve);
            char[,] palya = new char[sorok.Length, sorok[0].Length];
            for (int sorIndex = 0; sorIndex < palya.GetLength(0); sorIndex++)
            {
                for (int oszlopIndexe = 0; oszlopIndexe < palya.GetLength(1); oszlopIndexe++)
                {
                    palya[sorIndex, oszlopIndexe] = sorok[sorIndex][oszlopIndexe];
                }
            }
            return palya;
        }
        /// <summary>
        /// A megadott pályát elmenti a megadott néven szöveges fájlba.
        /// </summary>
        /// <param name="palya">A pálya adatait tartamazó char mátrix</param>
        /// <param name="palyaNeve">Az állomány elérési útja és neve</param>
        static void MentTerkepet(char[,] palya, string palyaNeve)
        {
            string[] sorok = new string[palya.GetLength(0)];
            // todo 13.feladat: A kapott pálya adatait kellene a sorok tömbbe elhelyezni!
            for(byte sor = 0; sor < sorok.Length; sor++)
                for (byte oszlop = 0; oszlop < sorok.Length; oszlop++)
                    sorok[sor] += palya[sor,oszlop];
            File.WriteAllLines(palyaNeve, sorok); //A string tömböt írja ki a fájlba
        }
        /// <summary>
        /// Megjeleníti az elérhető menüpontokat és lekérdezi a billentyűzetet.
        /// </summary>
        /// <returns>Visszaadja annak a menüpontnak a betüjelét, amit a felhasználó kiválasztott</returns>
        static char ValasztMenubol()
        {
            Console.WriteLine("\nMenü");
            Console.WriteLine("\t[g]enerál egy pályát");
            Console.WriteLine("\t[u]res pálya - A meglévő pálya alaphelyzetbe hozása");
            Console.WriteLine("\t[s]zigetek elhelyezése");
            Console.WriteLine("\t[b]etöltés fájlból");
            Console.WriteLine("\t[m]entés fájlba");
            Console.WriteLine("\t[p]álya szerkesztése");
            Console.WriteLine("\t[k]ilépés a programból");
            Console.Write("Kérem válasszon! ");
            char valasz;
            do
            {
                valasz = Console.ReadKey().KeyChar;
                //todo 6.feladat: Nem működik a kilépés menü! Mi lehet a gond? Módosítsa a kódot!
                if (valasz == 'g' || valasz == 'u' || valasz == 's'
                    || valasz == 'b' || valasz == 'm' || valasz == 'k' || valasz == 'p')
                {
                    break;
                }
            } while (true);
            Console.WriteLine();
            return valasz;
        }
        /// <summary>
        /// Meghatározza, hogy az adott pályán (tenger) hány jel van a megadott jelből.
        /// </summary>
        /// <param name="palya">Pálya</param>
        /// <param name="keresettJel">A keresett jel</param>
        /// <returns>A keresett jelek száma a pályán</returns>
        static int SzigetekSzama(char[,] terkep, char keresettJel)
        {
            int szigetekSzama = 0;
            for (int sorIndex = 0; sorIndex < terkep.GetLength(0); sorIndex++)
            {
                for (int oszlopIndexe = 0; oszlopIndexe < terkep.GetLength(1); oszlopIndexe++)
                {
                    //todo 8.feladat: Egészítse ki a kódot! Mi hiányzik a megszámoláshoz?
                    if (terkep[sorIndex,oszlopIndexe]==keresettJel)
                        szigetekSzama++;
                }
            }
            return szigetekSzama;
        }
        /// <summary>
        /// Meghatározza, hogy az adott pályán (tenger) hány jel van a megadott jelből a pálya szélén.
        /// </summary>
        /// <param name="palya">Pálya</param>
        /// <param name="keresettJel">A keresett jel</param>
        /// <returns>A szélen (első sor+utolsó sor+első oszlop+utolsó oszlop) lévő keresett jelek száma</returns>
        static int TengerSzelenSzigetekSzama(char[,] palya, char keresettJel)
        {
            int szigetSzam = 0;
            for (int sorIndex = 0; sorIndex < palya.GetLength(0); sorIndex++)
            {
                if (palya[sorIndex, 0] == keresettJel) //bal szélső oszlop
                    szigetSzam++;
                if (palya[sorIndex, palya.GetLength(1) - 1] == keresettJel) //jobb szélső oszlop
                    szigetSzam++;
            }
            for (int oszlopIndexe = 0; oszlopIndexe < palya.GetLength(1); oszlopIndexe++)
            {
                if (palya[0, oszlopIndexe] == keresettJel) //felső sor
                    szigetSzam++;
                if (palya[palya.GetLength(0) - 1, oszlopIndexe] == keresettJel) //alsó sor
                    szigetSzam++;
            }
            return szigetSzam;
        }
        /// <summary>
        /// Véletlenszerűen szigeteket helyez el a tengeren.
        /// </summary>
        /// <param name="palya">A pálya (tenger) mátrixa</param>
        /// <param name="darab">A szigetek száma</param>
        static void SzigeteketRak(char[,] palya, int darab)
        {
            Random vel = new Random();
            for (int i = 0; i < darab; i++)
            {
                int sorIndex = vel.Next(palya.GetLength(0));
                int oszlopIndexe = vel.Next(palya.GetLength(1));
                //todo 3.feladat: Helyezze el a sziget jelét a meghatározott pozícióra!
                palya[sorIndex,oszlopIndexe] = SZIGET_JEL;
            }
        }
        /// <summary>
        /// A kapott pályát (tenger) alaphelyzetbe állítja azzal, hogy mindenhová a tenger jelét teszi.
        /// </summary>
        /// <param name="palya">Hivatkozás a pályára</param>
        static void Alaphelyzet(char[,] palya)
        {
            for (int sorIndex = 0; sorIndex < palya.GetLength(0); sorIndex++)
            {
                for (int oszlopIndex = 0; oszlopIndex < palya.GetLength(1); oszlopIndex++)
                {
                    palya[sorIndex, oszlopIndex] = TENGER_JEL;
                }
            }
        }
        /// <summary>
        /// A pálya megjelenítését végzi. Kétféle módban működik a vanSzegely értékétől függően.
        /// </summary>
        /// <param name="terkep">A megjelenítendő pálya (tenger)</param>
        /// <param name="vanSzegely">false esetén nincs szegély, true esetény kiírja a sorok és oszlopok indexét szegély formájában</param>
        //todo 9.feladat: A 2. paraméternek legyen false a default értéke
        static void Megjelenit(char[,] terkep, bool vanSzegely=false)
        {
            Console.Clear();
            if (vanSzegely)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(' ');
                for (int oszlopIndex = 1; oszlopIndex <= terkep.GetLength(1); oszlopIndex++)
                {
                    if (oszlopIndex % 10 == 0)
                    {
                        Console.Write('.');
                    }
                    else
                    {
                        Console.Write(oszlopIndex % 10);
                    }
                }
            }
            Console.WriteLine();
            for (int sorIndex = 0; sorIndex < terkep.GetLength(0); sorIndex++)
            {
                if (vanSzegely)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    if ((sorIndex + 1) % 10 == 0)
                    {
                        Console.Write('.');
                    }
                    else
                    {
                        Console.Write((sorIndex + 1) % 10);
                    }
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                for (int oszlopIndexe = 0; oszlopIndexe < terkep.GetLength(1); oszlopIndexe++)
                {
                    Console.Write(terkep[sorIndex, oszlopIndexe]);
                }
                Console.WriteLine();
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        /// <summary>
        /// A pályára rak egy karaktert, alapból a.
        /// </summary>
        /// <param name="palya">A pálya (tenger) amire egy kiválasztott karaktert rak</param>
        /// <param name="x">X koordináta</param>
        /// <param name="y">Y koordináta</param>
        /// <param name="jel">Jel</param>
        static char[,] PalyaSzerkeszt(char[,] palya, int x, int y, char jel=SZIGET_JEL)
        {
            palya[x,y] = jel;
            return palya;
        }
    }
}