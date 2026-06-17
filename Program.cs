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

            CheckAction();
        }
        

        static void CheckAction()
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\n========== MENU ==========");
                Console.WriteLine("Enter 1 to read and display all entertainment activities from EntertainmentActivities.CSV");
                Console.WriteLine("Enter 2 to add a new entertainment activity record to EntertainmentActivities.CSV");
                Console.WriteLine("Enter 3 to read and display all fitness activities from FitnessActivities.CSV");
                Console.WriteLine("Enter 4 to add a new fitness activity record to FitnessActivities.CSV");
                Console.WriteLine("Enter 5 to exit the program");
                Console.Write("Your choice: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 5.\n");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        ReadAndDisplayEntertainmentActivities();
                        break;
                    case 2:
                        AddNewEntertainmentActivity();
                        break;
                    case 3:
                        ReadAndDisplayFitnessActivities();
                        break;
                    case 4:
                        AddNewFitnessActivity();
                        break;
                    case 5:
                        Console.WriteLine("Exiting the program. Goodbye!");
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.\n");
                        break;
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

        //Read and display all entertainment activities from EntertainmentActivities.CSV
        static void ReadAndDisplayEntertainmentActivities()
        {
            string entertainmentCSVPath = "EntertainmentActivities.CSV";

            if (!File.Exists(entertainmentCSVPath))
            {
                Console.WriteLine($"\n{entertainmentCSVPath} does not exist.");
                return;
            }

            Console.WriteLine("\n========== ENTERTAINMENT ACTIVITIES ==========\n");

            string[] lines = File.ReadAllLines(entertainmentCSVPath);

            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i].Trim();

                if (string.IsNullOrEmpty(line)) continue;

                string[] fields = line.Split(',');

                if (fields.Length >= 6)
                {
                    Console.WriteLine($"Activity #{i}");
                    Console.WriteLine($"  Type: {fields[0].Trim()}");
                    Console.WriteLine($"  Date/Start Time: {fields[1].Trim()}");
                    Console.WriteLine($"  Title: {fields[2].Trim()}");
                    Console.WriteLine($"  Cost: ${fields[3].Trim()}");
                    Console.WriteLine($"  Location: {fields[4].Trim()}");
                    Console.WriteLine($"  Min Participants: {fields[5].Trim()}");
                    Console.WriteLine();
                }
            }
        }

        //Add a new entertainment activity record to EntertainmentActivities.CSV
        static void AddNewEntertainmentActivity()
        {
            string entertainmentCSVPath = "EntertainmentActivities.CSV";

            if (!File.Exists(entertainmentCSVPath))
            {
                Console.WriteLine($"\n{entertainmentCSVPath} does not exist.");
                return;
            }

            Console.WriteLine("\n========== ADD NEW ENTERTAINMENT ACTIVITY ==========\n");

            Console.Write("Enter Type (entertainment): ");
            string type = "entertainment";

            Console.Write("Enter Date/Start Time: ");
            string dateStartTime = Console.ReadLine().Trim().ToLower();

            Console.Write("Enter Title: ");
            string title = Console.ReadLine().Trim().ToLower();

            Console.Write("Enter Cost: ");
            float cost = float.TryParse(Console.ReadLine().Trim(), out float parsedCost) ? parsedCost : 0f;

            Console.Write("Enter Location: ");
            string location = Console.ReadLine().Trim().ToLower();

            Console.Write("Enter Min Participants: ");
            int minParticipants = int.TryParse(Console.ReadLine().Trim(), out int parsedParticipants) ? parsedParticipants : 0;

            using (StreamWriter writer = new StreamWriter(entertainmentCSVPath, append: true))
            {
                writer.WriteLine($"{type},{dateStartTime},{title},{cost},{location},{minParticipants}");
            }

            Console.WriteLine("\nNew entertainment activity added successfully!");
        }

        //Read and display all fitness activities from FitnessActivities.CSV
        static void ReadAndDisplayFitnessActivities()
        {
            string fitnessCSVPath = "FitnessActivities.CSV";

            if (!File.Exists(fitnessCSVPath))
            {
                Console.WriteLine($"\n{fitnessCSVPath} does not exist.");
                return;
            }

            Console.WriteLine("\n========== FITNESS ACTIVITIES ==========\n");

            string[] lines = File.ReadAllLines(fitnessCSVPath);

            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i].Trim();

                if (string.IsNullOrEmpty(line)) continue;

                string[] fields = line.Split(',');

                if (fields.Length >= 6)
                {
                    Console.WriteLine($"Activity #{i}");
                    Console.WriteLine($"  Type: {fields[0].Trim()}");
                    Console.WriteLine($"  Date/Start Time: {fields[1].Trim()}");
                    Console.WriteLine($"  Title: {fields[2].Trim()}");
                    Console.WriteLine($"  Cost: ${fields[3].Trim()}");
                    Console.WriteLine($"  Location: {fields[4].Trim()}");
                    Console.WriteLine($"  Min Participants: {fields[5].Trim()}");
                    Console.WriteLine();
                }
            }
        }

        //Add a new fitness activity record to FitnessActivities.CSV
        static void AddNewFitnessActivity()
        {
            string fitnessCSVPath = "FitnessActivities.CSV";

            if (!File.Exists(fitnessCSVPath))
            {
                Console.WriteLine($"\n{fitnessCSVPath} does not exist.");
                return;
            }

            Console.WriteLine("\n========== ADD NEW FITNESS ACTIVITY ==========\n");

            string type = "fitness";

            Console.Write("Enter Date/Start Time: ");
            string dateStartTime = Console.ReadLine().Trim().ToLower();

            Console.Write("Enter Title: ");
            string title = Console.ReadLine().Trim().ToLower();

            Console.Write("Enter Cost: ");
            float cost = float.TryParse(Console.ReadLine().Trim(), out float parsedCost) ? parsedCost : 0f;

            Console.Write("Enter Location: ");
            string location = Console.ReadLine().Trim().ToLower();

            Console.Write("Enter Min Participants: ");
            int minParticipants = int.TryParse(Console.ReadLine().Trim(), out int parsedParticipants) ? parsedParticipants : 0;

            using (StreamWriter writer = new StreamWriter(fitnessCSVPath, append: true))
            {
                writer.WriteLine($"{type},{dateStartTime},{title},{cost},{location},{minParticipants}");
            }

            Console.WriteLine("\nNew fitness activity added successfully!");
        }
    }
}
