using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    /// <summary>動く速度（子供）</summary>
    [SerializeField,Header("子供状態の動くスピード")]
    float _childMoveSpeed = 10f;
    /// <summary>動く速度（大人）</summary>
    [SerializeField, Header("大人状態の動くスピード")]
    float _adultMoveSpeed = 15f;
    /// <summary>時計</summary>
    float _timer = 0f;
    /// <summary>枕を返すまでにかかる時間</summary>//atai
    [SerializeField,Header("枕を返すまでにかかる時間")]
    float _timerlimit = 0.5f;
    Vector2 _moveVelocity;
    /// <summary>止まる直前の速度方向</summary>
    Vector2 _lastMoveVelocity;
    /// <summary>レベル</summary>
    int level = 1;
    public VariableJoystick _joyStick;
    Animator _anim;
    Rigidbody2D _rb;
    /// <summary>枕を返す標的のscriptを保持する</summary>
    Returnpillow _pillowEnemy;
    /// <summary>枕を返す標的のgameobjectを保持する</summary>
    GameObject pillowEnemy;
    UIManager _ui;
    /// <summary>枕返し圏内</summary>
    bool _pillow = false;
    /// <summary>大人か子供か</summary>
    [SerializeField,Header("プレイヤーが大人か子供か")]
    bool _adultState = false;

    /// <summary>枕返し圏内</summary>
    public bool Pillow { get => _pillow; set => _pillow = value; }
    public int Level { get => level; set => level = value; }
    public float Timerlimit { get => _timerlimit; set => _timerlimit = value; }
    public Returnpillow PillowEnemy { get => _pillowEnemy; set => _pillowEnemy = value; }
    public GameObject PillowEnemyObject { get => pillowEnemy; set => pillowEnemy = value; }
    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _ui = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //float h = Input.GetAxisRaw("Horizontal");
        //float v = Input.GetAxisRaw("Vertical");
        float h = _joyStick.Horizontal;
        float v = _joyStick.Vertical;

        if (!_adultState)
        {
            ChildMode(h, v);
        }
        else
        {
            AdultMode(h, v);
        }

        _rb.velocity = _moveVelocity;
        _lastMoveVelocity = _moveVelocity;

        if (Input.GetButton("Jump"))//右長押し
        {
            if (_pillowEnemy)//枕返し圏内にいたら
            {
                _timer += Time.deltaTime;
                _ui.ChargeSlider(_timer);
            }
        }
        else if(Input.GetButtonUp("Jump"))
        {
            _timer = 0;
            _ui.ChargeSlider(_timer);
        }
    }

    void LateUpdate()
    {
        if (_anim)
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
        //if (collision.gameObject.CompareTag("ReturnPillow"))
        //{
        //    pillowEnemy = collision.GetComponent<Returnpillow>();
        //}

        if(collision.gameObject.TryGetComponent<Returnpillow>(out var returnpillow))
        {
            pillowEnemy = collision.gameObject;
            _pillowEnemy = returnpillow;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _pillowEnemy = null;
        _timer = 0;
        _ui.ChargeSlider(_timer);
    }

    private void ChildMode(float h, float v)
    {
        _moveVelocity = new Vector2(h, v).normalized * _childMoveSpeed;
    }

    private void AdultMode(float h, float v)
    {
        _moveVelocity = new Vector2(h, v).normalized * _adultMoveSpeed;
    }
}
