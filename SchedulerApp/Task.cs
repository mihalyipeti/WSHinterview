using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerApp
{
    internal class Task : IComparable<Task>
    {
        /// <summary>
        /// Constructs a new instance of the 'Task' class.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        /// <param name="duration">The duration of the task.</param>
        /// <param name="deadline">The deadline of the task.</param>
        public Task(string name, int duration, int deadline)
        {
            Name = name;
            Duration = duration;
            Deadline = deadline;
        }

        public string Name { get; set; }
        public int Duration { get; set; }
        public int Deadline { get; set; }

        /// <summary>
        /// Compares the current task with another task based on their deadlines.
        /// </summary>
        /// <param name="other">The other task to compare with.</param>
        /// <returns>A value that indicates the relative order of the tasks being compared. The return value has these meanings: Less than zero: This task is earlier than the other task. Zero: This task is the same as the other task. Greater than zero: This task is later than the other task.</returns>
        public int CompareTo(Task? other)
        {
            if (other == null)
            {
                return 1;
            }
            return this.Deadline.CompareTo(other.Deadline);
        }

    }
}
