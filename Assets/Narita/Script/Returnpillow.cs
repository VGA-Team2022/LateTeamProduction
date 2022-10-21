using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Returnpillow: MonoBehaviour
{
    //[SerializeField]
    //Slider _slider = null;
    /// <summary>枕返しを行ったかどうか</summary>
    bool _returnPillow;
    /// <summary>起きる時間（基準）</summary>
    float _getupTime;
    /// <summary>playerが大人か子供かで変化する</summary>
    float _timeInPlayerStats;
    /// <summary>赤ん坊がいた場合使用する</summary>
    float _timeInBaby;
    /// <summary>時計</summary>
    float _timer;
    Animator _anim = null;
    Image _image = null;
    public bool ReturnPillow { get => _returnPillow; set => _returnPillow = value; }
    // Start is called before the first frame update
    void Start()
    {
        _returnPillow = false;
        _anim = GetComponent<Animator>();
        _image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_returnPillow)
        {
            //↓当たり判定は後で変更予定
            GetComponent<BoxCollider2D>().enabled = false;
            //↓変える色は後で変更予定
            _image.color = Color.black;
        }
    }
    private void LateUpdate()
    {
        _anim.SetBool("boolの名前", _returnPillow);
    }

    private void OnTriggerStay2D(Collider2D collision)//プレイヤーが当たり判定の中にとどまったら
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().Pillow = true;
            _timer += Time.deltaTime;
            //数値の幅は後で変更予定
            _getupTime = Random.Range(2, 5);
            if (_getupTime <= _timer && _returnPillow)//制限時間を超えた　＋　枕を返されていなかったら
            {
                
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _timer -= _timer;
    }
}
