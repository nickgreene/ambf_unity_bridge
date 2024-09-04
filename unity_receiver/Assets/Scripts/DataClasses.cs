using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class ReceivedPose
{
    public string name;
    public Pose pose;
}

[Serializable]
public class Pose
{
    public Position position;
    public Orientation orientation;
}

[Serializable]
public class Position
{
    public float x;
    public float y;
    public float z;
}


[Serializable]
public class Orientation
{
    public float x;
    public float y;
    public float z;
    public float w;
}


