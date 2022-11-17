using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HouseInBaby : HouseBase
{
    [SerializeField,Tooltip("枕の起きる時間")] 
    float _getUpTime = 0f;


    public override void Init()
    {
        base.Init();
        //自身の家の中いるに枕の起きる時間の設定
        //ToDo:Player側で起きる時間を設定する関数を書いてPublicで公開してもらう
        //_returnPillows.Select(x => x.SetGetUpTime(_getUpTime));
    }
}
