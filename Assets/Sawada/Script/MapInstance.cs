using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInstance : MonoBehaviour
{
    int _stageLevel = 0;
    void GenerateMap()
    {
        
    }
}

public struct SpawnPosState
{
    public Transform SpawnPos;
    public SpawnState State;
    public enum SpawnState
    {
        none = 0,
        used = 1
    }
}
