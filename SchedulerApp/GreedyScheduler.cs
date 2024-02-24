using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerApp
{
    internal class GreedyScheduler : IScheduler
    {
        /// <summary>
        /// Schedules the tasks between a start and end time using a greedy algorithm.
        /// The tasks are first sorted by their duration and then by their deadline.
        /// The method then iterates through the tasks and adds a task to the schedule if it can be completed before its deadline and within the time interval.
        /// </summary>
        /// <param name="tasks">The list of tasks to be scheduled.</param>
        /// <param name="startTime">The start time of the scheduling interval.</param>
        /// <param name="endTime">The end time of the scheduling interval.</param>
        /// <returns>A list of tasks that represents the schedule.</returns>
        public IList<Task> Schedule(List<Task> tasks, int startTime, int endTime)
        {
            int interval = endTime - startTime;
            tasks.OrderBy(x => x.Duration).ThenBy(x => x.Deadline).ToList();
            var schedule = new List<Task>();
            int time = startTime;
            for (int i = 0; i < tasks.Count; i++)
            {
                if (time + tasks[i].Duration <= tasks[i].Deadline && time + tasks[i].Duration <= interval)
                {
                    schedule.Add(tasks[i]);
                }
            }
            return schedule;
        }
    }
}
