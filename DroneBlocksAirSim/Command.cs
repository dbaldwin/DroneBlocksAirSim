﻿using MessagePack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneBlocksAirSim
{
    [MessagePackObject]
    public class Command
    {

        [Key(0)]
        public int Request { get; set; } // Should always be 0
        [Key(1)]
        public int MessageId { get; set; }
        [Key(2)]
        public string Method { get; set; }
        [Key(3)]
        public ArrayList args { get; set; }
        
    }
}
