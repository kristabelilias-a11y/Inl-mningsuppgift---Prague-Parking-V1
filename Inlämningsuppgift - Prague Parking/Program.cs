using System.ComponentModel.Design;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;


public class PargueParking
{

    private static string[] pPlats = new string[101];

    public static void Main(string[] args)
    {

        Console.WriteLine("Välkommen till Prague Parking");

        {


            while (true)
            {

                Console.WriteLine("\n MENY");
                Console.WriteLine("1. Parkera fordon");
                Console.WriteLine("2. Flytta fordon");
                Console.WriteLine("3. Hämta fordon");
                Console.WriteLine("4. Sök fordon");
                Console.WriteLine("5. Avsluta");

                Console.Write("\nAnge ett av alternativen: ");


                string val = Console.ReadLine();


                switch (val)
                {
                    case "1": ParkeraFordon(); break;
                    case "2": FlyttaFordon(); break;
                    case "3": HämtaFordon(); break;
                    case "4": SökFordon(); break;
                    case "5": Avsluta(); 
                        
                        Console.WriteLine("\n\nProgrammet avslutas. Tack för besöket!"); // om användaren väljer 5
                        Environment.Exit(0);
                        break;

                    default: Console.WriteLine("\nOgilitgt val!");
                                           return;
                }

            }
        }

        void ParkeraFordon()
        {

          

            Console.Write("\nVänligen ange ditt registreringsnummer: ");
            string regNr = Console.ReadLine().ToUpper();


            // Förhindra att fordonet redan finns i systemet
            for (int j = 0; j < pPlats.Length; j++)
            {
                if (!string.IsNullOrEmpty(pPlats[j]) && pPlats[j].Contains(regNr + "-", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"\nFordon med registreringsnummer {regNr} finns redan på plats {j + 1}.");
                    return;
                }
            }


            if (regNr.Length > 10)
            {
                Console.WriteLine("\nFör lång text. Max 10 tecken tillåtet. Vänligen försök igen.");
            }
            else if (string.IsNullOrWhiteSpace(regNr))
            {
                Console.WriteLine("\nOgiltigt registreringsnummer");
                return;
            }




            Console.Write("\nVänligen ange fordonstyp (Bil eller MC): ");
            string fordonsTyp = Console.ReadLine().ToUpper();

            if (fordonsTyp.ToLower() != "bil" && fordonsTyp.ToLower() != "mc" || string.IsNullOrWhiteSpace(fordonsTyp))

            {
                Console.WriteLine("\nOgiltig fordonstyp. Endast 'Bil' eller 'MC' är tillåtna. Vänligen försök igen.");
                return;
            }




            string fordonsinformation = $"{regNr}-{fordonsTyp}"; // Få fram fordonstyp samt regNr i en sträng "ABC123-BIL"

            Console.WriteLine();


            if (fordonsTyp.EndsWith("BIL"))

            {
                for (int j = 0; j < pPlats.Length; j++)
                {
                    if (string.IsNullOrEmpty(pPlats[j]))
                    {
                        pPlats[j] = fordonsinformation;
                        Console.WriteLine($"\n{fordonsinformation} har tilldelats parkeringsplats nummer {j + 1}");
                        return;

                    }

                    

                }

            }

            else if (fordonsTyp.EndsWith("MC"))

            {
                for (int j = 0; j < pPlats.Length; j++)
                {
                    if (string.IsNullOrEmpty(pPlats[j]))
                    {
                        pPlats[j] = fordonsinformation;
                        Console.WriteLine($"\n{fordonsinformation} har tilldelats parkeringsplats nummer {j + 1}");
                        return;
                    }
                }


                for (int j = 0; j < pPlats.Length; j++)
                {

                    if (pPlats[j].Contains("MC")) //Letar efter plats med annan MC
                    {
                        pPlats[j] += " | " + fordonsinformation; //Lägger till den nya MC:n
                        Console.WriteLine($"\n{fordonsinformation} har tilldelats parkeringsplats nummer {j + 1}");
                        return;

                    }
                    if (fordonsTyp.EndsWith("BIL"))
                    {
                        Console.WriteLine("Denna plats rymmer inte en bil.");
                        return;
                    }

                }



                Console.WriteLine($"\nAlla platser är tyvärr upptagna");



            }

        }
        

            static int HittaPlats(string fordonsinformation)
            {
                for (int j = 0; j < pPlats.Length; j++)
                {
                    if (!string.IsNullOrEmpty(pPlats[j]) && pPlats[j].Contains(fordonsinformation))
                        return j; // returnerar platsens index som int
                }
                return -1; // om inget fordon hittas
            }

        

        static void FlyttaFordon()
        {
            Console.Write("\nAnge registreringsnummer att flytta: ");
            string regNr= Console.ReadLine().ToUpper();

            int gammalPlats = HittaPlats(regNr);

            if (gammalPlats == -1)
            {
                Console.WriteLine($"\nKunde inte hitta fordon med registreringsnummer {regNr}");
                return;
            }

            Console.Write("\nAnge ny parkeringsplats (1-100): ");
            if (int.TryParse(Console.ReadLine(), out int nyPlats) && nyPlats < 1 || nyPlats > 100)
            {
                Console.WriteLine("\nOgiltig plats");
                return;

            }

       

            int nyPlatsIndex = nyPlats - 1;

            if (!string.IsNullOrEmpty(pPlats[nyPlatsIndex]))

            {

                if (pPlats[nyPlatsIndex].Contains("BIL", StringComparison.OrdinalIgnoreCase))


                {
                    Console.WriteLine($"\nPlats {nyPlats} är redan upptagen. Flytt misslyckades. Försök igen");
                    return;
                }



                { if (pPlats[gammalPlats].EndsWith("BIL",StringComparison.OrdinalIgnoreCase))

                    if (pPlats[nyPlatsIndex].Contains("MC")) // En bil kan ej ställa sig där det finns en MC.
                    {
                        Console.WriteLine("Denna plats rymmer inte en bil.");
                        return;
                    }

                }

                if (pPlats[nyPlatsIndex].Contains(" | ") && pPlats[nyPlatsIndex].Contains("MC"))
                {

                    Console.WriteLine($"\nPlats {nyPlats} är full. Flytt misslyckades."); // Om det redan står två MC på platsen
                    return;

                }

                if (pPlats[nyPlatsIndex].Contains("BIL", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"\nKan inte flytta MC till plats {nyPlats}, där står en bil."); // En MC kan ej flyttas dit en bil står
                    return;
                }
            }
     

            string fordonSomFlyttas = null; // Hämta fordon som ska flyttas

            if (pPlats[gammalPlats].Contains(" | "))
            {
                string[] mclista = pPlats[gammalPlats].Split(" | ", StringSplitOptions.TrimEntries); // Dela upp MC som ska flyttas
                List<string> kvarvarande = new List<string>();


                foreach (string fordon in mclista)
                {
                    if (fordon.StartsWith(regNr, StringComparison.OrdinalIgnoreCase))
                    {
                        fordonSomFlyttas = fordon; // den MC vi flyttar
                    }
                    else
                    {
                        kvarvarande.Add(fordon); // den andra MCn stannar kvar
                    }
                }

                pPlats[gammalPlats] = kvarvarande.Count > 0 ? string.Join(" | ", kvarvarande) : null; // uppdaterar tidigare plats och lämnar kvar gammal MC om det finns
            }



            else
            {
                fordonSomFlyttas = pPlats[nyPlatsIndex];
                pPlats[gammalPlats] = null;

                // Bara en MC/bil kvar på platsen
            }

            if (!string.IsNullOrEmpty(pPlats[nyPlatsIndex]))

            {
                if (pPlats[nyPlatsIndex].Contains("MC"))
                {
                    pPlats[nyPlatsIndex] += " | " + fordonSomFlyttas; // Lägger endast till fordonet som flyttas
                    Console.WriteLine($"\n{regNr} har flyttats till delad plats {nyPlats}.");
                    return;
                }

               
            }
           

            else
            { Console.WriteLine($"\n{regNr} har flyttats till parkeringsplats nummer {nyPlats}"); }

        }
                
    }
    

       static void HämtaFordon()

            
        {
            Console.Write("\nVänligen ange registreringsnummer för fordon som ska hämtas ut: ");
            string regNr = Console.ReadLine().ToUpper();

            if (string.IsNullOrWhiteSpace(regNr))
            {
                Console.WriteLine("\nOgiltig angivelse. Försök igen");
                return;
            }

            bool hittad = false; //markerar om något hittas

            

            for (int j = 0; j < pPlats.Length; j++) // Leta igenom alla platser

            {
                if (string.IsNullOrEmpty(pPlats[j]))
                    continue;


                if (pPlats[j].Contains(regNr,StringComparison.OrdinalIgnoreCase)) // Om platsen innehåller regnr

                {
                    hittad = true;

                    if (pPlats[j].Contains(" | "))

                    {
                        string[] mcLista = pPlats[j].Split(" | "); // dela upp MC

                        List<string> kvarvarande = new List<string>();


                        foreach (string fordon in mcLista)
                        {
                            if (!fordon.Contains(regNr,StringComparison.OrdinalIgnoreCase)) // Behåller övriga fordon, ignorerar case
                            {
                                kvarvarande.Add(fordon); // Lägg till de MCs som inte ska tas bort
                            }
                        }

                        

                        Console.WriteLine($"\n{regNr} har hämtats ut från plats {j + 1}.");
                        return;
                    }

                    else
                    {
                        pPlats[j] = null;
                        Console.WriteLine($"\n{regNr} har hämtats ut från plats {j + 1}");
                        return;
                    }
                }


            }

            
                if (!hittad)

                {
                    Console.WriteLine($"\nInget fordon med registreringsnnummer {regNr} kunde hittas.");
                    return;
                }

         


        }



        static void SökFordon()

        {
            Console.Write("\nAnge registreringsnummer för fordon du söker: ");
            string regNr = Console.ReadLine().ToUpper();


            if (string.IsNullOrWhiteSpace(regNr))
            { Console.WriteLine("\nOgiltig inmatning."); }

            bool hittad = false;

            for (int j = 0; j < pPlats.Length; j++)

            {
                if (!string.IsNullOrEmpty(pPlats[j]) && pPlats[j].Contains(regNr, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"\n{regNr} hittades på parkeringsplats {j + 1}");
                    hittad = true;
                }
            }

            if (!hittad)
            {
                Console.WriteLine($"\n{regNr} kunde inte hittas i systemet.");
            }
        }


        static void Avsluta()
        {

        }
            
    }




