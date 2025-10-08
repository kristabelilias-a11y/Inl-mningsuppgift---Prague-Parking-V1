using System.ComponentModel.Design;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;


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
                    //case "3": HämtaFordon(); break;
                    //case "4": SökFordon(); break;
                    //case "5": Avsluta(); return;
                    default: Console.WriteLine("Ogilitgt val!"); continue;
                }

            }
        }


        void ParkeraFordon()
        {
            string? skiljeteckenMC = " | ";



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
                Console.WriteLine("Ogiltig fordonstyp. Endast 'Bil' eller 'MC' är tillåtna. Vänligen försök igen.");
                return;
            }

          


                string fordonsinformation = $"{regNr}-{fordonsTyp}"; // Få fram fordonet samt typen i en sträng

            Console.WriteLine();


            if (fordonsTyp == "BIL")

            {
                for (int j = 0; j < 100; j++)
                {
                    if (string.IsNullOrEmpty(pPlats[j]))
                    {
                        pPlats[j] = fordonsinformation;
                        Console.WriteLine($"{regNr} har tilldelats parkeringsplats nummer {j + 1}");
                        return;
                    }
                }
                Console.WriteLine("Alla platser är tyvärr upptagna");
            }

            else if (fordonsTyp == "MC")

            {
                for (int j = 0; j < pPlats.Length; j++)

                    if (string.IsNullOrEmpty(pPlats[j]) && (pPlats[j] == null || !pPlats[j].Contains("- MC") && pPlats[j].Contains("|"))) //Letar efter plats med annan MC
                    {
                        pPlats[j] += skiljeteckenMC + fordonsinformation; //Lägger till den nya MC:n
                        Console.WriteLine($"{regNr} har tilldelats en delad parkeringsplats nummer {j + 1}");
                        return;
                    }


                for (int j = 0; j < pPlats.Length; j++)
                {
                    if (string.IsNullOrEmpty(pPlats[j]))
                    {
                        pPlats[j] = fordonsinformation;
                        Console.WriteLine($"{regNr} har parkerats på egen plats {j + 1}");
                        return;
                    }
                }

                Console.WriteLine($"Alla platser är tyvärr upptagna");
     
            
                 
            }

        }

        static int HittaPlats(string regNr)
        {
            for (int i = 0; i < pPlats.Length; i++)
            {
                if (!string.IsNullOrEmpty(pPlats[i]) && pPlats[i].Contains(regNr))
                    return i; // returnerar platsens index som int
            }
            return -1; // om inget fordon hittas
        }

        static void FlyttaFordon()
        {
            Console.Write("Ange registreringsnummer att flytta: ");
            string regNr = Console.ReadLine().ToUpper();

            int gammalPlats = HittaPlats(regNr); 
            if (gammalPlats == -1)
            {
                Console.WriteLine($"Kunde inte hitta fordon med registreringsnummer {regNr}");
                return;
            }

            Console.WriteLine("Ange ny parkeringsplats (1-100)");
            if (int.TryParse(Console.ReadLine(), out int nyPlats) && nyPlats < 1 || nyPlats > 100)
            {
                Console.WriteLine("Ogiltig plats");

            }
                int nyPlatsIndex = nyPlats - 1;

                if (!string.IsNullOrEmpty(pPlats[nyPlatsIndex]))
                {
                    Console.WriteLine($"Plats {nyPlats} är redan upptagen. Flytt misslyckades.");
                    return;
                }

                if (pPlats[gammalPlats].Contains("|"))
            {
                string[] fordon = pPlats[gammalPlats].Split('|', StringSplitOptions.TrimEntries);

                if (fordon[0].StartsWith(regNr))
                {
                    pPlats[nyPlatsIndex] = fordon[0];
                    pPlats[gammalPlats] = fordon[1];
                }
                else
                {
                    pPlats[nyPlatsIndex] = fordon[1];
                    pPlats[gammalPlats] = fordon[0];

                }
            
            }
            else
            {
                pPlats[nyPlatsIndex] = pPlats[gammalPlats];
                pPlats[gammalPlats] = null;
            }

            Console.WriteLine($"{regNr} har flyttats till plats {nyPlats}");
        }
    


    }
}

