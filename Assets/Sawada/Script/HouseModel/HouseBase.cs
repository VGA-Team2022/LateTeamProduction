using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HouseBase
{

    public virtual void Initialize<T>(T house) where T : HouseBehaviour 
    {
        house.ReturnPillows.ToList().ForEach(x => x.GetUpTimeAndTimeInPlayerStats(house.GetUpTime));
    }
    public virtual void PlayerEntryHouseMotion(PlayerController player) { }
    public virtual void PlayerInHouseMotion(PlayerController player) { }
    public virtual void PlayerExitHouseMotion(PlayerController player) { }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="allPillowValue"></param>
    /// <returns></returns>
    public int SetPillow(Returnpillow[] returnPillows, int allPillowValue)
    {
        int pillowValue
            = allPillowValue >= returnPillows.Length 
            ? pillowValue = UnityEngine.Random.Range(1, 4) : pillowValue = allPillowValue;
        for (int i = 0; i < pillowValue; i++)
        {
            returnPillows[i].gameObject.SetActive(true);
        }
        allPillowValue -= pillowValue;
        return allPillowValue;
    }
    /// <summary>
    ///　コンポーネント有効時に呼ぶ関数
    /// </summary>

}
public enum HouseType
{
    None = 0,
    Baby = 1,
    Solt = 2,
    DevilArrow = 3,
    DoubleType = 4
}

