using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapElemantEntity
{
    [Tooltip("通常の家の数")]
    public int _houseValue;
    [Tooltip("盛り塩がある家の数")]
    public int _houseValueOnSolt;
    [Tooltip("赤ちゃんがいる家の数")]
    public int _houseValueInBaby;
    [Tooltip("ギミック「板」の数")]
    public bool _existBoard;
}

