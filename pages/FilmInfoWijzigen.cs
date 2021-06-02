﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft;
using Newtonsoft.Json;
using ProjectB.Classes;
using ProjectB.DAL;

namespace ProjectB.pages
{
    class FilmInfoWijzigen
    {
        public static bool inputCheck(string wat)
        {
            Console.Clear();
            Console.WriteLine("Klopt de nieuwe informatie die u wilt toevoegen?");
            Console.WriteLine();
            Console.WriteLine(wat);
            Console.WriteLine("\n1. Ja\n2. Annuleren");

            string antwoord = Beheer.Input();

            if (antwoord == "1")
                return true;
            if (antwoord == "2")
                return false;

            Console.Clear();
            Console.WriteLine("Er ging iets fout");
            Console.WriteLine();
            Console.WriteLine("Wilt u deze informatie definitief aan de film informatie toevoegen?");
            Console.WriteLine();
            Console.WriteLine(wat);
            Console.WriteLine("\n1. Ja\n2. Annuleren");

            antwoord = Beheer.Input();

            if (antwoord == "1")
                return true;
            else if (antwoord == "2")
                return false;
            else
                inputCheck(wat);

            return false;
        }
        public static void filmInfoWijzigen(int selectedFilm)
        {
            Console.Clear();

            Console.WriteLine("Informatie geselecteerde film\n\nWelke informatie wilt u veranderen:\n");
            Console.WriteLine();
            Console.WriteLine("1: Titel: " + DataStorageHandler.Storage.Films[selectedFilm].Titel);
            Console.WriteLine("2: Categorie: " + DataStorageHandler.Storage.Films[selectedFilm].Categorie);
            Console.WriteLine("3: Minimum leeftijd: " + DataStorageHandler.Storage.Films[selectedFilm].Leeftijd);
            Console.WriteLine("4: Beschrijving: " + DataStorageHandler.Storage.Films[selectedFilm].Beschrijving);
            Console.WriteLine("5: Projectie: " + DataStorageHandler.Storage.Films[selectedFilm].Projectiemoment);
            Console.WriteLine("6: Taal: " + DataStorageHandler.Storage.Films[selectedFilm].Taal);
            Console.WriteLine("7: Ondertiteling: " + DataStorageHandler.Storage.Films[selectedFilm].Ondertiteling);
            Console.WriteLine("8: Acteurs: " + DataStorageHandler.Storage.Films[selectedFilm].Acteurs);
            Console.WriteLine("9: Regiseur: " + DataStorageHandler.Storage.Films[selectedFilm].Regisseur);
            Console.WriteLine("10: Zaal: " + DataStorageHandler.Storage.Films[selectedFilm].Zaal);
            Console.WriteLine();
            Console.WriteLine("0. Terug naar filmprogramma beheren");

            string infoIndex = Beheer.Input("");
            bool correctAntwoord = infoIndex == "1" || infoIndex == "2" || infoIndex == "3" || infoIndex == "4" || infoIndex == "5" || infoIndex == "6" || infoIndex == "7" || infoIndex == "8" || infoIndex == "9";

            if (infoIndex == "1")
            {
                string nieuweInfo = Beheer.Input("Wat is de nieuwe titel: ");

                if (inputCheck(nieuweInfo))
                {
                    DataStorageHandler.Storage.Films[selectedFilm].Titel = nieuweInfo;
                    FilmprogrammaBeheren.filmprogrammaBeheren();
                }
                else
                    filmInfoWijzigen(selectedFilm);
            }

            else if (infoIndex == "2")
            {
                string nieuweInfo = Beheer.Input("Wat is de nieuwe categorie: ");
                if (inputCheck(nieuweInfo))
                {
                    DataStorageHandler.Storage.Films[selectedFilm].Categorie = nieuweInfo;
                    FilmprogrammaBeheren.filmprogrammaBeheren();
                }
                else
                    filmInfoWijzigen(selectedFilm);
            }

            else if (infoIndex == "3")
            {
                int nieuweInfo = 0;

                try { nieuweInfo = Int32.Parse(Beheer.Input("Wat is de nieuwe minimale leeftijd: ")); }
                catch { FilmInfoWijzigen.filmInfoWijzigen(selectedFilm);  }
                
                DataStorageHandler.Storage.Films[selectedFilm].Leeftijd = nieuweInfo;
                FilmprogrammaBeheren.filmprogrammaBeheren();
            }

            else if (infoIndex == "4")
            {
                string nieuweInfo = Beheer.Input("Wat is de nieuwe beschrijving: ");
                if (inputCheck(nieuweInfo))
                {
                    DataStorageHandler.Storage.Films[selectedFilm].Beschrijving = nieuweInfo;
                    FilmprogrammaBeheren.filmprogrammaBeheren();
                }
                else
                    filmInfoWijzigen(selectedFilm);
            }

            else if (infoIndex == "5")
            {
                int hoeveelDagen = 0;
                bool dagenInput = false;
                while (dagenInput != true)
                {
                    try
                    {
                        hoeveelDagen = Int32.Parse(Beheer.Input("\nOp hoeveel dagen totaal wordt de film gedraaid? "));
                        dagenInput = true;
                    }
                    catch { Console.WriteLine("Verkeerde input!\nTyp Enter"); Beheer.Input(); }
                }
                string[][] nProjectiemoment = new string[hoeveelDagen][];

                for (int i = 0; i < hoeveelDagen; i++)
                {
                    string datum = "";
                    bool datumInput = false;
                    while (datumInput != true)
                    {
                        Console.WriteLine($"\nWelke datum is de {i + 1}e projectie?\n(DD-MM-JJJJ)");
                        datum = Beheer.Input("");
                        if (datum.Length == 10)
                            datumInput = true;
                        else
                        {
                            Console.WriteLine("Verkeerde input!\nTyp Enter");
                            Beheer.Input();
                        }
                    }

                    int hoeveelTijden = 0;
                    bool tijdenInput = false;
                    while (tijdenInput != true)
                    {
                        try
                        {
                            hoeveelTijden = Int32.Parse(Beheer.Input($"\nHoe vaak wordt de film op {datum} gedraaid? \n"));
                            nProjectiemoment[i] = new string[hoeveelTijden + 1];
                            nProjectiemoment[i][0] = datum;
                            tijdenInput = true;
                        }
                        catch { Console.WriteLine("Verkeerde input!\nTyp Enter"); Beheer.Input(); }
                    }


                    for (int x = 1; x < hoeveelTijden + 1; x++)
                    {
                        bool huidigeTijdInput = false;
                        while (huidigeTijdInput != true)
                        {
                            Console.WriteLine($"\nOp welk tijdstip begint de {x}e projectie op {nProjectiemoment[i][0]}\n(HH:MM) ?");
                            string huidigeTijd = Beheer.Input("");
                            if (huidigeTijd.Length == 5)
                            {
                                huidigeTijdInput = true;
                                nProjectiemoment[i][x] = huidigeTijd;
                            }
                            else
                            {
                                Console.WriteLine("\nVerkeerde input!\nTyp Enter");
                                Beheer.Input();
                            }
                        }
                    }
                }
                DataStorageHandler.Storage.Films[selectedFilm].Projectiemoment = nProjectiemoment;
            }

            else if (infoIndex == "6")
            {
                string nieuweInfo = Beheer.Input("Welke taal is de nieuwe hoofdtaal: ");
                if (inputCheck(nieuweInfo))
                {
                    DataStorageHandler.Storage.Films[selectedFilm].Taal = nieuweInfo;
                    FilmprogrammaBeheren.filmprogrammaBeheren();
                }
                else
                    filmInfoWijzigen(selectedFilm);
            }

            else if (infoIndex == "7")
            {
                string nieuweInfo = Beheer.Input("Wat is de nieuwe taal van de ondertiteling: ");
                if (inputCheck(nieuweInfo))
                {
                    DataStorageHandler.Storage.Films[selectedFilm].Ondertiteling = nieuweInfo;
                    FilmprogrammaBeheren.filmprogrammaBeheren();
                }
                else
                    filmInfoWijzigen(selectedFilm);
            }

            else if (infoIndex == "8")
            {
                string nieuweInfo = Beheer.Input("Wie zijn de nieuwe acteurs: ");
                if (inputCheck(nieuweInfo))
                {
                    DataStorageHandler.Storage.Films[selectedFilm].Acteurs = nieuweInfo;
                    FilmprogrammaBeheren.filmprogrammaBeheren();
                }
                else
                    filmInfoWijzigen(selectedFilm);
            }

            else if (infoIndex == "9")
            {
                string nieuweInfo = Beheer.Input("Wie is de nieuwe regisseur: ");
                if (inputCheck(nieuweInfo))
                {
                    DataStorageHandler.Storage.Films[selectedFilm].Regisseur = nieuweInfo;
                    FilmprogrammaBeheren.filmprogrammaBeheren();
                }
                else
                    filmInfoWijzigen(selectedFilm);
            }

            else if (infoIndex == "10")
            {
                int nZaal = 0;
                while (nZaal != 1 && nZaal != 2 && nZaal != 3)
                {
                    Console.WriteLine("\nIn welke zaal speelt de film?\nZaal 1(IMAX), Zaal 2 (3D) of Zaal 3 (2D)\n");
                    string sZaal = Beheer.Input("Typ 1, 2 of 3: ");
                    try
                    {
                        nZaal = Int32.Parse(sZaal);
                    }
                    catch { Console.WriteLine("Verkeerde zaal input!\nTyp Enter"); Beheer.Input(); }
                }
                DataStorageHandler.Storage.Films[selectedFilm].Zaal = nZaal;
            }

            else if(infoIndex == "0")
            {
                FilmprogrammaBeheren.filmprogrammaBeheren();
            }
            if (!correctAntwoord)
                filmInfoWijzigen(selectedFilm);
        }
    }
}