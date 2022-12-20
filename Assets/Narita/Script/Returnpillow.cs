using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Returnpillow : MonoBehaviour,IRevers
{
    [SerializeField, Header("枕を返す時のプレイヤーの位置"), Tooltip("枕を返す時のプレイヤーの位置情報")]
    Transform _returnPillouPosLeft;
    [SerializeField, Header("枕を返す時のプレイヤーの位置"), Tooltip("枕を返す時のプレイヤーの位置情報")]
    Transform _returnPillouPosRight;
    [SerializeField, Header("枕を返されたかどうか"), Tooltip("枕を返されたかどうか")]
    bool _returnPillow = false;
    [SerializeField, Header("起きる時間（基準）"), Tooltip("起きる時間（関数内で値を決める）")]
    float _getupTime = 5f;
    //
    [SerializeField, Header("プレイヤーの状態で変化する時間"), Tooltip("プレイヤーが大人の時値を変化させる")]
    float _timeInPlayerStats = 0f;
    /// <summary>赤ん坊がいた場合使用する</summary>
    [SerializeField, Header("部屋の中に赤ん坊がいた場合変化する時間")]
    float _timeInBaby = 0f;
    //,「//」で囲っている変数はこのscriptが持つべきなのかわからないが、話に上がっているのにどこにも存在しないため定義している。
    [Tooltip("当たり判定の配列")]
    Collider2D[] collider = null;
    [Tooltip("起きるまでの時間をカウントするタイマー")]
    float _getupCountTimer;
    Animator _anim = null;
    [SerializeField,Tooltip("playerを見つけたときに使用")]
    SoundManager _sound = null;
    [Tooltip("枕を返されたかどうか、外部参照用")]
    public bool ReturnPillow { get => _returnPillow; set => _returnPillow = value; }
    [Tooltip("枕を返す時のプレイヤーの位置情報、外部参照用")]
    public Transform ReturnPillouPosLeft { get => _returnPillouPosLeft;}
    public Transform ReturnPillouPosRight { get => _returnPillouPosRight; }
    // Start is called before the first frame update
    void Start()
    {
        _returnPillow = false;
        _anim = GetComponent<Animator>();
        collider = GetComponents<Collider2D>();
    }

    // Update is called once per frame

    private void OnTriggerStay2D(Collider2D collision)//プレイヤーが当たり判定の中にとどまったら
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player))
        {
            _getupCountTimer += Time.deltaTime;
            if (_getupTime <= _getupCountTimer && _returnPillow)//制限時間を超えた + 枕を返されていなかったら + プレイヤーがまだ範囲内にいたら
            {
                //見つかった時、ゲームオーバーの関数を書く
                _sound.Discoverd();
                player.PlayerFind();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _getupCountTimer = 0;
    }
    /// <summary>起きる時間を決める、プレイヤーの状態が変化したときに呼ぶ関数</summary>
    /// <param name="time"></param>
    public void GetUpTimeAndTimeInPlayerStats(float getuptime)
    {
        _getupTime = getuptime;
    }

    private void GetUp(PlayerController player)
    {
        player.PlayerFind();
    }

    public void ObjectRevers()
    {
        _returnPillow = true;
        if (_anim)
            _anim.SetBool("returnPillowPlay", _returnPillow);
        foreach (var col in collider)
        {
            col.enabled = false;
        }
    }
}