using System;
using UnityEngine;

public class HouseBehaviour : MonoBehaviour,IHousePool
{
    [SerializeField,Header("起きる時間")]
    protected float _getUpTime = 10f;
    [SerializeField, Header("掛け軸")]
    protected HangingScroll _hangingScroll = null;
    [SerializeField, Header("家の屋内全てのRenderer")]
    protected Renderer[] _renderersInHouse = null;
    [SerializeField, Header("家の屋内全てのcollider")]
    protected Collider2D[] _collidersInHouse = null;
    [SerializeField, Header("家の中にいる枕")]
    protected Returnpillow[] _returnPillows = null;

    [Tooltip("プレイヤーを格納する変数")]
    protected PlayerController _playerController;
    [Tooltip("家のデータ1")]
    protected HouseBase _data1 = null;


    public float GetUpTime => _getUpTime;
    public Collider2D[] ColidersInHouse => _collidersInHouse;
    public Returnpillow[] ReturnPillows => _returnPillows;

    private void Start()
    {
        //CreateHouseObject(new HouseBase());
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player))
        {
            _data1.PlayerEntryHouseMotion(player);
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
    public virtual int CreateHouseObject(MapInstance mapInstance,HouseBase house1)
    {
        _data1 = house1;
        _data1.Initialize(this);
        int remainPillows = _data1.SetPillow(_returnPillows,mapInstance.AllPillowValue);
        Array.ForEach(_returnPillows, x => x.gameObject.transform.rotation = Quaternion.identity);
        Array.ForEach(_returnPillows, x => x.gameObject.SetActive(false));
        _hangingScroll.Initialize();

        return remainPillows;
    }

    /// <summary>
    /// 枕を起動させる関数
    /// </summary>
    /// <returns>起動させた枕の数</returns>
    
}
