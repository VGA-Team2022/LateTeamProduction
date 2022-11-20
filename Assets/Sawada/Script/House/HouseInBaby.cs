using System.Linq;
using UnityEngine;

public class HouseInBaby : HouseBase
{
    [SerializeField,Tooltip("枕の起きる時間")] 
    float _getUpTime = 0f;


    public override void Init()
    {
        base.Init();
        _type = HouseType.Baby;
        //自身の家の中いるに枕の起きる時間の設定
        //_returnPillows.Select(x => x.GetUpTime(_getUpTime));
    }
}
