using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WarGame
{
    public class CardWars
    {

        static Random rando = new Random();
        static List<string> Shuffle(List<string> cards)
        {
            return cards.OrderBy(x => rando.Next()).ToList();
        }

        private static int CompareCards(string card1, string card2)
        {
            int pValue = GetCardValue(card1);
            int cValue = GetCardValue(card2);
            if (pValue > cValue)
                return 1;//player wins
            else if (pValue < cValue)
                return -1;//npc wins 

            return 0;
        }

        private static int GetCardValue(string card)
        {
            int value;
            if (card.Length == 3) //10 card
                value = 10;
            else
            {
                string face = card[0].ToString();
                if (face == "A")
                    value = 1;
                else if (face == "J")
                    value = 11;
                else if (face == "Q")
                    value = 12;
                else if (face == "K")
                    value = 13;
                else
                    value = int.Parse(face);
            }

            return value;
        }

        public List<string> LoadCards(string filePath)
        {
            List<string> cardList = new List<string>();
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] cards = line.Split(';');
                foreach (string card in cards)
                {
                    cardList.Add(card.Trim());
                }
            }
            return cardList;
        }

        public static void PlayGame(List<string> cards, List<HighScore> highScores, string highScoreFile)
        {
            List<string> shuffledCards = Shuffle(cards);
            List<string> playerCards = shuffledCards.GetRange(0, shuffledCards.Count / 2);
            List<string> npcCards = shuffledCards.GetRange(shuffledCards.Count / 2, shuffledCards.Count / 2);

            List<string> playerPile = new List<string>();
            List<string> npcPile = new List<string>();
            List<string> unclaimedPile = new List<string>();

            while (playerCards.Count > 0)
            {
                string playerCard = playerCards[0];
                string npcCard = npcCards[0];
                Console.WriteLine($"\nPlayer: {playerCard} ----- NPC: {npcCard}");

                unclaimedPile.Add(playerCard);
                unclaimedPile.Add(npcCard);

                int result = CompareCards(playerCard, npcCard);

                if (result == -1)
                {
                    npcPile.AddRange(unclaimedPile);
                    unclaimedPile.Clear();
                    Console.WriteLine("NPC Wins!");
                }
                else if (result == 1)
                {
                    playerPile.AddRange(unclaimedPile);
                    unclaimedPile.Clear();
                    Console.WriteLine("Player Wins!");

                }
                else
                {
                    Console.WriteLine("Tie");
                }

                playerCards.RemoveAt(0);
                npcCards.RemoveAt(0);
            }

            Console.WriteLine($"Player has {playerPile.Count} cards and NPC has {npcPile.Count} cards");

            if (npcPile.Count > playerPile.Count)
            {
                Console.WriteLine("NPC Wins the round!");
            }
            else if (playerPile.Count == npcPile.Count)
            {
                Console.WriteLine("It's a tie!");
            }
            else
            {
                Console.WriteLine("Player Wins the round!");
                if (playerPile.Count > highScores[highScores.Count - 1].Score)
                {
                    Console.WriteLine("It's a new high score!");
                    Console.Write("Enter your name: ");
                    string name = Console.ReadLine();
                    HighScore highScore = new HighScore { Score = playerPile.Count, Name = name };
                    highScores.Add(highScore);

                    for (int i = 0; i < highScores.Count; i++)
                    {
                        if (highScore.Score >= highScores[i].Score)
                        {
                            highScores.Insert(i, highScore);
                            highScores.RemoveAt(highScores.Count - 1);
                            break;
                        }
                    }
                    HighScore saveHighScore = new HighScore { };
                    saveHighScore.SaveHighScores(highScoreFile, highScores);
                }
            }
        }
    }
}
