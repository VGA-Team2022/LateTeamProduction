using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UniRx;

public class HouseInBaby : HouseBase
{
    float _newGetUp = 0.8f;
    public override void Initialize<T>(T house)
    {
        base.Initialize(house);
        _houseType = HouseType.Baby;
        house.ReturnPillows.ToList().ForEach(x => x.GetUpTimeAndTimeInPlayerStats(house.GetUpTime * _newGetUp));
    }
}
