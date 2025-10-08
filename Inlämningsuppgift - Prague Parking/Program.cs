using System.ComponentModel.Design;
using System;
using System.Collections.Generic;




List<string> regNr = new List<string>();
List<string> fordonsTyp = new List<string>();
string skiljetecken = new string(" - ");
string fördelaMC = ";";
string[] pPlats = new string[101];



Console.WriteLine("Välkommen till Prag Parking");

void meny()
{
    while (true)
{
        Console.WriteLine("\n1. Parkera fordon");
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
            case "5": Avsluta(); break;
            default: Console.WriteLine("Ogilitgt val!"); continue;
        }

    }
}

void ParkeraFordon()
{
    string? regNr;
    string? fordonsTyp;
    string? skiljetecken = " - ";

    do
    {
        Console.Write("\nVänligen ange ditt registreringsnummer: ");
        regNr = Console.ReadLine().ToUpper();

        

        if (regNr.Length > 10)
        {
            Console.WriteLine("\nFör lång text. Max 10 tecken tillåtet. Vänligen försök igen.");
        }
    } while (regNr.Length > 10);
            

    do
    {
        Console.Write("\nVänligen ange fordonstyp (Bil eller MC): ");
        fordonsTyp = Console.ReadLine().ToUpper();

        if (fordonsTyp.ToLower() != "bil" && fordonsTyp.ToLower() != "mc")
        {
            Console.WriteLine("Ogiltig fordonstyp. Endast 'Bil' eller 'MC' är tillåtna. Vänligen försök igen.");
        }
    } while (fordonsTyp.ToLower() != "bil" && fordonsTyp.ToLower() != "mc");


    List<string> fordonsInformation = new List<string>();
    fordonsInformation.Add(regNr);
    fordonsInformation.Add(skiljetecken);
    fordonsInformation.Add(fordonsTyp);

    Console.WriteLine();
    Console.WriteLine(fordonsInformation);
    Console.WriteLine();
    string[] pPlats = new string[100];


    for (int j = 0; j < 100; j++)
    {
        if (fordonsTyp.ToLower() == "bil")
        {
            if (string.IsNullOrEmpty(pPlats[j]))
            {
                pPlats[j] = regNr;
                Console.WriteLine($"{regNr} har tilldelats parkeringsplats nummer {j + 1}");
                return;
            }
        }
        else if (fordonsTyp.ToLower() == "mc")
        {
            if (string.IsNullOrEmpty(pPlats[j]))
            {
                pPlats[j] = regNr;
                Console.WriteLine($"{regNr} har tilldelats parkeringsplats nummer {j + 1}");
                return;
            }
            else if (pPlats[j].EndsWith("- MC") && !pPlats[j].Contains(fördelaMC))
            {

                pPlats[j] += ";" + regNr + "-MC";
                Console.WriteLine($"{regNr} har tilldelats parkeingsplats {j + 1} (delad med annan MC)");
                return;

            }
        }
    }

    Console.WriteLine("Inga lediga parkeringsPlatser.");

}
   


static void FlyttaFordon()
{
    Console.Write("Ange registreringsnummer att flytta: ");
    string regNr = Console.ReadLine().ToUpper();

        string plats;
        if (string.IsNullOrEmpty(regNr))
     if (plats != regNr)

    { 

        Console.WriteLine("Fordonet hittades inte");
        return;

    }


 
        Console.WriteLine("Ange ny parkeringsplats (1-100)");
    if (int.TryParse(Console.ReadLine(), out int nyPlats) && nyPlats >= 1 && nyPlats <= 100)
    {
       
        }

        
    }
}

}

