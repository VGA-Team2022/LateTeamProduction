using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapInstance : MonoBehaviour
{
    [SerializeField, Tooltip("現在のレベル")]
    string _filePath = null;
    [SerializeField, Tooltip("現在のレベル")]
    int _currentMapLevel = 0;

    [Tooltip("Transformをランダムで指定する為の変数")]
    System.Random _random = new System.Random();
    [Tooltip("マップデータ")]
    MapData _mapData = null;
    [Tooltip("GameManagerを格納する変数")]
    GameManager _gameManager = null;
    [Tooltip("生成するTransform")]
    SpawnPosState[] _insPos = null;
    [Tooltip("家のプレハブ")]
    HouseBehaviour[] _houseBases = null;
    [Tooltip("家のデータの配列")]
    HouseBase[] _houseDatas = null;


    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _mapData = new MapData(_filePath);
        _houseBases = Resources.LoadAll<HouseBehaviour>("HousePrefab").OrderBy(x => x.Type).ToArray();
    }

    public void SetStage()
    {
        int[] houseTypeValue = null;
        //用意する家の総数をカウント
        houseTypeValue = new int[5] { _mapData.data[_currentMapLevel][(int)HouseType.None]
                                    , _mapData.data[_currentMapLevel][(int)HouseType.Baby]
                                    , _mapData.data[_currentMapLevel][(int)HouseType.Solt]
                                    , _mapData.data[_currentMapLevel][(int)HouseType.DevilArrow]
                                    , _mapData.data[_currentMapLevel][(int)HouseType.DoubleType]};
        int houseValueSum = houseTypeValue.Sum();

        _insPos = GetComponentsInChildren<Transform>().Where(x => x.tag == "SpawnPos").Select(x => new SpawnPosState(x)).ToArray();
        for (int houseTypes = 0; houseTypes < houseTypeValue.Length; houseTypes++)
        {
            if((houseTypeValue[houseTypes]) <= 0) return;
            HouseBehaviour[] houses = CreateHouse((HouseType)houseTypes, houseTypeValue[houseTypes]);
            SetHouse(houses);
        }
        _currentMapLevel++;
    }
    /// <summary>
    /// 属性が一つの家オブジェクトを生成するための関数
    /// </summary>
    /// <param name="type1">家の属性</param>
    /// <param name="targetHouseValue">用意する家の数</param>
    /// <returns></returns>
    HouseBehaviour[] CreateHouse(HouseType type1, int targetHouseValue)
    {
        HouseBehaviour[] houses = new HouseBehaviour[targetHouseValue];
        HouseBehaviour housePrefab = _houseBases[(int)type1];
        for (int i = 0; i < targetHouseValue; i++)
        {
            houses[i] = Instantiate(housePrefab);
            houses[i].CreateObject();
            houses[i].HouseData.SetValue(_gameManager);
        }
        return houses;
    }
    /// <summary>
    /// 属性が二つの家オブジェクトを生成するための関数
    /// </summary>
    /// <param name="type1">家の属性1</param>
    /// <param name="type2">家の属性2</param>
    /// <param name="targetHouseValue">用意する家の数</param>
    /// <returns></returns>
    HouseBehaviour[] CreateHouse(HouseType type1, HouseType type2, int targetHouseValue)
    {
        HouseBehaviour[] houses = new HouseBehaviour[targetHouseValue];
        HouseBehaviour housePrefab = (HouseBehaviour)_houseBases.Where(x => x.Type == type1 && x.Type == type2);
        if (housePrefab == null) return null;
        for (int i = 0; i < targetHouseValue; i++)
        {
            houses[i] = Instantiate(housePrefab);
        }
        return houses;
    }
    /// <summary>
    /// ゲーム上の座標からランダムに選択し家をセットする関数
    /// </summary>
    /// <param name="house">セットする家のプレハブ</param>
    void SetHouse(HouseBehaviour[] houses)
    {
        for (int i = 0; i < houses.Length; i++)
        {
            SpawnPosState[] unUsePosValue = _insPos.Where(x => x.State == SpawnPosState.SpawnState.none).ToArray();
            SpawnPosState targetPos = unUsePosValue[_random.Next(unUsePosValue.Length)];
            houses[i].transform.position = targetPos.spawnPos.position;
            houses[i].transform.rotation = targetPos.spawnPos.rotation;
            targetPos.State = SpawnPosState.SpawnState.used;
        }
    }
}

//家をセットする座標の状態
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

