using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Trip
{

    public int direction_id;
    public int route_id;
    public string trip_id;
    public string trip_headsign;
    public int block_id;
    public string shape_id;

    public Trip(int direction_id, int route_id, string trip_id, string trip_headsign, int block_id, string shape_id)
    {
        this.direction_id = direction_id;
        this.route_id = route_id;
        this.trip_id = trip_id;
        this.trip_headsign = trip_headsign;
        this.block_id = block_id;
        this.shape_id = shape_id;
    }

    public override string ToString()
    {
        return "Trip: " + trip_id + " " + trip_headsign;
    }
}