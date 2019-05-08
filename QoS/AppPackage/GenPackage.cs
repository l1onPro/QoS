using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace QoS.AppPackage
{
    class GenPackage
    {
        List<Package> packages;
        DispatcherTimer timerGen;

        public GenPackage()
        {
            packages = new List<Package>();
        }
        
        private void StartTimer()
        {
            timerGen = new DispatcherTimer();
            timerGen.Interval = new TimeSpan();
            timerGen.Tick += new EventHandler(Move);
            timerGen.Start();
        }

        private void StopTimer()
        {
            timerGen.Stop();
        }

        private void Move(object sender, EventArgs e)
        {

        }
    }
}
