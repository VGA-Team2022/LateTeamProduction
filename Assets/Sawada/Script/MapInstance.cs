using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInstance : MonoBehaviour
{
    MapElemantEntity _entity = null;
    Transform[] _instansTransform = null;

    public MapElemantEntity Entity => _entity;

    void GenerateMap()
    {
        SpawnPosState[] insPos = null;
        int[] houseValues = new int[] { _entity.HouseValue, _entity.HouseValueOnSolt, _entity.HouseValueInBaby, _entity.HouseValueInArrow };
        int houseValueSum = 0;

        _instansTransform = GetComponentsInChildren<Transform>();
        foreach(int v in houseValues)
        {
            houseValueSum += v;
        }
        for (int i = 0; i < houseValueSum; i++)
        {
            insPos[i] = new SpawnPosState(_instansTransform[i]);
        }
    }
}

public struct SpawnPosState
{
    public Transform SpawnPos;
    public SpawnState State;
    public SpawnPosState(Transform spawnPos)
    {
        SpawnPos = spawnPos;
        State = SpawnState.none;
    }
    public enum SpawnState
    {
        none = 0,
        used = 1
    }
}

