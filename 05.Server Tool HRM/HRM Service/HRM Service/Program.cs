using System.ServiceProcess;

namespace HRMServices
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new HRMService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
