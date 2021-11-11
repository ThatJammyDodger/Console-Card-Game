using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace CSHARP
{
    class main
    {
        static void Main()
        {

            Console.WriteLine("Welcome to the game of card-based luck!\n___________________________________\n");


            using (var reader = new StreamWriter(@"winners.csv"))
            {
                reader.WriteLine("Simon Cowell,30");
                reader.WriteLine("Dev,26");
                reader.WriteLine("Dr Who,16");
                reader.WriteLine("Mr Bean,20");
                reader.WriteLine("Dev's friend,16");
            }

 




            string username1;
            string username2;
            string password1;
            string password2;

            cards.setDeck();
            cards.shuffleDeck();

            string[] players = new string[2];







            do
            {
                Console.Write("Enter username for player 1: ");
                username1 = Console.ReadLine();

                Console.Write("Enter password for player 1: ");
                password1 = Console.ReadLine();

                Console.Write("\nEnter username for player 2: ");
                username2 = Console.ReadLine();

                Console.Write("Enter password for player 2: ");
                password2 = Console.ReadLine();

                if (login.auth(username1, password1) == false)
                {
                    Console.WriteLine("Login failure for player 1. Please try again.\n\n");
                }
                if (login.auth(username2, password2) == false)
                {
                    Console.WriteLine("Login failure for player 2. Please try again.\n\n");
                }
                if (username1 == username2)
                {
                    Console.WriteLine("Login failure: both players have used the same credentials. Please try again");
                }

            } while ((login.auth(username1, password1) == false) || (login.auth(username2, password2) == false) || username1 == username2);






            Console.Write("\n\nEnter display name for player 1: ");
            players[0] = Console.ReadLine();

            Console.Write("Enter display name for player 2: ");
            players[1] = Console.ReadLine();

            Console.WriteLine("\nLogged in successfully!");








            void showStats()
            {
                Console.WriteLine("\n\t===================================");
                Console.WriteLine($"{players[0]} has {cards.Player1Cards.Count} cards, having won {cards.Player1Cards.Count / 2} rounds out of 15.");
                Console.WriteLine($"{players[1]} has {cards.Player2Cards.Count} cards, having won {cards.Player2Cards.Count / 2} rounds out of 15.");
                Console.WriteLine($"There are {cards.Deck.Count / 2} rounds left.");
                Console.WriteLine("\t===================================\n");
            }

            void play()
            {
                cards.reset();
                while (cards.Deck.Count > 0)
                {
                    showStats();

                    Console.WriteLine($"{players[0]}, type 'draw' to take a card.");
                    Console.ReadLine();
                    string player1card = cards.drawCard();
                    Console.WriteLine($"Your card is {player1card}. \n\nLet's give {players[1]} a go too now.");

                    Console.WriteLine($"{players[1]}, type 'draw' to take a card.");
                    Console.ReadLine();
                    string player2card = cards.drawCard();

                    Console.WriteLine($"Your card is {player2card}. \n\nLet me just calculate the winner now.");
                    int winner = cards.calculateWinner(player1card, player2card);
                    Console.WriteLine($"The winner of that round is {players[winner - 1]}.");

                    cards.giveCards(winner, player1card, player2card);
                    Console.WriteLine("\n\n");
                }



                int finalWinner = cards.getFinalWinner();




                Console.WriteLine("\nGAME OVER!\n\n__________________________________________________\n");
                Console.WriteLine($"The final winner, as you probably already know, is ... *drumroll* ... {players[finalWinner - 1]}.");
                Console.WriteLine("Their cards were:");





                int WinnerNoOfCards = 0;

                if (finalWinner == 1)
                {
                    foreach (string x in cards.Player1Cards)
                    {
                        Console.WriteLine($"\t{x}");
                    }
                    WinnerNoOfCards = cards.Player1Cards.Count;
                }
                else if (finalWinner == 2)
                {
                    foreach (string x in cards.Player2Cards)
                    {
                        Console.WriteLine($"\t{x}");
                    }
                    WinnerNoOfCards = cards.Player2Cards.Count;
                }







                using (var reader = File.AppendText(@"winners.csv"))
                {
                    reader.WriteLine($"{players[finalWinner - 1]},{WinnerNoOfCards}");
                }

                List<int> scores = new List<int>();
                List<string> names = new List<string>();

                using (var reader = new StreamReader(@"winners.csv"))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        int value;
                        int.TryParse(values[1], out value);
                        names.Add(values[0]);
                        scores.Add(value);
                    }
                }





                SortList(ref scores, ref names);



                Console.WriteLine("\nALL-TIME WINNERS:");
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine($"{i + 1}) {names[i]} with a score of {scores[i]}.");
                }


            }

            play();

            Console.ReadLine();



        }
    
        static void SortList(ref List<int> score, ref List<string> name)
        {
            int swaps = -1;
            while (swaps!=0)
            {
                swaps = 0;
                for (int i = 0; i <= score.Count - 2; i++)
                {
                    if (score[i+1] > score[i])
                    {
                        int t = score[i];
                        score[i] = score[i+1];
                        score[i+1] = t;

                        string s = name[i];
                        name[i] = name[i+1];
                        name[i+1] = s;
                        swaps ++;

                    }
                }
            }
        }
    }
}