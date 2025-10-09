using System.ComponentModel.Design;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;


public class PargueParking
{

    private static string[] pPlats = new string[100];

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


            if (fordonsTyp == "BIL")

            {
                for (int j = 0; j < 100; j++)
                {
                    if (string.IsNullOrEmpty(pPlats[j]))
                    {
                        pPlats[j] = fordonsinformation;
                        Console.WriteLine($"\n{fordonsinformation} har tilldelats parkeringsplats nummer {j + 1}");
                        return;
                    }
                }
                Console.WriteLine("\nlla platser är tyvärr upptagna");
            }

            else if (fordonsTyp == "MC")

            {
                for (int j = 0; j < pPlats.Length; j++)

                    if (string.IsNullOrEmpty(pPlats[j]) && (pPlats[j] == null || !pPlats[j].Contains("-MC") && pPlats[j].Contains(" | "))) //Letar efter plats med annan MC
                    {
                        pPlats[j] += " | " + fordonsinformation; //Lägger till den nya MC:n
                        Console.WriteLine($"\n{fordonsinformation} har tilldelats en delad parkeringsplats nummer {j + 1}");
                        return;
                    }


                for (int j = 0; j < pPlats.Length; j++)
                {
                    if (string.IsNullOrEmpty(pPlats[j]))
                    {
                        pPlats[j] = fordonsinformation;
                        Console.WriteLine($"\n{fordonsinformation} har parkerats på egen plats {j + 1}");
                        return;
                    }
                }

                Console.WriteLine($"\nAlla platser är tyvärr upptagna");



            }

        }

        static int HittaPlats(string fordonsinformation)
        {
            for (int i = 0; i < pPlats.Length; i++)
            {
                if (!string.IsNullOrEmpty(pPlats[i]) && pPlats[i].Contains(fordonsinformation))
                    return i; // returnerar platsens index som int
            }
            return -1; // om inget fordon hittas
        }


        static void FlyttaFordon()
        {
            Console.Write("\nAnge registreringsnummer att flytta: ");
            string regNr = Console.ReadLine().ToUpper();

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

                if (pPlats[nyPlatsIndex].Contains("-BIL",StringComparison.OrdinalIgnoreCase))

                {
                    Console.WriteLine($"\nPlats {nyPlats} är redan upptagen. Flytt misslyckades. Försök igen");
                    return;
                }

            string fordonSomFlyttas = null; // Hämta fordon som ska flyttas

            if (pPlats[gammalPlats].Contains(" | "))
            {
                string[] mclista = pPlats[gammalPlats].Split(" | ", StringSplitOptions.TrimEntries);
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
                fordonSomFlyttas = pPlats[gammalPlats];
                pPlats[gammalPlats] = null;

                // Bara en MC/bil kvar på platsen
            }

            if (!string.IsNullOrEmpty(pPlats[nyPlatsIndex]))

            {
                pPlats[nyPlatsIndex] += " | " + fordonSomFlyttas;
            }

            else
            {
                pPlats[nyPlatsIndex] = fordonSomFlyttas;

            }
            

            if (pPlats[nyPlatsIndex].Contains("-MC", StringComparison.OrdinalIgnoreCase))

            { 
                pPlats[nyPlatsIndex] = " | " + regNr + "-MC";
                Console.WriteLine($"\n{regNr} har flyttats till delad plats {nyPlats}.");

                pPlats[gammalPlats] = null; // ta bort fordonet från gamla platsen
                return;
            }

            else if (pPlats[nyPlatsIndex].Contains(" | "))
            {

                Console.WriteLine($"\nPlats {nyPlats} är full. Flytt misslyckades.");
                return;

            }







            Console.WriteLine($"\n{regNr} har flyttats till parkeringsplats nummer {nyPlats}");
    }
    

       static void HämtaFordon()

            
        {
            Console.Write("\nVänligen ange registreringsnummer för fordon som ska hämtas ut: ");
            string regNr = Console.ReadLine();

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

                        // Om det finns någon MC kvar, slå ihop dem
                        if (kvarvarande.Count > 0)
                            pPlats[j] = string.Join(" | ", kvarvarande);
                        else
                            pPlats[j] = null; // annars är platsen tom

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
                    Console.WriteLine($"\n{regNr} hittades på parkeirngsplats {j + 1}");
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


}

