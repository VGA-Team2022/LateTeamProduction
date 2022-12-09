using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HouseInBaby : HouseBase
{
    public override void Init()
    {
        base.Init();
        //Ž©g‚Ì‰Æ‚Ì’†‚¢‚é‚É–‚Ì‹N‚«‚éŽžŠÔ‚ÌÝ’è
        _returnPillows.ToList().ForEach(x => x.GetUpTime(_getUpTime));
    }
}
