using System;
using System.Collections.Generic;
namespace dasads
{
    public class Card
    {
        Random rand = new Random();
        public int CardValue
        { get; private set; }
        public static string[] suits = { "♣", "♦", "♥", "♠" };
        public static string[] values = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
        public string Suit { get; private set; }

        public Card(int currentValue)
        {
            bool success = false;
            while (!success)
            {

                if (Program.playedCards.Keys.Count == 52)
                {
                    System.Console.WriteLine("cards ran out");
                    Console.ReadKey();
                    Environment.Exit(1);
                }

                int value = rand.Next(values.Length);
                string suit = suits[rand.Next(suits.Length)];

                string relevantCard = suit + values[value];
                Suit = relevantCard;

                if (value <= 7)
                {
                    CardValue = int.Parse(values[value]);
                }
                else if (values[value] != "A")
                {
                    CardValue = 10;
                }
                else
                {
                    if (currentValue + 11 <= 21)
                    {
                        CardValue = 11;
                    }
                    else
                    {
                        CardValue = 1;
                    }
                }

                if (!Program.playedCards.ContainsKey(relevantCard))
                {
                    Program.playedCards.Add(relevantCard, CardValue);
                    success = true;
                }
            }

        }

        public static void AssignCards()
        {
            for (int i = 0; i < suits.Length; i++)
            {
                for (int j = 0; j < values.Length; j++)
                {
                    Program.allCards.Add(suits[i] + values[j], 1);
                }
            }
        }

    }
}
