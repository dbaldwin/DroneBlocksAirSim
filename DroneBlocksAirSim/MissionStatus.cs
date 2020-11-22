using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneBlocksAirSim
{

    class MissionStatus
    {
        public bool IsMissionRunning { get; set; }
        public bool IsMissionComplete { get; set; }
        public int CurrentCommandIndex { get; set; }
        public int MissionCommandLength { get; set; }

        public MissionStatus()
        {
            this.IsMissionRunning = false;
            this.IsMissionComplete = false;
            this.CurrentCommandIndex = 0;
            this.MissionCommandLength = 0;
        }
    }
}
