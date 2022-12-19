using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HouseBehaviour : MonoBehaviour,IHousePool
{
    [Tooltip("プレイヤーを格納する変数")]
    protected PlayerController _playerController;
    [Tooltip("家のデータ1")]
    protected HouseBase _data1 = null;
    [Tooltip("掛け軸")]
    protected HangingScroll _hangingScroll = null;
    [Tooltip("家の屋内全てのRenderer")]
    protected TilemapRenderer[] _renderersInHouse = null;
    [Tooltip("家の屋内全てのcollider")]
    protected Collider2D[] _collidersInHouse = null;
    [Tooltip("家の中にいる枕の数")]
    protected Returnpillow[] _returnPillows = null;
    
    public Collider2D[] ColidersInHouse => _collidersInHouse;
    public Returnpillow[] ReturnPillows => _returnPillows;

    private void Start()
    {
        CreateHouseObject(new HouseBase());
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

    public void Activate()
    {
        Array.ForEach(_collidersInHouse, x => x.enabled = true);
        Array.ForEach(_renderersInHouse, x => x.enabled = true);
        Array.ForEach(_returnPillows, x => x.enabled = true);
    }
    /// <summary>
    /// コンポーネント非有効時に呼ぶ関数
    /// </summary>
    public void Desactivate()
    {
        Array.ForEach(_collidersInHouse, x => x.enabled = false);
        Array.ForEach(_renderersInHouse, x => x.enabled = false);
        Array.ForEach(_returnPillows, x => x.enabled = false);
    }

    public virtual void CreateHouseObject(HouseBase house1)
    {
        _renderersInHouse = GetComponentsInChildren<TilemapRenderer>();
        _collidersInHouse = GetComponentsInChildren<Collider2D>();
        _returnPillows = GetComponentsInChildren<Returnpillow>();
        _hangingScroll = GetComponentInChildren<HangingScroll>();
        _hangingScroll.Init();
        _data1 = house1;
        _data1.Init(this);
    }
}
