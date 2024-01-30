using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Transactions;

namespace franzAuftrag
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Encoding encoding = Encoding.UTF8;
            void EnglishMain()
            {


                /*
                Mountains
                Coastlines
                Countryside
                Plains
                Tundra
                Forests
                Grasslands
                Coastal
                Desert
                Savannah
                Plateaus
                Sahel
                Highlands
                Hills
                Coastline
                Volcanic Islands
                Island
                Forested
                Atolls
                Coral
                Reefs
                Urban
                 */

                string pathToCsv = "Path\\To\\CSV.csv";
                //creating dictionary with the country:Attribute pairs
                Dictionary<string, List<string>> countriesAndAttributes = new Dictionary<string, List<string>>();
                //Reading the lines of the file
                string[] lines = File.ReadAllLines(pathToCsv);

                //All the available attributes for the landscape
                string[] attributesList = { "Regenwald", "Berge", "Küsten", "Landschaft", "Ebenen", "Tundra", "Wälder", "Grasland", "Küstengebiet", "Wüste", "Savanne", "Plateaus", "Sahel", "Hochländer", "Hügel", "Küstenlinie", "Vulkaninseln", "Insel", "Bewaldet", "Atolle", "Korallen", "Riffe", "Urban" };

                //Save the attribute choices of the user 
                bool strand = false;
                string attribute = "";
                string klima = "";
                bool loop = true;



                //countriesAndAttributes["Key1"] = new List<string> { "42", "54" };
                for (int i = 0; i < lines.Length; i++)
                {
                    countriesAndAttributes[lines[i].Split(',')[0]] = new List<string> { lines[i].Split(',')[1], lines[i].Split(',')[2], lines[i].Split(',')[3], lines[i].Split(',')[4] };
                }
                /*
                //Debug Option the help with the encoding
                foreach (KeyValuePair<string, List<string>> entry in countriesAndAttributes)
                {
                    Console.WriteLine($"{entry.Key}: {string.Join(", ", entry.Value)}");
                }
                */

                //Write all the available landscape choices
                Console.WriteLine("Wählen sie ihre gewünschte Landschaft:");
                for (int i = 0; i < attributesList.Length; i++)
                {
                    Console.WriteLine($"{i}) {attributesList[i]}");

                }

                while (true)
                {
                    try
                    {
                        Console.WriteLine("\nGeben Sie eine Nummer ein: ");

                        //Save the users landscape choice
                        attribute = attributesList[Convert.ToInt32(Console.ReadLine())];
                        Console.WriteLine($"Ihre Wahl: {attribute}");
                        break;
                    }
                    catch { Console.WriteLine("Bitte geben Sie eine gültige Nummer ein."); }
                }


                //Get the Users beach preference
                Console.WriteLine("Mit oder ohne Strand?");
                Console.WriteLine("1) Mit");
                Console.WriteLine("2) Ohne");

                //Save the Users preference
                while (loop)
                {
                    switch (Console.ReadLine()) {
                        case "1":
                            strand = true;
                            loop = false;
                            break;

                        case "2":
                            strand = false;
                            loop = false;
                            break;
                        default:
                            Console.WriteLine("Please Enter a Valid Number.");
                            break;
                    }
                }
                loop = true;


                //Get Users climate preference
                Console.WriteLine("Welches Klima bevorzugen Sie?");
                Console.WriteLine("1) Warm");
                Console.WriteLine("2) Gemässigt");
                Console.WriteLine("3) Kalt");
                Console.WriteLine("4) Wechselhaft");

                //Save the Users choices
                loop = true;
                while (loop)
                {
                    switch (Console.ReadLine())
                    {
                        case "1":
                            klima = "warm";
                            loop = false;
                            break;
                        case "2":
                            klima = "gemässigt";
                            loop = false;
                            break;
                        case "3":
                            klima = "kalt";
                            loop = false;
                            break;
                        case "4":
                            klima = "wechselhaft";
                            loop = false;
                            break;
                        default:
                            Console.WriteLine("Bitte geben sie eine Zahl ein.");
                            break;
                    }
                }


                //Write a recap of all the choices
                Console.WriteLine("\n\nIhre Auswahl:");
                Console.WriteLine($"Landschaft: {attribute}");
                Console.WriteLine($"Strand: {strand}");
                Console.WriteLine($"Klima: {klima}");


                Dictionary<string, Int32> countryScores = new Dictionary<string, Int32>();

                //Rate the matches

                foreach (KeyValuePair<string, List<string>> entry in countriesAndAttributes)
                {
                    countryScores[entry.Key] = 0;

                    //check if the landscape matches
                    if (entry.Value[0].ToLower().Contains(attribute.ToLower()))
                    {
                        countryScores[entry.Key] += 3;
                    }
                    //check if the beach option matches
                    if (strand)
                    {
                        if (entry.Value[2].ToLower().Contains("y"))
                        {
                            countryScores[entry.Key] += 2;
                        }
                    }
                    else
                    {
                        if (entry.Value[2].ToLower().Contains("n"))
                        {
                            countryScores[entry.Key] += 2;
                        }
                    }
                    //check if the climate option matches
                    switch (klima)
                    {
                        case "warm":
                            if (entry.Value[3].ToLower().Contains("warm"))
                            {
                                countryScores[entry.Key] += 2;
                            }
                            break;
                        case "gemässigt":
                            if (entry.Value[3].ToLower().Contains("gemässigt"))
                            {
                                countryScores[entry.Key] += 2;
                            }
                            break;
                        case "kalt":
                            if (entry.Value[3].ToLower().Contains("kalt"))
                            {
                                countryScores[entry.Key] += 2;
                            }
                            break;
                        case "wechselhaft":
                            if (entry.Value[3].ToLower().Contains("wechselhaft"))
                            {
                                countryScores[entry.Key] += 2;
                            }
                            break;
                    }
                }
                //find the country with the highest rating
                // Create a list of key-value pairs
                List<KeyValuePair<string, int>> sortedList = countryScores.ToList();

                // Sort the list based on the values
                sortedList.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));

                // Create a new sorted dictionary
                Dictionary<string, int> sortedDictionary = sortedList.ToDictionary(x => x.Key, x => x.Value);

                // Display the sorted dictionary
                var keysWithCertainValues = sortedList.Where(pair => pair.Value == sortedList[0].Value).Select(pair => pair.Key).ToList();



                Console.WriteLine("\nIhrer Auswahl entsprechen folgende Länder: \n");
                int j = 0;
                foreach (var v in keysWithCertainValues)
                {
                    Console.WriteLine($"{v}: {countryScores[v]} Punkte");
                    Console.WriteLine($"\tLandschaft: {countriesAndAttributes[v][0]}");
                    Console.WriteLine($"\tTypisches Essen: {countriesAndAttributes[v][1]}");
                    if (countriesAndAttributes[v][2] == "y")
                    {
                        Console.WriteLine($"\tStrand: Ja");
                    }
                    else
                    {
                        Console.WriteLine($"\tStrand: Nein");
                    }

                    Console.WriteLine($"\tKlima: {countriesAndAttributes[v][3]}");
                   
                    j++;
                    if (j>=4) { break; }
                }
                Console.ReadLine();
            }


            //---------------------------------French Version-----------------------------------
            void FrenchMain()
            {


                string pathToCsv = "C:\\Users\\Super\\source\\repos\\franzAuftrag\\OnlyCountrysFrenchFrench.csv";
                //creating dictionary with the country:Attribute pairs
                Dictionary<string, List<string>> countriesAndAttributes = new Dictionary<string, List<string>>();
                //Reading the lines of the file
                string[] lines = File.ReadAllLines(pathToCsv);

                //All the available attributes for the landscape
                string[] attributesList = { "Forêt tropicale", "Montagnes", "Littoraux", "Campagne", "Plaines", "Toundra", "Forêts", "Prairies", "Côtier", "Désert", "Savane", "Plateaux", "Sahel", "Hautes terres", "Collines", "Littoral", "Îles volcaniques", "Île", "Boisé", "Atolls", "Corail", "Récifs", "Urbain" };


                //Save the attribute choices of the user 
                bool strand = false;
                string attribute = "";
                string klima = "";



                //countriesAndAttributes["Key1"] = new List<string> { "42", "54" };
                for (int i = 0; i < lines.Length; i++)
                {
                    countriesAndAttributes[lines[i].Split(',')[0]] = new List<string> { lines[i].Split(',')[1], lines[i].Split(',')[2], lines[i].Split(',')[3], lines[i].Split(',')[4] };
                }
                /*
                foreach (KeyValuePair<string, List<string>> entry in countriesAndAttributes)
                {
                    Console.WriteLine($"{entry.Key}: {string.Join(", ", entry.Value)}");
                }
                */

                //Write all the available landscape choices
                Console.WriteLine("Choisissez le paysage souhaité:");
                for (int i = 0; i < attributesList.Length; i++)
                {
                    Console.WriteLine($"{i}) {attributesList[i]}");

                }
                Console.WriteLine("\nEntrez un nombre: ");


                //Save the users landscape choice
                while (true)
                {
                    try
                    {
                        attribute = attributesList[Convert.ToInt32(Console.ReadLine())];
                        Console.WriteLine($"Votre choix: {attribute}");
                        break;
                    }
                    catch { Console.WriteLine("Entrez un nombre."); }
                }
                //Get the Users beach preference
                Console.WriteLine("Avec ou sans plage:");
                Console.WriteLine("1) Avec");
                Console.WriteLine("2) Sans");

                //Save the Users preference
                while (true)
                {
                    string answer = Console.ReadLine();
                    if (answer == "1")
                    {
                        strand = true;
                        break;
                    }
                    else if(answer == "2")
                    {
                        strand = false;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Entrez un nombre.");
                    }
                }

                //Get Users climate preference
                Console.WriteLine("Quel climat préférez-vous ?");
                Console.WriteLine("1) Chaud");
                Console.WriteLine("2) Tempéré");
                Console.WriteLine("3) Froid");
                Console.WriteLine("4) Changeant");

                //Save the Users choices
                
                switch (Console.ReadLine())
                {
                    case "1":
                        klima = "chaud";
                        break;
                    case "2":
                        klima = "tempéré";
                        break;
                    case "3":
                        klima = "froid";
                        break;
                    case "4":
                        klima = "changeant";
                        break;
                }

                //Write a recap of all the choices
                Console.WriteLine("\n\nVotre choix:");
                Console.WriteLine($"Paysage: {attribute}");
                Console.WriteLine($"Plage: {strand}");
                Console.WriteLine($"Climat: {klima}");


                Dictionary<string, Int32> countryScores = new Dictionary<string, Int32>();

                //Rate the matches

                foreach (KeyValuePair<string, List<string>> entry in countriesAndAttributes)
                {
                    countryScores[entry.Key] = 0;

                    //check if the landscape matches
                    if (entry.Value[0].ToLower().Contains(attribute.ToLower()))
                    {
                        countryScores[entry.Key] += 3;
                    }
                    //check if the beach option matches
                    if (strand)
                    {
                        if (entry.Value[2].ToLower().Contains("y"))
                        {
                            countryScores[entry.Key] += 2;
                        }
                    }
                    else
                    {
                        if (entry.Value[2].ToLower().Contains("n"))
                        {
                            countryScores[entry.Key] += 2;
                        }
                    }
                    //check if the climate option matches
                    switch (klima)
                    {
                        case "chaud":
                            if (entry.Value[3].ToLower().Contains("chaud"))
                            {
                                countryScores[entry.Key] += 2;
                            }
                            break;
                        case "tempéré":
                            if (entry.Value[3].ToLower().Contains("tempéré"))
                            {
                                countryScores[entry.Key] += 2;
                            }
                            break;
                        case "froid":
                            if (entry.Value[3].ToLower().Contains("froid"))
                            {
                                countryScores[entry.Key] += 2;
                            }
                            break;
                        case "changeant":
                            if (entry.Value[3].ToLower().Contains("changeant"))
                            {
                                countryScores[entry.Key] += 2;
                            }
                            break;
                    }
                }
                //find the country with the highest rating
                // Create a list of key-value pairs
                List<KeyValuePair<string, int>> sortedList = countryScores.ToList();

                // Sort the list based on the values
                sortedList.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));

                // Create a new sorted dictionary
                Dictionary<string, int> sortedDictionary = sortedList.ToDictionary(x => x.Key, x => x.Value);

                // Display the sorted dictionary
                var keysWithCertainValues = sortedList.Where(pair => pair.Value == sortedList[0].Value).Select(pair => pair.Key).ToList();


                int j = 0;
                Console.WriteLine("\nEn fonction de vos choix, le pays le mieux adapté à visiter est:");
                foreach (var v in keysWithCertainValues)
                {

                    Console.WriteLine($"{v}: {countryScores[v]} Points");
                    Console.WriteLine($"\tPaysage: {countriesAndAttributes[v][0]}");
                    Console.WriteLine($"\tNourriture emblématique: {countriesAndAttributes[v][1]}");
                    if (countriesAndAttributes[v][2] == "y")
                    {
                        Console.WriteLine($"\tPlage: oui");
                    }
                    else
                    {
                        Console.WriteLine($"\tPlage: non");
                    }

                    Console.WriteLine($"\tClimat: {countriesAndAttributes[v][3]}");
                    j++;
                    if (j >= 4) { break; }
                }
                Console.ReadLine();
            }


            
            
            
            Console.WriteLine("Möchten Sie das Programm in Französisch oder Deutsch ausführen? Bitte geben sie eine Nummer ein.\nVoulez-vous exécuter le programme en français ou en anglais ? Entrez un numéro");
            Console.WriteLine("1) Deutsch/Allemagne");
            Console.WriteLine("2) Französisch/Français");
            switch (Console.ReadLine())
            {
                case "1":
                    EnglishMain(); break;
                case "2":
                    FrenchMain(); break;
            }
        }
    }
}
