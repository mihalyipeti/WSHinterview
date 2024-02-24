using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulerApp
{
    internal class OptimalScheduler : IScheduler
    {
        private int interval = 0;
        private int startTime = 0;

        /// <summary>
        /// Schedules the tasks between a start and end time using an optimal scheduling algorithm.
        /// The tasks are first sorted by their duration and then by their deadline.
        /// The method then generates all permutations of the tasks and checks each one to find the optimal schedule.
        /// </summary>
        /// <param name="tasks">The list of tasks to be scheduled.</param>
        /// <param name="startTime">The start time of the scheduling interval.</param>
        /// <param name="endTime">The end time of the scheduling interval.</param>
        /// <returns>A list of tasks that represents the optimal schedule.</returns>
        public IList<Task> Schedule(List<Task> tasks, int startTime, int endTime)
        {
            this.interval = endTime - startTime;
            this.startTime = startTime;
            tasks.OrderBy(x => x.Duration).ThenBy(x => x.Deadline).ToList();
            var schedule = new List<Task>();
            var allSol = Permute(tasks, 0, tasks.Count - 1, new List<IList<Task>>());
            allSol.Sort((a, b) => a.Count.CompareTo(b.Count));
            return allSol.Last();
        }

        /// <summary>
        /// Generates all permutations of a list of tasks.
        /// </summary>
        /// <param name="tasks">The list of tasks to permute.</param>
        /// <param name="start">The start index for the permutation.</param>
        /// <param name="end">The end index for the permutation.</param>
        /// <param name="list">The list to store the permutations.</param>
        /// <returns>A list of all permutations of the tasks.</returns>
        private List<IList<Task>> Permute(List<Task> tasks, int start, int end, List<IList<Task>> list)
        {
            if (start == end)
            {
                //We have a possible solution.
                var res = new List<Task>(tasks);
                int exit = 0;
                while(exit != -1)
                {
                    //Check the solution.
                    exit = check(res);
                    if(exit != -1)
                    {
                        //Remove invalid task.
                        res.RemoveAt(exit);
                    }
                }
                list.Add(res);
            }
            else
            {
                for (var i = start; i <= end; i++)
                {
                    //Next permutation.
                    Swap(tasks, start, i);
                    Permute(tasks, start + 1, end, list);
                    Swap(tasks, start, i);
                }
            }

            return list;
        }

        /// <summary>
        /// Swaps two tasks in a list.
        /// </summary>
        /// <param name="list">The list of tasks.</param>
        /// <param name="idxA">The index of the first task to swap.</param>
        /// <param name="idxB">The index of the second task to swap.</param>
        private void Swap(IList<Task> list, int idxA, int idxB)
        {
            var temp = list[idxA];
            list[idxA] = list[idxB];
            list[idxB] = temp;
        }

        /// <summary>
        /// Checks a schedule of tasks to see if it is valid.
        /// A schedule is valid if all tasks can be completed before their deadline and within the scheduling interval.
        /// </summary>
        /// <param name="tasks">The schedule of tasks to check.</param>
        /// <returns>The index of the first invalid task in the schedule, or -1 if the schedule is valid.</returns>
        private int check(List<Task> tasks) 
        {
            int time = this.startTime;
            foreach (var task in tasks)
            {
                if(time + task.Duration > task.Deadline || time + task.Duration > interval)
                {
                     return tasks.IndexOf(task);
                }
                time += task.Duration;
            }
            return -1;
        }
    }
}
