using HeroesV2.Tests;
using Newtonsoft.Json;
using PG2Input;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using WarGame;

namespace WarGame
{
    //New Changes
    /*  Lab Video   
      
        Here's a video showing what the lab could look like when completed:
        https://web.microsoftstream.com/video/504e89cc-ee22-43e2-ab1c-34d5a25483c6

    */
    internal class Program
    {
        static void Main(string[] args)
        {
            //CardGenerator.Generate();

            string userName = string.Empty;
            Input.GetString("What is your name? ", ref userName);
            Console.Clear();

            List<string> menu = new List<string>() { "1. Show High Scores", "2. Play Card Wars", "3. Exit", "\n4. Run Lab Tests\n" };

            string highScoreFile = "HighScores.json";
            string cardsFile = "cards.csv";

            //--------------------------------------------------------------------------
            //  PART A: The HighScore class
            //
            // PART A-1: HighScore class
            //      Create a HighScore class
            //      properties: Name, Score
            //

            //
            // PART A-2: LoadHighScores
            //      Add a LoadHighScores method to the HighScore class. (do not define the method inside of Main)
            //      It should have a string parameter for the file path.
            //      In the method, it should deserialize the file into a List<HighScore>.
            //      Return the list.
            //
            //  In Main (here), before the while loop,
            //  call LoadHighScores passing the highScoreFile variable and store the list it returns into a variable to be used later. 
            //

            List<HighScore> highScores = HighScore.LoadHighScores(highScoreFile);

            //
            // PART A-3: SaveHighScores
            //  Add a SaveHighScores method to the HighScore class.
            //  It should have a string parameter for the file path and a List<HighScore> parameter.
            //  In the method, it should serialize the list of high scores to the file.
            //  Call this method in the game (part B-2) when a player gets a new high score.
            //

            //
            // PART A-4: Show High Scores
            //     Add a ShowHighScores method to the HighScore class. It should have a List<HighScore> parameter. 
            //     It should print a "High Scores" title then loop over the high scores list and print each item.
            //     Format the output so that the scores are right-aligned and have a color different than the name.
            //     See example screenshot in the lab document in the Solution Items in Solution Explorer.
            //
            // In case 1 of the menu switch in Main in Program.cs, call the ShowHighScores method and pass the list of highscores.
            //
            //--------------------------------------------------------------------------


            //--------------------------------------------------------------------------
            //  PART B: CardWars Class
            //  Use the provided CardWars class in the CardWars.cs file.
            //
            // PART A-3: LoadCards
            //      Add a LoadCards method to the CardWars class. (do not define the method inside of Main)
            //      It should have a string parameter for the file path.
            //      In the method, it should read the csv file, split the data into a List<string>.
            //      Return the list.
            //
            //  In Main (here), before the while loop,
            //  call LoadCards passing the cardsFile variable and store the list it returns into a variable to be used later. 
            //
            CardWars cardGame = new CardWars();
            HighScore highScore_ = new HighScore();
            List<string> cards = cardGame.LoadCards(cardsFile);

            int part;
            string input;
            int selection;
            do
            {
                Console.Clear();
                Console.WriteLine($"Hello {userName}. Welcome to Card Wars");
                Input.GetMenuChoice("Choice? ", menu, out selection);

                switch (selection)
                {
                    case 1:
                        //
                        // call the ShowHighScores method and pass the list of highscores.
                        // 

                        HighScore highScore = new HighScore();
                        highScore.ShowHighScores(highScores);
                        break;

                    case 2:
                        //
                        // PART B-2: Play War Game
                        //      Add a PlayGame method to the CardWars class.
                        //      It should have 3 parameters: List<string> for the cards, List<HighScore> for the high scores, and a string for the name of the high score file.
                        //
                        //1.Call shuffle passing in the list of cards.
                        //2.Take the shuffled list and split it into 2 equal lists: playerCards and npcCards.
                        //3.Create 3 lists: playerPile, npcPile, unclaimedPile.
                        //4.Loop while the playerCards list is not empty
                        //  a.Print out the first card from playerCards and npcCards (see example in the lab doc).
                        //  b.Add the first card from playerCards and npcCards to the unclaimed pile.                          
                        //  c.Call CompareCards and pass the first card from the playerCards and npcCards.
                        //      i.NOTE: CompareCards will return -1 if the card1 < card2, 0 if card1 = card2, 1 if card1 > card2
                        //  d.If CompareCards returns - 1, add the unclaimed pile to the npcPile.Clear the unclaimed pile. Print NPC wins.
                        //  e.If CompareCards returns 1, add the unclaimed pile to the playerPile.Clear the unclaimed pile. Print player wins.
                        //  f.Remove the first card from the playerCards and npcCards.
                        //5.After the loop, check who won. Print the counts from the playerPile and npcPile lists.
                        //  a.If the npcPile has more cards, print that the npc won the round.
                        //  b.If the npcPile has the same number of cards as the playerPile, print that it was a tie.
                        //  c.Else, the playerPile has more cards.Print out that the player won and check if it’s a new high score.
                        //      i.NOTE: the last score in the high score list is the smallest high score. Therefor, if the playerPile count is greater than the last score in the high score list, the player has a new high score.
                        //      ii.If a new high score,
                        //          1.Get the user’s name using Input.GetString
                        //          2.loop from the beginning of the high score list
                        //          3.If the score is >= the high score, then
                        //              a.insert the high score into the list at that index
                        //              b.remove the last score in the list
                        //              c.call SaveHighScores(see part A-3)

                        //
                        //In case 2 of the menu switch in Main, call PlayGame to play a game of war!
                        //

                        //CardWars game = new CardWars();
                        //List<string> cards_ = CardWars.LoadCards(cardsFile);
                        CardWars.PlayGame(cards, highScores, userName);

                        //HighScore highScore_ = new HighScore();
                        highScore_.SaveHighScores(highScoreFile, highScores);
                        break;

                    case 3:
                        return;
                    case 4:
                        LabTests.RunTests();
                        break;
                }
                Console.ReadKey();
            } while (true);
        }

    }
}