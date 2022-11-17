using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapInstance : MonoBehaviour
{
    System.Random _random = new System.Random();
    Transform[] _instansTransform = null;
    List<SpawnPosState> _insPos = new();
    MapElemantEntity _entity = null;
    HouseBase[] _houseBasesPrefab = null;

    public MapElemantEntity Entity => _entity;

    public void GenerateMap()
    {
        
        _houseBasesPrefab = Resources.LoadAll<HouseBase>("HousePrefab");
        int[] houseValues = new int[] { _entity.HouseValue, _entity.HouseValueOnSolt, _entity.HouseValueInBaby, _entity.HouseValueInArrow };
        int houseValueSum = houseValues.Sum();
        _instansTransform = GetComponentsInChildren<Transform>();

        for (int i = 0; i < houseValueSum; i++)
        {
            _insPos[i] = new SpawnPosState(_instansTransform[i]);
        }
    }

    void CreateHouse(HouseType type1,int targetHouseValue)
    {
        HouseBase housePrefab = _houseBasesPrefab[(int)type1];
        for(int i = 0; i < targetHouseValue; i++)
        {
            Instantiate(housePrefab);
        }
    }

    void CreateHouse(HouseType type1,HouseType type2, int targetHouseValue)
    {
        
    }

    void SetHouse(HouseBase house)
    {
        SpawnPosState[] unUsePosValue = _insPos.Where(x => x.State == SpawnPosState.SpawnState.none).ToArray();
        SpawnPosState targetPos = unUsePosValue[_random.Next(unUsePosValue.Length)];
        house.transform.position = targetPos.SpawnPos.position;
        targetPos.State = SpawnPosState.SpawnState.used;
    }
}

public class SpawnPosState
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

