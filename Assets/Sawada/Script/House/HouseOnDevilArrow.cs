using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseOnDevilArrow : HouseBase
{
    [SerializeField, Tooltip("プレイヤーが子供に戻る時間")]
    float _cancellationTime = 0f;
    [SerializeField, Tooltip("カウントダウン")]
    float _time = 0;

    public override void OnTriggerStay2D(Collider2D collision)
    {
        base.OnTriggerStay2D(collision);
        if (collision.TryGetComponent<PlayerController>(out PlayerController player))
        {
            PlayerInHouseMotion(player);
        }
    }

    public override void PlayerInHouseMotion(PlayerController player)
    {
        base.PlayerInHouseMotion(player);
        //カウントダウン終了時に子供にする
        if (IsCountUp() && player.AdultState)
        {
            player.ModeChange(false);
        }
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
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

    //カウントーリセット
    public void ResetTimer()
    {
        _time = 0;
    }
}
