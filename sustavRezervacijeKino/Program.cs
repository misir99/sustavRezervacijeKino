namespace sustavRezervacijeKino
{
    class KinoDvorana
    {
        public string Film { get; set; }
        private int[,] sjedala;
        public int Redova { get; }
        public int SjedalaPoRedu { get; }
        private int brojProdanihKarata;
        public double ukupnaZarada;
        private List<string> povijestRezervacija = new List<string>();

        public KinoDvorana(string film, int r, int s)
        {
            Film = film;
            Redova = r;
            SjedalaPoRedu = s;
            sjedala = new int[r, s];
            this.brojProdanihKarata = 0;
            this.ukupnaZarada = 0;

        }
        public void PrikaziDvoranu()
        {
            Console.WriteLine($"\n--- PLATNO: {Film} ---");
            Console.WriteLine("Prodanih karata: " + brojProdanihKarata);
            Console.WriteLine("Ukupna zarada: $" + ukupnaZarada);
            Console.WriteLine("Slobodnih sjedala: " + SlobodnihSjedala);
           foreach (var zapis in povijestRezervacija)
            {
                Console.WriteLine(zapis);
            }
            Console.WriteLine();
            Console.Write("     ");
            for (int s = 1; s <= SjedalaPoRedu; s++)
            {
                Console.Write(s.ToString().PadRight(2));
            }
            Console.WriteLine();

            for (int i = 0; i < Redova; i++)
            {
                Console.Write((i + 1).ToString().PadLeft(2) + " | ");
                for (int j = 0; j < SjedalaPoRedu; j++)
                {
                    char simbol = (sjedala[i, j] == 0) ? '.' : 'X';
                    Console.Write(simbol + " ");
                }
                Console.WriteLine();
            }

            
        }

        public bool RezervirajSjedalo(int r, int s)
        {
            if (r < 0 || r >= Redova || s < 0 || s >= SjedalaPoRedu)
            {
                Console.WriteLine("Greška: Neispravan unos sjedala!");
                return false;
            }

            if (sjedala[r, s] == 0)
            {

                sjedala[r, s]= 1;
                brojProdanihKarata++;
               if(r == 0 || r == Redova - 1)
                {
                    ukupnaZarada += 10.0;
                   
                }
                else
                {
                                       ukupnaZarada += 7.0;
                    
                }
                string zapis = $"Red {r + 1}, Sjedalo {s + 1} - Film: {Film}";
                povijestRezervacija.Add(zapis);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("sjedalo je uspjesno rezervirano!");
                Console.ResetColor();
                return true;
               
            } 
           
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("sjedalo je vec zauzeto!");
                Console.ResetColor();
                return false;
            }
        }

        public int SlobodnihSjedala
        {
            get
            {
                int ukupno = Redova * SjedalaPoRedu;
                return ukupno - brojProdanihKarata;
            }
        }
    }
        internal class Program
        {
            static void Main(string[] args)
            {

             KinoDvorana dvorana1 = new KinoDvorana("Avatar 3", 5, 10);
           

             bool programRadi = true;
                while (programRadi)
                {
                    dvorana1.PrikaziDvoranu();

                    Console.WriteLine("\n--- IZBORNIK ---");
                    Console.WriteLine("1. Rezerviraj sjedalo");
                   
                    Console.WriteLine("2. Izlaz");
                    Console.Write("Odabir: ");
                    string izbor = Console.ReadLine();

                    if(izbor  == "1")
                    {
                        try
                        {
                            Console.WriteLine("Unesite RED (1-10): ");
                            int r = int.Parse(Console.ReadLine()) - 1;

                            Console.WriteLine("Unesite SJEDALO (1-12): ");
                            int s = int.Parse(Console.ReadLine()) - 1;

                            if(dvorana1.RezervirajSjedalo(r, s))
                            {
                                Console.WriteLine("uspjesno rezervirano! pritisnite bilo koju tipku...");
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Greška: Unosite samo brojeve!");
                        }
                        Console.ReadKey();
                    }

                    else if (izbor == "2")
                    {
                        programRadi = false;
                    }

                }


            }
        }
    
}
