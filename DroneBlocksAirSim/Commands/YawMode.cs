using MessagePack;

namespace DroneBlocksAirSim
{
    [MessagePackObject(keyAsPropertyName: true)]
    public class YawMode
    {
        public bool is_rate { get; set; }
        public int yaw_or_rate { get; set; }
    }
}
