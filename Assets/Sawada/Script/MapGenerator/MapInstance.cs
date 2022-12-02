using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapInstance : MonoBehaviour
{
    //[SerializeField, Tooltip("マップデータ")]
    //MapData _entity = null;
    [SerializeField, Tooltip("現在のレベル")]
    int _currentMapLevel = 0;
    [SerializeField, Tooltip("生成するTransform")]
    GameObject _insPosParent = null;

    [Tooltip("Transformをランダムで指定する為の変数")]
    System.Random _random = new System.Random();
    [Tooltip("生成するTransform")]
    SpawnPosState[] _insPos = null;
    [Tooltip("家のプレハブ")]
    HouseBase[] _houseBases = null;


    void Start()
    {
        //if (_entity != null)
        {
            SetStage();

        }
    }

    public void SetStage()
    {
        //プレハブデータを取得
        _houseBases = Resources.LoadAll<HouseBase>("HousePrefab");

        //int[] houseTypeValue = new int[4] { _entity.MapTable[_currentMapLevel].houseValue
        //                               , _entity.MapTable[_currentMapLevel].houseValueOnSolt
        //                               , _entity.MapTable[_currentMapLevel].houseValueInBaby
        //                               , _entity.MapTable[_currentMapLevel].houseValueInArrow };
        //int houseValueSum = houseTypeValue.Sum();

        _insPos = _insPosParent.GetComponentsInChildren<Transform>().Where(x => x.tag == "SpawnPos").Select(x => new SpawnPosState(x)).ToArray();
        //for (int houseTypes = 0; houseTypes < houseTypeValue.Length; houseTypes++)
        //{
        //    HouseBase[] houses = CreateHouse((HouseType)houseTypes, houseTypeValue[houseTypes]);
        //    SetHouse(houses);
        //}
    }

    HouseBase[] CreateHouse(HouseType type1, int targetHouseValue)
    {
        HouseBase housePrefab = _houseBases[(int)type1];
        HouseBase[] houses = new HouseBase[targetHouseValue];
        for(int i = 0;i < targetHouseValue;i++)
        {
            houses[i] = Instantiate(housePrefab);
        }
        return houses;
    }

    HouseBase[] CreateHouse(HouseType type1, HouseType type2, int targetHouseValue)
    {
        HouseBase[] houses = new HouseBase[targetHouseValue];
        HouseBase insHouse = (HouseBase)_houseBases.Where(x => x.Type == type1 && x.Type == type2);
        houses.ToList().ForEach(x => Instantiate(insHouse));
        return houses;
    }

    /// <summary>
    /// ゲーム上の座標からランダムに選択し家をセットする関数
    /// </summary>
    /// <param name="house">セットする家のプレハブ</param>
    void SetHouse(HouseBase[] houses)
    {
        for(int i = 0;i < houses.Length;i++)
        {
            SpawnPosState[] unUsePosValue = _insPos.Where(x => x.State == SpawnPosState.SpawnState.none).ToArray();
            SpawnPosState targetPos = unUsePosValue[_random.Next(unUsePosValue.Length)];
            houses[i].transform.position = targetPos.spawnPos.position;
            houses[i].transform.rotation = targetPos.spawnPos.rotation;
            targetPos.State = SpawnPosState.SpawnState.used;
        }
    }
}

public class SpawnPosState
{
    public Transform spawnPos;
    public SpawnState State;
    public SpawnPosState(Transform position)
    {
        this.spawnPos = position;
        State = SpawnState.none;
    }
    public enum SpawnState
    {
        none = 0,
        used = 1
    }
}

