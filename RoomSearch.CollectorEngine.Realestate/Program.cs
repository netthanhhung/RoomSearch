using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;


namespace RoomSearch.CollectorEngine.Realestate
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.Log.Debug("Initializing Realestate Services...");
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            {
                new CollectorService()
            };
            ServiceBase.Run(ServicesToRun);

            //KenhNhaTroJob job = new KenhNhaTroJob();
            //job.Execute(null);

            //BatDongSanJob job = new BatDongSanJob();
            //job.Execute(null);

            //RongBayJob job = new RongBayJob();
            //job.Execute(null);

        }
    }
}
