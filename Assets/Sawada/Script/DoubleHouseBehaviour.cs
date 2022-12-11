using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleHouseBehaviour : HouseBehaviour
{
    [Tooltip("â∆ÇÃÉfÅ[É^2")]
    HouseBase _data2 = null;

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player))
        {
            _data2.PlayerEntryHouseMotion(player);
        }
    }
    public override void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player))
        {
            _data2.PlayerInHouseMotion(player);
        }
    }
    public override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player))
        {
            _data2.PlayerExitHouseMotion(player);
        }
    }

    public  void CreateHouseObject(HouseBase house1, HouseBase house2,GameManager gameManager)
    {
        house1.Init(this);
        house2.Init(this);
        _data1 = house1;
        _data2 = house2;
    }
}
