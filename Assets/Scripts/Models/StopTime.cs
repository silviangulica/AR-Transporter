using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StopTime
{
    public string trip_id;
    public int stop_id;
    public int stop_sequence;

    public StopTime(string trip_id, int stop_id, int stop_sequence)
    {
        this.trip_id = trip_id;
        this.stop_id = stop_id;
        this.stop_sequence = stop_sequence;
    }

    public override string ToString()
    {
        return string.Format("[StopTime: trip_id={0}, stop_id={1}, stop_sequence={2}]", trip_id, stop_id, stop_sequence);
    }
}