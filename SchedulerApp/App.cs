using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerApp
{
    internal class App
    {
        // The scheduler instance used for scheduling tasks.
        private IScheduler scheduler;

        /// <summary>
        /// Initializes a new instance of the App class.
        /// </summary>
        /// <param name="scheduler">The scheduler to be used for scheduling tasks.</param>
        public App(IScheduler scheduler)
        {
            this.scheduler = scheduler;
        }

        /// <summary>
        /// Creates a schedule for tasks read from an input file.
        /// </summary>
        /// <param name="startTime">The start time for the schedule.</param>
        /// <param name="endTime">The end time for the schedule.</param>
        public void CreateSchedule(int startTime, int endTime)
        {
            //Initialize list and file.
            var tasks = new List<Task>();
            string file = "input.txt";
            if (File.Exists(file))
            {
                string[] lines = {""};

                // Try to read all lines from the file.
                try
                {
                    lines = File.ReadAllLines(file);
                }
                catch(Exception e)
                {

                    // Print the exception message and return if reading fails.
                    Console.WriteLine(e.Message);
                    return;
                }


                // Process each line in the file.
                foreach (string ln in lines)
                {
                    string[] splitted = ln.Split(" ");
                    if (splitted.Length != 3)
                    {
                        Console.WriteLine("Invalid format. Please check input file.");
                        continue;
                    }

                    // Try to create a task from the line.
                    try
                    {
                        Task t = new Task(splitted[0], Int32.Parse(splitted[1]), Int32.Parse(splitted[2]));
                        tasks.Add(t);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    
                }

                // Schedule the tasks and generate the HTML output.
                var scheduled = scheduler.Schedule(tasks, startTime, endTime);
                var output = HTMLBuilder.ToHtmlTable(scheduled, startTime);

                // Write the output to a file.
                var sw = new StreamWriter("index.html");
                sw.Write(output);
                sw.Flush();
                sw.Close();
                sw.Dispose();
                Console.WriteLine("Schedule created.");
            }
            else
            {
                // Print a message if the file does not exist.
                Console.WriteLine("No such file!");
            }
        }
    }
}
