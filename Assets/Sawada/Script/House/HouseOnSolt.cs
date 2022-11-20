using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class HouseOnSolt : HouseBase
{
    //[Tooltip("")]

    [Tooltip("ドアのオブジェクトRendererの配列")]
    Renderer[] _doorRenderers = null;
    [Tooltip("ドアのオブジェクトのColliderの配列")]
    Collider2D[] _doorColliers = null;

    public override void Init()
    {
        base.Init();
        _type = HouseType.Solt;
        //取得(ドアのオブジェクトには”Door”というタグを付けてください)
        _doorColliers = gameObject.GetComponentsInChildren<Collider2D>().Where(x => x.tag == "Door").ToArray();
        _doorRenderers = new Renderer[_doorColliers.Length];
        for (int i = 0; i < _doorColliers.Length; i++)
        {
            _doorRenderers[i] = _doorColliers[i].GetComponent<Renderer>();
        }
    }

    public override void PlayerEntryHouseMotion(PlayerController player)
    {
        base.PlayerEntryHouseMotion(player);
        //プレイヤーの状況に応じてドアを開ける
        foreach (Collider2D col in _doorColliers)
        {
            col.enabled = player.AdultState;
        }
        foreach (Renderer ren in _doorRenderers)
        {
            ren.enabled = player.AdultState;
        }
    }

    public override void PlayerInHouseMotion(PlayerController player)
    {
        base.PlayerInHouseMotion(player);
        //if(!player.AdultState)
        //{
        //    ここにゲームオーバーの処理を書く
        //}
    }
}
