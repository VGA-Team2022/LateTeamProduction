using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    /// <summary>動く速度</summary>
    float _moveSpeed = 10f;
    /// <summary>時計</summary>
    float _timer = 0f;
    /// <summary>枕を返すまでにかかる時間</summary>//atai
    float _timerlimit = 0.5f;
    Vector2 _moveVelocity;
    /// <summary>止まる直前の速度方向</summary>
    Vector2 _lastMoveVelocity;
    /// <summary>レベル</summary>
    int _level = 1;
    public VariableJoystick _joyStick;
    Animator _anim;
    Rigidbody2D _rb;
    /// <summary>枕を返す標的を保持する</summary>
    Returnpillow _pillowEnemy;

    /// <summary>枕返し圏内</summary>
    bool _pillow = false;
    /// <summary>大人か子供か</summary>
    bool _adultState = false;

    /// <summary>枕返し圏内</summary>
    public bool Pillow { get => _pillow; set => _pillow = value; }

    public int Level { get => _level; set => _level = value; }

    public float Timer { get => _timer;}
    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //float h = Input.GetAxisRaw("Horizontal");
        //float v = Input.GetAxisRaw("Vertical");
        float h = _joyStick.Horizontal;
        float v = _joyStick.Vertical;

        _moveVelocity = new Vector2(h, v).normalized * _moveSpeed;
        _rb.velocity = _moveVelocity;

        _lastMoveVelocity = _moveVelocity;

        if(Input.GetButton("Fire1"))//右長押し
        {
            if(_pillowEnemy)//枕返し圏内にいたら
            {
                _timer += Time.deltaTime;
            }
        }
    }

    void LateUpdate()
    {
        if(_anim)
        {
            {//動いている間
                _anim.SetFloat("floatの名前", _moveVelocity.x);
                _anim.SetFloat("floatの名前", _moveVelocity.y);
                //上の場合、＋Y
                //下の場合、ーY
                //右上、右下、右の場合、＋X
                //左上、左下、左の場合、ーX
            }
            {//止まっている間
                _anim.SetFloat("", _lastMoveVelocity.x);
                _anim.SetFloat("", _lastMoveVelocity.y);
                //上の条件に加えて、プレイヤーが動いていないこと。
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)//寝ている敵の情報を取る
    {
        if(collision.gameObject.CompareTag("Enemy名"))
        {
            _pillowEnemy = collision.GetComponent<Returnpillow>();

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _pillowEnemy = null;
    }
    //void ReturnPillowCount()
    //{
    //    timer += Time.deltaTime;
    //    if(timerlimit <= timer)
    //    {
           
    //    }
    //}
}
