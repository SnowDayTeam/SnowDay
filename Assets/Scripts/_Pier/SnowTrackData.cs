﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SnowTrackData 
{
    public Transform transform;
    [Range(0, 2)]
    public float _brushSize;
    [Range(0, 1)]
    public float _brushStrength;

    public bool active;
}