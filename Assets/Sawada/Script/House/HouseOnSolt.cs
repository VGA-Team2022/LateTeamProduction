using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class HouseOnSolt : HouseBase
{
    [Tooltip("ドアのオブジェクトRendererの配列")]
    Renderer[] _doorRenderers = null;
    [Tooltip("ドアのオブジェクトのColliderの配列")]
    Collider2D[] _doorColliers = null;

    public override void Init()
    {
        base.Init();
        //取得
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
        //ToDo:プレイヤーの大人判定をプロパティで公開してもらう。
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
