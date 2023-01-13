using System;
using UnityEngine;

public class HouseBehaviour : MonoBehaviour, IHousePool
{
    [SerializeField, Header("起きる時間")]
    protected float _getUpTime = 10f;
    [SerializeField, Header("掛け軸")]
    protected HangingScroll _hangingScroll = null;
    [SerializeField, Header("家の屋内全てのRenderer")]
    protected Renderer[] _renderersInHouse = null;
    [SerializeField, Header("家の屋内全てのcollider")]
    protected Collider2D[] _collidersInHouse = null;
    [SerializeField, Header("家の中にいる枕")]
    protected Returnpillow[] _returnPillows = null;
    [SerializeField, Header("家の属性ごとに配置するオブジェクト")]
    GameObject[] _objectsOfHouse;

    [Tooltip("家のデータ1")]
    protected HouseBase _data1 = null;
    bool _isMapInstance = false;


    public float GetUpTime => _getUpTime;
    public Collider2D[] ColidersInHouse => _collidersInHouse;
    public Returnpillow[] ReturnPillows => _returnPillows;
    public GameObject ObjectsOfHouse => _objectsOfHouse[(int)_data1.Type];


    void Start()
    {
        if (!_isMapInstance) CreateHouseObject(new HouseBase());
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player))
        {
            _data1.PlayerEntryHouseMotion(player);
            _hangingScroll.RendererChange(player);
        }
    }
    public virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player))
        {
            _data1.PlayerInHouseMotion(player);
        }
    }
    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player))
        {
            _data1.PlayerExitHouseMotion(player);
        }
    }

    /// <summary>
    /// コンポーネント有効時に呼ぶ関数
    /// </summary>
    public void Activate()
    {
        Array.ForEach(_collidersInHouse, x => x.enabled = true);
        Array.ForEach(_renderersInHouse, x => x.enabled = true);
        Array.ForEach(_returnPillows, x => x.gameObject.SetActive(true));
        if (this.gameObject.transform.rotation.z < 1f)
        {
            Array.ForEach(_returnPillows, x => x.gameObject.transform.Rotate(Vector3.zero, Space.World));
            _hangingScroll.transform.Rotate(Vector3.zero, Space.World);
        }
        else if (gameObject.transform.rotation.z >= 1f)
        {
            Array.ForEach(_returnPillows, x => x.gameObject.transform.Rotate(new Vector3(0f, 0f, 180f), Space.World));
            _hangingScroll.transform.Rotate(new Vector3(0f, 0f, 180f), Space.World);
        }
    }
    /// <summary>
    /// コンポーネント非有効時に呼ぶ関数
    /// </summary>
    public void Desactivate()
    {
        Array.ForEach(_collidersInHouse, x => x.enabled = false);
        Array.ForEach(_renderersInHouse, x => x.enabled = false);
        Array.ForEach(_returnPillows, x => x.gameObject.SetActive(false));
    }
    /// <summary>
    /// コンポーネント生成時に呼ぶ関数
    /// </summary>
    /// <param name="house1"></param>
    /// <returns>残りの枕の数</returns>
    public virtual int CreateHouseObject(MapInstance mapInstance, HouseBase house1)
    {
        _data1 = house1;
        _data1.Initialize(this);
        _isMapInstance = true;
        int remainPillows = _data1.SetPillow(_returnPillows, mapInstance.AllPillowValue);
        Array.ForEach(_returnPillows, x => x.gameObject.SetActive(false));
        _hangingScroll.Initialize();
        Desactivate();
        switch(_data1.Type)
        {
            case HouseType.None:
                break;
            default:
                _objectsOfHouse[(int)_data1.Type].SetActive(true);
                break;
        }

        return remainPillows;
    }

    public void CreateHouseObject(HouseBase house1)
    {
        _data1 = house1;
        _data1.Initialize(this);
        _hangingScroll.Initialize();
    }
}
