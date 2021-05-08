using System;
using System.Collections.Generic;
namespace dasads
{
    class Program
    {
        public static Dictionary<string, int> playedCards = new Dictionary<string, int>();  //lagrar alla spelade kort
        public static Dictionary<string, int> allCards = new Dictionary<string, int>();     //lagrar alla möjliga kort som finns i leken

        static void Main(string[] args)     //logiken bör inte köras i main men jag orkar verkligen inte
        {
            string[] choices = { "hit", "stand" };  //ger de olika valen spelaren har
            int currentValue = 0;   //lagrar spelarens totala värde
            int dealerValue = 0;    //lagrar dealerns totala värde
            Card.AssignCards();     //skapar kortleken
            Card firstDealerCard = new Card(dealerValue);   //skapar dealerns visade kort
            Card dealerHiddenCard = new Card(dealerValue);  //skapar dealerns gömda kort
            string playerLineup = "|";  //strängen som beskriver spelarens kort
            string dealerLineup = "| "; //strängen som beskriver dealerns kort

            dealerLineup += firstDealerCard.Suit + "| "; //lagrar dealerns första kort

            //player
            while (true)    //while loop som kör tills den bryts
            {
                Card newCard = new Card(currentValue);  //skapar ett nytt kort
                playerLineup += newCard.Suit + "| ";    //uppdaterar spelarens kort
                currentValue += newCard.CardValue;      //uppdaterar spelarens poäng
                System.Console.WriteLine($"Dealer's revealed card is {firstDealerCard.Suit}");
                System.Console.WriteLine($"Your new card is {newCard.Suit}");
                System.Console.WriteLine($"Your cards are {playerLineup}");
                //^^ beskriver de olika korten som dras och har dragits
                Console.ReadKey();  //väntar på att man klickar på en tangent
                if (currentValue > 21)  //kollar ifall spelaren bustar
                {
                    System.Console.WriteLine("Bust");
                    break;  //bryter loopen
                }
                if (Selection(choices, $"Hit or stand? Your lineup is: {playerLineup} and the dealer's revealed card is {firstDealerCard.Suit}") == 1)  //ställer frågan ifall spelaren fill stanna eller hit:a  
                {
                    break;  //bryter loopen ifall de vill stanna
                }
                Console.Clear();    //rensar konsollen för pretty reasons
            }

            //dealer
            dealerValue = firstDealerCard.CardValue + dealerHiddenCard.CardValue;   //uppdaterar dealerns poäng
            dealerLineup += dealerHiddenCard.Suit + "| ";   //uppdaterar dealerns kort
            System.Console.WriteLine($"Dealer flipped his hidden card revealing {dealerHiddenCard.Suit}");
            System.Console.WriteLine($"Dealer's cards: {dealerLineup}");
            //^^ beskriver dealerns kort
            Console.ReadKey();  //väntar på ett tangentryck.
            while (true)    //while loop som körs tills den bryts
            {
                if (dealerValue < 17)   //ifall dealerns totala värde är under 17 hit:ar den annars bryter den loopen
                {
                    Card extraDealerCard = new Card(dealerValue);   //skapar ettt nytt kort
                    dealerValue += extraDealerCard.CardValue;   //uppdaterar dealerns poäng
                    dealerLineup += extraDealerCard.Suit + "| ";    //uppdaterar dealerns kort
                    System.Console.WriteLine($"Dealer flipped {extraDealerCard.Suit}");
                    System.Console.WriteLine($"Dealer's cards: {dealerLineup}");
                    //^^beskriver dealerns kort
                    Console.ReadKey();  //väntar på ett tangenttryck
                }
                else
                {
                    break;      //bryter loopen
                }
            }

            System.Console.WriteLine($"game ended with player: {currentValue} and dealer: {dealerValue}");      //beskriver slutpoängen
            Console.ReadKey();  //väntar på tangenttryck igen

            //Här skriver du win conditions


        }

        //Nedan följer en mer avancerad selection algoritm än att ta råa strings från användaren
        public static void PrintChoices(string[] choices, int current, string q)    //printar valen och markerar den som är hovrad
        {
            System.Console.WriteLine(q);
            for (int i = 0; i < choices.Length; i++)
            {
                if (current == i)
                {
                    System.Console.WriteLine($">  {choices[i]}");
                }
                else
                {
                    System.Console.WriteLine($"  {choices[i]} ");
                }
            }
        }
        public static int Selection(string[] choices, string q) //logiken
        {
            int current = 0;    //skapar en variabel som håller koll på spelarens val
            while (true)    //loopar tills den bryts
            {
                Console.Clear();    //rensar konsollen
                PrintChoices(choices, current, q);  //printar de nya valen
                ConsoleKeyInfo key = Console.ReadKey(true);     //lagrar ett tangenttryck

                //kollar vilken knapp som trycktes
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        {
                            current++;  //ökar spelarens val
                            current = current % choices.Length;     //ser till att spelaren inte kan flytta markören utanför valen
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        {
                            current--;  //minskar spelarens val
                            if (current < 0) { current = choices.Length - 1; }  //ser till att spelaren inte kan flytta markören utanför valen
                            else
                            {
                                current = Math.Abs(current % choices.Length);
                            }
                        }
                        break;
                    case ConsoleKey.Enter:  //kollar ifall spelaren klickar enter och returnerar valet
                        {
                            return current; //när man returnerar bryts också loopen
                        }
                    default:
                        {
                            // handle everything else
                        }
                        break;
                }
            }
        }

    }
}
