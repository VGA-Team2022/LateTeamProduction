using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HouseOnDevilArrow : HouseBase
{
    [Tooltip("プレイヤーが子供に戻る時間")]
    float _cancellationTime = 5f;
    [Tooltip("カウントダウン")]
    float _time = 0;

    public override void PlayerInHouseMotion(PlayerController player)
    {
        base.PlayerInHouseMotion(player);
        _houseType = HouseType.DevilArrow;
        //カウントダウン終了時に子供にする
        if (IsCountUp() && player.AdultState)
        {
            player.ModeChange(false);
        }
    }
    public override void PlayerExitHouseMotion(PlayerController player)
    {
        base.PlayerExitHouseMotion(player);
        ResetTimer();
    }

    //カウントダウン
    public bool IsCountUp()
    {
        _time += Time.deltaTime;
        if (_time < _cancellationTime)
        {
            return false;
        }
        return true;
    }

    //カウントリセット
    public void ResetTimer()
    {
        _time = 0;
    }
}
