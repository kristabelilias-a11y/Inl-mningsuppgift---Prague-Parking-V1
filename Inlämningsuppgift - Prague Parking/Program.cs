using System.ComponentModel.Design;
using System;
using System.Collections.Generic;


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
            case "5": Avsluta(); break;
            default: Console.WriteLine("Ogilitgt val!"); continue;
        }
          
    }
}


void ParkeraFordon()
{
    string? skiljetecken = " - ";

    do
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
        
   
            

    do
    {
        Console.Write("\nVänligen ange fordonstyp (Bil eller MC): ");
        string fordonsTyp = Console.ReadLine().ToUpper();

        if (fordonsTyp.ToLower() != "bil" && fordonsTyp.ToLower() != "mc")
        {
            Console.WriteLine("Ogiltig fordonstyp. Endast 'Bil' eller 'MC' är tillåtna. Vänligen försök igen.");
        }
    } while (fordonsTyp.ToLower() != "bil" && fordonsTyp.ToLower() != "mc");



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
                break;
            }
        }
        else if (fordonsTyp.ToLower() == "mc")
        {
            if (string.IsNullOrEmpty(pPlats[j]))
            {
                pPlats[j] = regNr;
                Console.WriteLine($"{regNr} har tilldelats parkeringsplats nummer {j + 1}");
                break;
            }
            else if (pPlats[j].EndsWith("- MC") && !pPlats[j].Contains(fördelaMC))
            {

                pPlats[j] += fördelaMC + regNr + "-MC";
                Console.WriteLine($"{regNr} har tilldelats parkeingsplats {j + 1} (delad med annan MC)");
                break;

            }
        }
    }
     
}
   


static void FlyttaFordon()
    {
        Console.Write("Ange registreringsnummer att flytta: ");
        string regNr = Console.ReadLine().ToUpper();

        string plats;
        if (string.IsNullOrEmpty(regNr))
        //if (plats != regNr)

        {

            Console.WriteLine("Fordonet hittades inte");
            return;

        }



        Console.WriteLine("Ange ny parkeringsplats (1-100)");
        if (int.TryParse(Console.ReadLine(), out int nyPlats) && nyPlats >= 1 && nyPlats <= 100)
        {

        }


    }

