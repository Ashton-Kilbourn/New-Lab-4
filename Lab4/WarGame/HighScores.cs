using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace WarGame
{
    public class HighScore
    {
        //A-1
        public string Name { get; set; }
        public int Score { get; set; }

        //A-2
        public static List<HighScore> LoadHighScores(string filePath)
        {
            List<HighScore> highScores = new List<HighScore>();

            if (File.Exists(filePath))
            {
                using (StreamReader file = new StreamReader(filePath))
                {
                    Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
                    highScores = (List<HighScore>)serializer.Deserialize(file, typeof(List<HighScore>));
                }
            }

            return highScores;
        }

        //A-3
        public void SaveHighScores(string filePath, List<HighScore> highScores)
        {
            string jsonString = JsonConvert.SerializeObject(highScores);
            File.WriteAllText(filePath, jsonString);
        }

        //A-4
        public void ShowHighScores(List<HighScore> highScores)
        {
            Console.WriteLine("High Scores\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("{0,-15}{1,-10}\n", "Name", "Score");

            foreach (HighScore highScore in highScores)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("{0,-15}{1,-10}", highScore.Name, highScore.Score);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
