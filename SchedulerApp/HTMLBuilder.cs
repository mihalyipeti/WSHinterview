using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerApp
{
    internal static class HTMLBuilder
    {
        /// <summary>
        /// Converts a list of tasks into an HTML table.
        /// The table includes columns for the task name, start time, and end time.
        /// The start time for the first task is provided as an argument.
        /// </summary>
        /// <param name="tasks">The list of tasks to include in the table.</param>
        /// <param name="start">The start time for the first task, in hours since midnight.</param>
        /// <returns>A string containing the HTML for the table.</returns>
        public static string ToHtmlTable(IList<Task> tasks, int start)
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine("<html><head><link rel=\"stylesheet\" type=\"text/css\" href=\"" + "taskstyle.css" + "\"></head><body><table>");
            html.AppendLine("<tr><th>Task</th><th>Start Time</th><th>End Time</th></tr>");

            DateTime startTime = DateTime.Today.AddHours(start);

            //Generate a row for each task.
            foreach (var task in tasks)
            {
                DateTime endTime = startTime.AddHours(task.Duration);

                html.AppendLine($"<tr><td>{task.Name}</td><td>{startTime:HH:mm}</td><td>{endTime:HH:mm}</td></tr>");

                startTime = endTime;
            }

            html.AppendLine("</table></body></html>");
            return html.ToString();
        }
    }
}
