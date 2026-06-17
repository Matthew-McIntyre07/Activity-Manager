namespace OOSkills_MatthewMcIntyre
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Store the location of Activities.CSV and prepare StreamReader to read the CSV later on
            string CSVPath = "Activities.CSV";
            StreamReader reader = null;

            //Our parallel arrays for CSV data to be stored inside of
            List<string> Type = new List<string>();
            List<string> DateStartTime = new List<string>();
            List<string> Title = new List<string>();
            List<float> Cost = new List<float>();
            List<string> Location = new List<string>();
            List<int> MinParticipants = new List<int>();

            //If the path to the CSV exists, create a new streamreader to read the CSV
            if (File.Exists(CSVPath))
            {
                reader = new StreamReader(File.OpenRead(CSVPath));
            }

            //storing all of the lines of the CSV for use when comparing
            string[] lines = File.ReadAllLines(CSVPath);

            //For every line inside of the CSV remove whitespace, also skipping the first line
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i].Trim();

                //if a line is empty, ignore it and continue
                if (string.IsNullOrEmpty(line)) continue;

                string[] fields = line.Split(',');

                //Append the data to their according variables, if empty store the variable as empty or as a value of 0
                Type.Add(fields.Length > 0 ? fields[0].Trim().ToLower() : "");
                DateStartTime.Add(fields.Length > 1 ? fields[1].Trim().ToLower() : "");
                Title.Add(fields.Length > 2 ? fields[2].Trim().ToLower() : "");
                Cost.Add(fields.Length > 3 && !string.IsNullOrEmpty(fields[3].Trim()) ? float.Parse(fields[3].Trim()) : 0f);
                Location.Add(fields.Length > 4 ? fields[4].Trim().ToLower() : "");
                MinParticipants.Add(fields.Length > 5 && !string.IsNullOrEmpty(fields[5].Trim()) ? int.Parse(fields[5].Trim()) : 0);
            }
            PrintActivities(Type, DateStartTime, Title, Cost, Location, MinParticipants);

            using (StreamWriter writer = new StreamWriter("FitnessActivities.CSV"))
            {
                writer.WriteLine("Type,Date/Start Time,Title,Cost,Location,Min Participants");
                for (int i = 0; i < Type.Count; i++)
                {       
                    if (Type[i] == "fitness")
                    {
                        writer.WriteLine($"{Type[i]},{DateStartTime[i]},{Title[i]},{Cost[i]},{Location[i]},{MinParticipants[i]}");
                    }
                }
                    
            }

            using (StreamWriter writer = new StreamWriter("EntertainmentActivities.CSV"))
            {
                writer.WriteLine("Type,Date/Start Time,Title,Cost,Location,Min Participants");
                for (int i = 0; i < Type.Count; i++)
                {
                    if (Type[i] == "entertainment")
                    {
                        writer.WriteLine($"{Type[i]},{DateStartTime[i]},{Title[i]},{Cost[i]},{Location[i]},{MinParticipants[i]}");
                    }
                }

            }
        }
        
        //Print all of the activities out from their variables
        static void PrintActivities(List<string> type, List<string> dateStartTime, List<string> title, List<float> cost, List<string> location, List<int> minParticipants)
        {
            Console.WriteLine("\n========== ACTIVITIES ==========\n");

            for (int i = 0; i < type.Count; i++)
            {
                Console.WriteLine($"Activity #{i + 1}");
                Console.WriteLine($"  Type: {type[i]}");
                Console.WriteLine($"  Date/Start Time: {dateStartTime[i]}");
                Console.WriteLine($"  Title: {title[i]}");
                Console.WriteLine($"  Cost: ${cost[i]:F2}");
                Console.WriteLine($"  Location: {location[i]}");
                Console.WriteLine($"  Min Participants: {minParticipants[i]}");
                Console.WriteLine();
            }
        }
    }
}
