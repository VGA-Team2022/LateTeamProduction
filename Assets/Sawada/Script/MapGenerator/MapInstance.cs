using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapInstance : MonoBehaviour
{
    [Tooltip("Transformをランダムで指定する為の変数")]
    System.Random _random = new System.Random();
    [Tooltip("生成するTransform")]
    Transform[] _instansTransform = null;
    List<SpawnPosState> _insPos = new();
    [SerializeField,Tooltip("マップデータ")]
    MapData _entity = null;
    HouseBase[] _houseBases = null;

    public MapData Entity => _entity;

    void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        _houseBases = Resources.LoadAll<HouseBase>("HousePrefab");

        int mapLevel = 0;
        int[] houseValues = new int[4] { _entity.MapEntity[mapLevel].HouseValue
                                       , _entity.MapEntity[mapLevel].HouseValueOnSolt
                                       , _entity.MapEntity[mapLevel].HouseValueInBaby
                                       , _entity.MapEntity[mapLevel].HouseValueInArrow };
        int houseValueSum = houseValues.Sum();
        _instansTransform = GetComponentsInChildren<Transform>().Where(x => x.tag == "SpawnPos").ToArray();

        for (int i = 0; i < houseValueSum; i++)
        {
            _insPos[i] = new SpawnPosState(_instansTransform[i]);
        }
    }

    HouseBase[] CreateHouse(HouseType type1, int targetHouseValue)
    {
        HouseBase[] houses = new HouseBase[targetHouseValue];
        HouseBase housePrefab = _houseBases[(int)type1];
        houses.ToList().ForEach(x => x = Instantiate(housePrefab));
        //for(int i = 0; i < targetHouseValue; i++)
        //{
        //    var obj = Instantiate(housePrefab);
        //}
        return houses;
    }

    void CreateHouse(HouseType type1, HouseType type2, int targetHouseValue)
    {
        HouseBase[] houses = new HouseBase[targetHouseValue];
        HouseBase insHouse = (HouseBase)_houseBases.Where(x => x.Type == type1 && x.Type == type2);
        houses.ToList().ForEach(x => Instantiate(insHouse));
        //for(int i = 0; i < targetHouseValue; i++)
        //{
        //    var obj = Instantiate(housePrefab);
        //}
        //return houses;
    }

    /// <summary>
    /// ゲーム上の座標からランダムに選択し家をセットする関数
    /// </summary>
    /// <param name="house">セットする家のプレハブ</param>
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

