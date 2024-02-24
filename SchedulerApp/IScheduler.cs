using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerApp
{
    internal interface IScheduler
    {
        public IList<Task> Schedule(List<Task> tasks, int startTime, int endTime);
    }
}
