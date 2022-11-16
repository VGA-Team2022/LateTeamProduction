using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class HouseOnSolt : HouseBase
{
    [Tooltip("ドアのオブジェクトのの配列")]
    GameObject[] _doorObjects = null;
    Renderer[] _doorRenderers = null;
    Collider2D[] _doorColliers = null;

    public override void Init()
    {
        base.Init();
        //取得
        _doorObjects = GetComponentsInChildren<GameObject>().Where(x => x.tag == "Door").ToArray();
        for(int i = 0; i < _doorObjects.Length; i++)
        {
            _doorRenderers[i] = _doorObjects[i].GetComponent<Renderer>();
            _doorColliers[i] = _doorColliers[i].GetComponent<Collider2D>();
        }
    }

    public override void HouseEntryMotion(PlayerController player)
    {
        base.HouseEntryMotion(player);
        //プレイヤーの状況に応じてドアを開ける
        //ToDo:プレイヤーの大人判定をプロパティで公開してもらう。
        //foreach (Collider2D col in _doorColliers)
        //{
        //    col.enabled = player.AdaltState;
        //}
        //foreach (Renderer ren in _doorRenderers)
        //{
        //    ren.enabled = player.AdaltState;
        //}
    }
}
