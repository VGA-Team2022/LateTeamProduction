using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapElemantEntity
{
    [Tooltip("ステージレベル")]
    public int StageLevel = 0;
    [Tooltip("敵「寝てる敵」の総数")]
    public int SleeperValue = 0;
    [Tooltip("敵「仲居さん」の総数")]
    public int NakaiValue = 0;
    [Tooltip("合成している家を用意するか")]
    public bool IsSynthesisHouse;
    [Tooltip("通常の家の数")]
    public int HouseValue;
    [Tooltip("盛り塩がある家の数")]
    public int HouseValueOnSolt;
    [Tooltip("赤ちゃんがいる家の数")]
    public int HouseValueInBaby;
    [Tooltip("破魔矢がある家の数")]
    public int HouseValueInArrow;
}



