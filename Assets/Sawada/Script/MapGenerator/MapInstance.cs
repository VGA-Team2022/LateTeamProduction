using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using IsGame;

//マップの自動生成クラス
public class MapInstance : MonoBehaviour
{
    [SerializeField, Header("CSVファイルのパス")]
    string _filePath = "MapElemant";
    [SerializeField, Header("現在のレベル")]
    int _currentMapLevel = 0;
    [SerializeField, Header("生成するTransform")]
    Transform[] _insTrans = null;

    [Tooltip("家の総数")]
    int[] _houseTypeValue = null;
    [Tooltip("枕の総数")]
    int _allPillowValue = 0;
    [Tooltip("Transformをランダムで指定する為の変数")]
    System.Random _random = new System.Random();
    [Tooltip("マップデータ")]
    MapData _mapData = null;

    SpawnPosState[] _insPos = null;
    [Tooltip("家のプレハブ")]
    HouseBehaviour[] _houseBases = null;
    [Tooltip("家のデータの配列")]
    HouseBase[] _houseDatas = new HouseBase[]　//家の挙動を決めるクラスの配列
                {
                    new HouseBase(),
                    new HouseInBaby(),
                    new HouseOnSolt(),
                    new HouseOnDevilArrow()
                };


    /// <summary>
    /// 家の総数
    /// </summary>
    public int AllHouseValue => _houseTypeValue.Sum();
    /// <summary>
    /// 枕の総数
    /// </summary>
    public int AllPillowValue => _allPillowValue;


    void Awake()
    {
        _mapData = new MapData(_filePath);　//CSVデータの読み込み
        _allPillowValue = _mapData.data[_currentMapLevel][0]; //枕の総数を初期化
        GameManager.Instance.SleepingEnemy = _allPillowValue; //ゲームマネージャーに枕の敵の総数を代入
        _houseBases = Resources.LoadAll<HouseBehaviour>("HousePrefab").ToArray();　//家のプレハブデータの取得
        _insPos = _insTrans.Select(x => new SpawnPosState(x)).ToArray(); //家の出現地点を取得

        SetStage();
    }

    /// <summary>
    /// ステージを自動生成する関数
    /// </summary>
    public void SetStage()
    {
        //用意する家の数を配列で一時保存
        _houseTypeValue = new int[5] { _mapData.data[_currentMapLevel][(int)HouseType.None + 2]
                                     , _mapData.data[_currentMapLevel][(int)HouseType.Baby + 2]
                                     , _mapData.data[_currentMapLevel][(int)HouseType.Solt + 2]
                                     , _mapData.data[_currentMapLevel][(int)HouseType.DevilArrow + 2]
                                     , _mapData.data[_currentMapLevel][(int)HouseType.DoubleType + 2]};

        //ゲームに存在する家の種類の数だけ回し、それぞれ指定された数だけ生成する
        for (int houseTypes = 0; houseTypes < _houseTypeValue.Length; houseTypes++)
        {
            if ((_houseTypeValue[houseTypes]) <= 0) continue;//生成する数が０だった場合飛ばす

            HouseBehaviour[] houses = new HouseBehaviour[_houseTypeValue[houseTypes]];
            SpawnPosState[] unUsePosValue = _insPos.Where(x => x.State == SpawnPosState.SpawnState.none).ToArray();
            _houseTypeValue[houseTypes] = unUsePosValue.Length < _houseTypeValue[houseTypes]
                                        ? unUsePosValue.Length : _houseTypeValue[houseTypes];
            switch ((HouseType)houseTypes)
            {
                case HouseType.DoubleType:
                    houses = CreateHouse(HouseType.Solt, HouseType.DevilArrow, _houseTypeValue[houseTypes]); //二つ以上の要素がある家を生成
                    break;
                default:
                    houses = CreateHouse((HouseType)houseTypes, _houseTypeValue[houseTypes]); //一つ以上の要素がある家を生成
                    break;
            }
            int remainPos = SetHouse(houses);
            if (remainPos <= 0) break;
        }
        _currentMapLevel++;
    }
    /// <summary>
    /// 属性が一つの家オブジェクトを生成するための関数
    /// </summary>
    /// <param name="type1">家の属性</param>
    /// <param name="targetHouseValue">用意する家の数</param>
    /// <returns>設置した要素1の家の配列</returns>
    HouseBehaviour[] CreateHouse(HouseType type1, int targetHouseValue)
    {
        HouseBehaviour[] houses = new HouseBehaviour[targetHouseValue];
        for (int i = 0; i < targetHouseValue; i++)
        {
            houses[i] = Instantiate(_houseBases[0]);
            _allPillowValue = houses[i].CreateHouseObject(this, _houseDatas[(int)type1]);
        }
        return houses;
    }
    /// <summary>
    /// 属性が二つの家オブジェクトを生成するための関数
    /// </summary>
    /// <param name="type1">家の属性1</param>
    /// <param name="type2">家の属性2</param>
    /// <param name="targetHouseValue">用意する家の数</param>
    /// <returns>設置した要素2の家の配列</returns>
    DoubleHouseBehaviour[] CreateHouse(HouseType type1, HouseType type2, int targetHouseValue)
    {
        DoubleHouseBehaviour[] houses = new DoubleHouseBehaviour[targetHouseValue];
        for (int i = 0; i < targetHouseValue; i++)
        {
            houses[i] = Instantiate((DoubleHouseBehaviour)_houseBases[1]);
            houses[i].CreateHouseObject(this, _houseDatas[(int)type1], _houseDatas[(int)type2]);
        }
        return houses;
    }
    /// <summary>
    /// ゲーム上の座標からランダムに選択し家をセットする関数
    /// </summary>
    /// <param name="house">セットする家のプレハブ</param>
    int SetHouse(HouseBehaviour[] houses)
    {
        SpawnPosState[] unUsePosValue = new SpawnPosState[0];
        for (int i = 0; i < houses.Length; i++)
        {
            unUsePosValue = _insPos.Where(x => x.State == SpawnPosState.SpawnState.none).ToArray();　//使われていない座標を配列で取得
            if (unUsePosValue.Length > 0)
            {
                SpawnPosState targetPos = unUsePosValue[_random.Next(unUsePosValue.Length)]; //配列の中からランダムに要素を取得
                houses[i].transform.position = targetPos.spawnPos.position;
                houses[i].transform.rotation = targetPos.spawnPos.rotation;
                houses[i].Activate();
                targetPos.State = SpawnPosState.SpawnState.used; //使われた要素の状態を更新する

            }
        }
        return unUsePosValue.Length;
    }

    /// <summary>
    /// ゲーム終了時に値をリセットする関数(シーンを遷移する場合は呼ばなくていい)
    /// </summary>
    public void ResetValue()
    {
        _houseTypeValue = null;
        Array.ForEach(_insPos, x => x.State = SpawnPosState.SpawnState.none);
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

