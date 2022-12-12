using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UniRx;

public class HouseInBaby : HouseBase
{
    public override void Init<T>(T house)
    {
        base.Init(house);
        house.ReturnPillows.ToList().ForEach(x => x.GetUpTimeAndTimeInPlayerStats(_getUpTime));

    }
}
