using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;

public class Hangman
{
    static void Main()
    {
        Console.WriteLine("Hello! This is a hangman game where You try to guess a capital of random country.");
        Console.WriteLine("On your last try, you will recive a hint :)");
        Console.WriteLine("Good Luck!");

        string[] text = File.ReadAllLines("countries_and_capitals.txt");
        Stopwatch stopWatch = new Stopwatch();
        bool loop = true;

        while (loop)
        {
            string guess = "";
            char[] guessWord;
            string notInWord = "";
            int wrong = 0;
            string choice;
            string strGuessWord = "";
            int guessingtries = 0;
            stopWatch.Start();
            Random rand = new Random();
            string capitalAndCountry = text[rand.Next(0, text.Length)];
            string[] randomChoice = capitalAndCountry.Split('|');
            string capital = randomChoice[1].ToLower().Trim();
            string country = randomChoice[0];

            guessWord = new char[capital.Length];
            for (int i = 0; i < guessWord.Length; i++)
            {
                guessWord[i] = '_';
            }
            Console.Write("Capital to guess: ");
            for (int i = 0; i < guessWord.Length; i++)
                Console.Write(guessWord[i] + " ");
            Console.WriteLine("");

            while (strGuessWord != capital && wrong < 5)
            {
                if (wrong == 4)
                {
                    Console.WriteLine("You have one guess left.\nCapital of {0}?", country);
                }
                if ((5 - wrong) > 1)
                {
                    Console.WriteLine("You have {0} life points.", 5 - wrong);
                }
                Console.WriteLine("do you want to guess whole word or a single letter ? type w for word or l for letter:");
                choice = Console.ReadLine();
                if (choice == "l")
                {
                    Console.WriteLine("Guess a letter: ");
                    guess = Console.ReadLine().ToLower();
                    guessingtries++;
                    Console.Clear();

                    bool found = false;
                    for (int i = 0; i < capital.Length; i++)
                    {
                        if (guess[0] == capital[i])
                        {
                            guessWord[i] = guess[0];
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        wrong++;
                        notInWord += guess[0] + " ";
                        Console.WriteLine("incorrect guess");
                    }

                    Console.WriteLine(new string(guessWord));
                    strGuessWord = new string(guessWord);
                }
                else if (choice == "w")
                {
                    Console.WriteLine("Guess a word: ");
                    guess = Console.ReadLine().ToLower();
                    guessingtries++;
                    Console.Clear();


                    if (guess == capital)
                    {
                        strGuessWord = capital;
                    }
                    else
                    {
                        wrong += 2;
                        Console.WriteLine("incorrect guess u lost 2 life points");
                    }
                }
            }

            if (strGuessWord == capital)
            {
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                string elapsedTime = String.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);

                Console.WriteLine("Congratulations ! You guessed correctly");
                Console.WriteLine("You guessed after {0} tries, and it took you {1} s ", guessingtries, elapsedTime);
                Console.WriteLine("do you want to save your score to a file ? y/n");
                string hs = Console.ReadLine();
                if (hs == "y")
                {
                    Console.WriteLine("Type your name:");
                    string name = Console.ReadLine();
                    DateTime localDate = DateTime.Now;
                    string score = name + " | " + localDate + " | " + guessingtries + " | " + elapsedTime + " | " + capital;
                    File.AppendAllText("score.txt", score + "\n");
                }

            }
            else
            {
                Console.WriteLine("Sorry you lost :(");
                stopWatch.Stop();
            }

            Console.WriteLine("Do you want to restart game ?: y/n");
            string yn = Console.ReadLine();
            if (yn == "n")
            {
                loop = false;
            }
        }
    }
}

