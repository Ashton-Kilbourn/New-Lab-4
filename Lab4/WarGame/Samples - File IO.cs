using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01
{
    /*      File Input / Output      
             https://learn.microsoft.com/en-us/dotnet/standard/io/
    
            Vocabulary:

                Serializing: the process of converting an object into a stream of bytes to store the object or transmit it to memory, a database, or a file.
                    https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/serialization/

                Deserializing: the process of converting a stream of bytes to objects.
                    https://hazelcast.com/glossary/deserialization/ 


            Links:
                Serializing vs deserializing: https://stackoverflow.com/questions/3316762/what-is-deserialize-and-serialize-in-json
                  
                  
             Lecture videos:
                File I/O lecture;
                    https://fullsailedu-my.sharepoint.com/:v:/g/personal/ggirod_fullsail_com/EdYpySsTlvBPsXQjhUHJj58Bat0hq6vY-lrXswXBjKh5tA?e=HeXea9

                Splitting Strings lecture:
                    https://fullsailedu-my.sharepoint.com/:v:/g/personal/ggirod_fullsail_com/EQdvRQGWin1It3KDZxkBxf0BeMXHNSl5wurc6zSKbGeq0g?e=yxd0Ls



    */


    internal class FileIOSamples
    {

        public void Sample()
        {
            string directories = @"C:\temp\2302"; //use @ in front of the string to ignore escape sequences inside the string
            string fileName = "tempFile.txt";
            string filePath = Path.Combine(directories, fileName); //use Path.Combine to get the proper directory separators

            char delimiter = '\n';

            /* [  Writing a CSV file  ]

               Open the file with a using statement.
               
            */
            //1. Open the file
            using (StreamWriter sw = new StreamWriter(filePath)) //IDisposable
            {
                //2. write to the file
                sw.Write("Batman is the best!");
                sw.Write(delimiter);
                sw.Write(5);
                sw.Write(delimiter);
                sw.Write(420.13);
                sw.Write(delimiter);
                sw.Write(true);
            }//3. CLOSE THE FILE!  } will call Dispose() on the sw which will call Close on the file


            /* [  Reading a CSV file  ]

               Open the file with a using statement.
               
            */
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }




            /* [  Splitting a CSV string  ]

                The sample string below has 2 delimiters: ; and &
                Using a character array, call Split on the string.

                If the string has delimiters next to each other (EX: ;;),
                then use StringSplitOptions.RemoveEmptyEntries to remove the empty items that would have been added to the array
               
            */
            string csvString = "Batman;Bruce Wayne;;;Bats;The Dark Knight&&Joker&Riddler&Bane&Aquaman";
            string[] data = csvString.Split(new char[] { ';', '&' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in data)
            {
                Console.WriteLine(item);
            }




            List<Superhero> JLA = new List<Superhero>();
            JLA.Add(new Superhero() { Name = "Batman", Secret = "Bruce Wayne", SuperPower = Powers.Money });
            JLA.Add(new Superhero() { Name = "Superman", Secret = "Clark Kent", SuperPower = Powers.Flight | Powers.LazerEyes });
            JLA.Add(new Superhero() { Name = "Wonder Woman", Secret = "Diana Prince", SuperPower = Powers.Strength });
            JLA.Add(new Superhero() { Name = "Flash", Secret = "Barry Allen", SuperPower = Powers.Speed });
            JLA.Add(new Superhero() { Name = "Aquaman", Secret = "Arthur Curry", SuperPower = Powers.Swimming });


            /* [  Serialize  ]

                Saving objects to a file

                Use nested using statements with the Json.NET classes.
               
            */
            //SERIALIZE the state (data) of my objects
            string newFilePath = Path.ChangeExtension(filePath, ".json");

            using (StreamWriter sw = new StreamWriter(newFilePath))
            {
                using (JsonTextWriter jtw = new JsonTextWriter(sw))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Formatting = Formatting.Indented;
                    serializer.Serialize(jtw, JLA);
                }
            }

            //OR

            /* [  Serialize  ]

                Saving objects to a file

                Combine the statements into 1 line: 
                    1) get the Json string from JsonConvert.SerializeObject
                    2) pass the Json string to the File.WriteAllText method.
               
            */
            File.WriteAllText(newFilePath, JsonConvert.SerializeObject(JLA, Formatting.Indented));





            /* [  Deserialize  ]

                Recreating objects by loading the state from a file
               
                1) read the json text from the file. Use File.ReadAllText  https://stackoverflow.com/questions/7387085/how-to-read-an-entire-file-to-a-string-using-c
                2) using Json.NET, deserialize the string into the objects using JsonConvert.DeserializeObject. https://www.newtonsoft.com/json/help/html/DeserializeCollection.htm
                    T variable = JsonConvert.DeserializeObject<T>(jsonString)
                    
                    replace T with the type of the data that was stored in the file.
            */

            //check if the file exists before trying to read from it
            if (File.Exists(newFilePath))
            {
                try  //use a try-catch to handle possible exceptions
                {
                    string heroJson = File.ReadAllText(newFilePath);

                    List<Superhero> supes = JsonConvert.DeserializeObject<List<Superhero>>(heroJson);

                    foreach (var hero in supes)
                    {
                        Console.WriteLine($"Hello Citizen. I am {hero.Name} (aka {hero.Secret}). I can {hero.SuperPower}");
                    }
                }
                catch (UnauthorizedAccessException uae)
                {
                    Console.WriteLine("You do not have access, Hacker!");
                }
                catch (Exception)
                {
                    Console.WriteLine("HACKERS!! Grr.");
                }
            }
            else
            {
                Console.WriteLine("OOPS! Hackers deleted the file.");
            }
        }
    }


    //Masking, & (Ands) | (Ors)
    enum Powers
    {
        Money = 1, Flight = 2, LazerEyes = 4, Strength = 8, Speed = 16, Swimming = 32
    }

    class Superhero
    {
        public string Name { get; set; }
        public string Secret { get; set; }
        public Powers SuperPower { get; set; }
    }
}
