using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerApp
{
    internal class UI
    {
        /// <summary>
        /// Prompts the user to select a scheduling strategy.
        /// The method lists all types that implement the 'IScheduler' interface and asks the user to choose one.
        /// If the user's choice is valid, an instance of the chosen type is created and returned.
        /// If the user's choice is not valid, an instance of 'OptimalScheduler' is created and returned.
        /// </summary>
        /// <returns>An instance of a class that implements the 'IScheduler' interface.</returns>
        public IScheduler SelectStrategy()
        {
            Console.WriteLine("Which strategy do you want to use?");
            Console.WriteLine();
            
            //Look for types that implement the IScheduler interface.
            var type = typeof(IScheduler);
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => type.IsAssignableFrom(p) && !p.IsInterface);
            int i = 1;
            foreach (var t in types)
            {
                if (t != null)
                {
                    Console.WriteLine(i + ". " + t.Name);
                    i++;
                }
            }
            
            //User input. If the number is out of range or the formant is incorrect, read again.
            string? s = "";
            int choice = 0;
            while (choice > types.Count() || choice <= 0)
            {
                try
                {
                    s = Console.ReadLine();
                    if (s != null && s != "")
                        choice = Int32.Parse(s);
                }
                catch(FormatException e)
                {
                    Console.WriteLine(e.Message);
                }
                
            }
            Console.WriteLine("Chosen strategy: " + choice);
            var instance = (IScheduler?)Activator.CreateInstance(types.ElementAt(choice - 1));
            if (instance != null)
                return instance;
            return new OptimalScheduler();
        }

        /// <summary>
        /// Selects the start time for scheduling tasks.
        /// This method currently returns a fixed start time of 8.
        /// </summary>
        /// <returns>The start time for scheduling tasks.</returns>
        public int SelectStartTime()
        {
            return 8;
        }

        /// <summary>
        /// Selects the end time for scheduling tasks.
        /// This method currently returns a fixed end time of 22.
        /// </summary>
        /// <returns>The end time for scheduling tasks.</returns>
        public int SelectEndTime()
        {
            return 22;
        }
    }
}
