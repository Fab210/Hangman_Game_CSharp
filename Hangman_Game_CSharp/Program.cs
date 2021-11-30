using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AsciiArt;

namespace Hangman_Game_CSharp
{
    class Program
    {

        static void ShowWord(string word, List<char> letters)
        {

            for (int i = 0; i < word.Length; i++)
            {

                char letter = word[i];

                if (letters.Contains(letter))
                {
                    Console.Write(word[i]);
                }
                else
                {
                    Console.Write("_ ");
                }



            }

        }

        static char AskLetter()
        {
            Console.WriteLine();
            Console.Write("Choose 1 Letter please :");
            Console.WriteLine();
            string letter = Console.ReadLine();
            letter = letter.ToUpper();
            bool isNumeric = int.TryParse(letter, out _);

            while (letter.Length != 1 || isNumeric == true)
            {
                Console.Write("Choose only 1 Letter :");

                letter = Console.ReadLine();
                letter = letter.ToUpper();
                isNumeric = int.TryParse(letter, out _);
            }
            char oneLetter = Convert.ToChar(letter);

            return oneLetter;
        }

        static bool AllLettersFound(string word, List<char> letters)
        {
            //string wordToFind = "";
            for (int i = 0; i < letters.Count; i++)
            {
                if (word.Contains(letters[i]))
                {
                    word = word.Replace(letters[i].ToString(), "");
                }
            }

            if (word.Length == 0)
            {
                return true;

            }
            else
            {
                return false;
            }

        }


        static void GuessWord(string word)
        {
            List<char> listOfLetters = new List<char>();
            List<char> lettersNotInWord = new List<char>();
            const int NB_LIFES = 6;
            int nbOfLifes = NB_LIFES;
            while (nbOfLifes > 0)
            {
                Console.WriteLine(Ascii.PENDU[NB_LIFES - nbOfLifes]);
                Console.WriteLine();
                ShowWord(word, listOfLetters);
                Console.WriteLine(" ");
                char letter = AskLetter();

                Console.Clear();
                if (word.Contains(letter))
                {
                    if (listOfLetters.Contains(letter))
                    {
                        Console.WriteLine("Letter already selected : " + letter);
                    }
                    else
                    {
                        listOfLetters.Add(letter);
                        Console.WriteLine(" ");
                        Console.WriteLine("** You found a letter **");
                        Console.WriteLine(" ");
                        bool win = AllLettersFound(word, listOfLetters);
                        if (win == true)
                        {
                            Console.WriteLine(" ");
                            Console.WriteLine("YOU WON !!!");
                            break;
                        }
                    }

                }
                else
                {

                    if (lettersNotInWord.Contains(letter))
                    {
                        Console.WriteLine("");
                        Console.WriteLine("NUMBER of Lifes : " + nbOfLifes);
                        Console.WriteLine("Letter already selected : " + letter);

                    }
                    else
                    {
                        nbOfLifes--;
                        lettersNotInWord.Add(letter);
                        Console.WriteLine("");
                        Console.WriteLine("NUMBER of Lifes : " + nbOfLifes);

                    }

                }
                if (lettersNotInWord.Count > 0)
                {
                    Console.WriteLine();
                    Console.WriteLine(" *** The word doesn t contain this letters : " + String.Join(", ", lettersNotInWord));
                    Console.WriteLine();
                }




            }

            if (nbOfLifes == 0)
            {
                Console.WriteLine(Ascii.PENDU[NB_LIFES - nbOfLifes]);
                Console.WriteLine(" ||| YOU LOOSE ||| The Word was : " + word);

            }


        }

        static string[] LoadWords(string fileName)
        {
            try
            {
                return File.ReadAllLines(fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on reading word file " + fileName + "( " + ex.Message + " )");
            }

            return null;

        }

        static bool AskReplay()
        {
            Console.WriteLine();
            Console.Write("Do you want to replay ? Y : Yes / N : No");
            Console.WriteLine();
            string answer = Console.ReadLine();
            answer = answer.ToUpper();
            if (answer == "Y")
            {
                return true;

            }
            if (answer == "N")
            {
                return false;
            }
            else
            {
                return AskReplay();
            }
        }

        static void StartGame()
        {
            var words = LoadWords("mots.txt");

            if ((words == null || words.Length == 0))
            {
                Console.WriteLine("List of words is empty ");
            }
            else
            {
                Random rd = new Random();
                int randomNum = rd.Next(words.Length);
                string word = words[randomNum].Trim().ToUpper();
                GuessWord(word);
                bool replay = AskReplay();
                if (replay == true)
                {
                    Console.Clear();
                    StartGame();
                }
            }
        }



        static void Main(string[] args)
        {
            StartGame();
        }
    }
}
