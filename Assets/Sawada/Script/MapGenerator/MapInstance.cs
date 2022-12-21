using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using IsGame;

//マップの自動生成クラス
public class MapInstance : MonoBehaviour
{
    [SerializeField, Tooltip("CSVファイルのパス")]
    string _filePath = null;
    [SerializeField, Tooltip("現在のレベル")]
    int _currentMapLevel = 0;

    [Tooltip("Transformをランダムで指定する為の変数")]
    System.Random _random = new System.Random();
    [Tooltip("マップデータ")]
    MapData _mapData = null;
    [Tooltip("生成するTransform")]
    SpawnPosState[] _insPos = null;
    [Tooltip("家のプレハブ")]
    HouseBehaviour[] _houseBases = null;
    [Tooltip("家のデータの配列")]
    HouseBase[] _houseDatas = null;


    void Start()
    {
        _mapData = new MapData(_filePath);　//CSVデータの読み込み
        _houseBases = Resources.LoadAll<HouseBehaviour>("HousePrefab").ToArray();　//家のプレハブデータの取得
        _houseDatas = new HouseBase[]　//家の挙動を決めるクラスの配列を生成
        {
            new HouseBase(),
            new HouseInBaby(),
            new HouseOnSolt(),
            new HouseOnDevilArrow()
        };
        _insPos = GetComponentsInChildren<Transform>()　　//生成地点の取得
            .Where(x => x.tag == "SpawnPos")
            .Select(x => new SpawnPosState(x))
            .ToArray();
        SetStage();
    }

    /// <summary>
    /// ステージの自動生成する関数
    /// </summary>
    public void SetStage()
    {
        //用意する家の数を配列で一時保存
        int[] houseTypeValue = new int[5] { _mapData.data[_currentMapLevel][(int)HouseType.None + 2]
                                    , _mapData.data[_currentMapLevel][(int)HouseType.Baby + 2]
                                    , _mapData.data[_currentMapLevel][(int)HouseType.Solt + 2]
                                    , _mapData.data[_currentMapLevel][(int)HouseType.DevilArrow + 2]
                                    , _mapData.data[_currentMapLevel][(int)HouseType.DoubleType + 2]};

        //ゲームに存在する家の種類の数だけ回し、それぞれ指定された数だけ生成する
        for (int houseTypes = 0; houseTypes < houseTypeValue.Length; houseTypes++)
        {
            HouseBehaviour[] houses = null;
            if ((houseTypeValue[houseTypes]) <= 0) continue;　//生成する数が０だった場合continueで飛ばす
            switch ((HouseType)houseTypes)
            {
                case HouseType.DoubleType:
                    houses = CreateHouse(HouseType.Solt, HouseType.DevilArrow, houseTypeValue[houseTypes]);　//二つ以上の要素がある家を生成
                    break;
                default:
                    houses = CreateHouse((HouseType)houseTypes, houseTypeValue[houseTypes]);　//一つ以上の要素がある家を生成
                    break;
            }
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
        for (int i = 0; i < targetHouseValue; i++)
        {
            houses[i] = Instantiate(_houseBases[0]);
            houses[i].CreateHouseObject(_houseDatas[(int)type1]);
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
    DoubleHouseBehaviour[] CreateHouse(HouseType type1, HouseType type2, int targetHouseValue)
    {
        DoubleHouseBehaviour[] houses = new DoubleHouseBehaviour[targetHouseValue];
        for (int i = 0; i < targetHouseValue; i++)
        {
            houses[i] = Instantiate((DoubleHouseBehaviour)_houseBases[1]);
            houses[i].CreateHouseObject(_houseDatas[(int)type1], _houseDatas[(int)type2]);
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
            SpawnPosState[] unUsePosValue = _insPos.Where(x => x.State == SpawnPosState.SpawnState.none).ToArray();　//使われていない座標を配列で取得
            SpawnPosState targetPos = unUsePosValue[_random.Next(unUsePosValue.Length)];　//配列の中からランダムに要素を取得
            houses[i].transform.position = targetPos.spawnPos.position;
            houses[i].transform.rotation = targetPos.spawnPos.rotation;
            targetPos.State = SpawnPosState.SpawnState.used; //使われた要素の状態を更新する
        }
    }
}

//家をセットする座標とその状態を保持するクラス
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

