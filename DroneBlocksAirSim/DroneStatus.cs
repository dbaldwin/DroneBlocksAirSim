using DroneBlocksAirSim.Commands;
using Newtonsoft.Json;
using System.Diagnostics;
using MessagePack;
using Newtonsoft.Json.Linq;

namespace DroneBlocksAirSim
{
    static class DroneStatus
    {
        private static TCP client;

        public static bool IsConnected { get; set; }
        public static bool IsFlying { get; set; }

        public static void GetState()
        {
            client = new TCP();

            byte[] response = client.Send(new MultirotorState().GetCommand(), 1024);
            dynamic json = JsonConvert.DeserializeObject(MessagePackSerializer.ConvertToJson(response));

            JToken parsed = JToken.Parse(MessagePackSerializer.ConvertToJson(response));
            JToken val = parsed[3]["landed_state"];
            IsFlying = (bool)val;

            // 0 is landed
            // 1 is flying
            Debug.WriteLine(IsFlying);

            client.Close();

        }
    }
}

// Sample response
/*
[1, 1, null, {
    "collision": {
        "has_collided": false,
        "penetration_depth": 0,
        "time_stamp": 0,
        "normal": {
            "x_val": 0,
            "y_val": 0,
            "z_val": 0
        },
        "impact_point": {
            "x_val": 0,
            "y_val": 0,
            "z_val": 0
        },
        "position": {
            "x_val": 0,
            "y_val": 0,
            "z_val": 0
        },
        "object_name": "",
        "object_id": -1
    },
    "kinematics_estimated": {
        "position": {
            "x_val": 0,
            "y_val": 0,
            "z_val": -0.1465039
        },
        "orientation": {
            "w_val": 0.9040833,
            "x_val": 0,
            "y_val": 0,
            "z_val": 0.4273564
        },
        "linear_velocity": {
            "x_val": 0,
            "y_val": 0,
            "z_val": 0
        },
        "angular_velocity": {
            "x_val": 0,
            "y_val": 0,
            "z_val": 0
        },
        "linear_acceleration": {
            "x_val": 0,
            "y_val": 0,
            "z_val": 0
        },
        "angular_acceleration": {
            "x_val": 0,
            "y_val": 0,
            "z_val": 0
        }
    },
    "gps_location": {
        "latitude": 47.638103597933,
        "longitude": -122.149805662166,
        "altitude": 322.8202
    },
    "timestamp": 1606053843482788352,
    "landed_state": 0,
    "rc_data": {
        "timestamp": 0,
        "pitch": 0,
        "roll": 0,
        "throttle": 0,
        "yaw": 0,
        "left_z": 0,
        "right_z": 0,
        "switches": 0,
        "vendor_id": "",
        "is_initialized": false,
        "is_valid": false
    }
}]*/
