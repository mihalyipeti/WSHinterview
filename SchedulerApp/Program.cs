namespace SchedulerApp
{
    class Program
    {
        public static void Main()
        {
            //Create components and run the application.
            var UI = new UI();
            var app = new App(UI.SelectStrategy());
            var startTime = UI.SelectStartTime();
            var endTime = UI.SelectEndTime();
            app.CreateSchedule(startTime, endTime);
        }
    }
}
