using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
using System.Threading;
using System.ComponentModel;
using System.Diagnostics;
using static BL.BL;

namespace BL
{
    internal class Simulator
    {
        const double speedToSecond = 1;

        Stopwatch stopwatch;

        Simulator(BL bl, int idDrone, Action update, Func<bool> checkStop)
        {
            


        while(checkStop() == true)
            {

                


            }
            if (Worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    // Perform a time consuming operation and report progress.
                    Thread.Sleep(500);
                    if (Worker.WorkerReportsProgress == true)
                        Worker.ReportProgress(i * 100 / length);
                }
            }




        }






    }
}
