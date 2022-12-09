using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HouseInBaby : HouseBase
{
    public override void Init<T>(T house)
    {
        base.Init(house);
        house.ReturnPillows.ToList().ForEach(x => x.GetUpTime(_getUpTime));
    }
}
