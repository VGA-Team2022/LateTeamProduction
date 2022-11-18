using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Returnpillow : MonoBehaviour
{
    /// <summary>枕返しを行ったかどうか</summary>
    [SerializeField, Header("枕を返されたかどうか")]
    bool _returnPillow;
    //Image image = null;
    SpriteRenderer image = null;
    /// <summary>起きる時間</summary>
    [SerializeField, Header("起きる時間（基準）")]
    float _getupTime = 0f;
    /// <summary>playerが大人か子供かで変化する</summary>
    [SerializeField, Header("プレイヤーの状態で変化する時間")]
    float _timeInPlayerStats = 0f;
    /// <summary>赤ん坊がいた場合使用する</summary>
    [SerializeField, Header("部屋の中に赤ん坊がいた場合変化する時間")]
    float _timeInBaby = 0f;
    Collider2D[] collider = null;
    /// <summary>時計</summary>
    float _timer;
    Animator _anim = null;
    public bool ReturnPillow { get => _returnPillow; set => _returnPillow = value; }
    // Start is called before the first frame update
    void Start()
    {

        _returnPillow = false;
        _anim = GetComponent<Animator>();
        //image = GetComponent<Image>();
        image = GetComponent<SpriteRenderer>();
        collider = GetComponents<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_returnPillow)
        {
            //↓当たり判定は後で変更予定
            foreach (var col in collider)
            {
                col.enabled = false;
            }
            //↓変える色は後で変更予定
            if (!image)
            {
                Debug.LogError("imageがありません");
            }
            else
            {
                image.color = Color.black;
            }
        }
    }
    private void LateUpdate()
    {
        if (_anim)
            _anim.SetBool("boolの名前", _returnPillow);
    }

    private void OnTriggerStay2D(Collider2D collision)//プレイヤーが当たり判定の中にとどまったら
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().Pillow = true;
            _timer += Time.deltaTime;
            //数値の幅は後で変更予定

            if (_getupTime <= _timer && _returnPillow)//制限時間を超えた　＋　枕を返されていなかったら
            {

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _timer -= _timer;
    }
    /// <summary>起きる時間を決める関数</summary>
    /// <param name="time"></param>
    public void GetUpTime(float time)
    {
        _getupTime = time;
    }
}