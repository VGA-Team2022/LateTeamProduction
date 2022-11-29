using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HouseInBaby : HouseBase
{
    [SerializeField,Tooltip("–‚Ì‹N‚«‚éŽžŠÔ")] 
    float _getUpTime = 0f;


    public override void Init()
    {
        base.Init();
        _type = HouseType.Baby;
        //Ž©g‚Ì‰Æ‚Ì’†‚¢‚é‚É–‚Ì‹N‚«‚éŽžŠÔ‚ÌÝ’è
        _returnPillows.ToList().ForEach(x => x.GetUpTime(_getUpTime));
    }
}
