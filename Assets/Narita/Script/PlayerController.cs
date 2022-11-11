using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    /// <summary>動く速度（子供）</summary>
    [SerializeField, Header("子供状態の動くスピード")]
    float _childMoveSpeed = 10f;
    /// <summary>動く速度（大人）</summary>
    [SerializeField, Header("大人状態の動くスピード")]
    float _adultMoveSpeed = 15f;
    /// <summary>時計</summary>
    float _timer = 0f;
    /// <summary>枕を返すまでにかかる時間</summary>//atai
    [SerializeField, Header("枕を返すまでにかかる時間")]
    float _timerlimit = 0.5f;
    [SerializeField, Header("誤差の許容範囲")]
    float toleranceDis = 0.2f;
    Vector2 _moveVelocity;
    /// <summary>止まる直前の速度方向</summary>
    Vector2 _lastMoveVelocity;
    /// <summary>レベル</summary>
    int level = 1;
    public VariableJoystick _joyStick;
    Animator _anim;
    Rigidbody2D _rb;
    /// <summary>枕を返す標的のscriptを保持する</summary>
    Returnpillow _pillowEnemy = null;
    /// <summary>枕を返す標的のgameobjectを保持する</summary>
    GameObject _pillowEnemyObject = null;
    /// <summary>敵の位置情報</summary>
    Vector2 _enemyPos = default;
    /// <summary>プレイヤーが枕を返せる位置</summary>
    Vector2 _returnPillowPos = default;
    /// <summary>敵からどれだけ離れた距離から枕を返すかの間隔</summary>
    float _returnPillowDisToEnemy = 1f;
    /// <summary>プレイヤーと_returnPillowPosの距離</summary>
    float _returnPillowDisToPlayer;
    UIManager _ui;
    /// <summary>枕返し圏内</summary>
    bool _pillow = false;
    /// <summary>大人か子供か</summary>
    [SerializeField, Header("プレイヤーが大人か子供か")]
    bool _adultState = false;
    /// <summary>枕返し圏内</summary>
    public bool Pillow { get => _pillow; set => _pillow = value; }
    public int Level { get => level; set => level = value; }
    public float Timerlimit { get => _timerlimit; set => _timerlimit = value; }
    public Returnpillow PillowEnemy { get => _pillowEnemy; set => _pillowEnemy = value; }
    public GameObject PillowEnemyObject { get => _pillowEnemyObject; set => _pillowEnemyObject = value; }
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
                TranslatePlayerPos();

                _timer += Time.deltaTime;
                _ui.ChargeSlider(_timer);
            }
        }
        if (Input.GetButtonDown("Jump"))//自動で動くために距離計算を行う
        {
            PlayerAndEnemyDis();
        }
        //else if (Input.GetButtonUp("Jump"))
        //{
        //    _timer = 0;
        //    _ui.ChargeSlider(_timer);
        //}
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
        if (collision.gameObject.CompareTag("ReturnPillow"))
        {
            _pillowEnemyObject = collision.gameObject;
            _pillowEnemy = _pillowEnemyObject.GetComponent<Returnpillow>();
            _enemyPos = _pillowEnemyObject.transform.position;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        InformationReset();
    }

    private void ChildMode(float h, float v)
    {
        _moveVelocity = new Vector2(h, v).normalized * _childMoveSpeed;
    }

    private void AdultMode(float h, float v)
    {
        _moveVelocity = new Vector2(h, v).normalized * _adultMoveSpeed;
    }

    public void InformationReset()
    {
        _pillowEnemyObject = null;
        _pillowEnemy = null;
        _enemyPos = default;
        _timer = 0;
        _ui.ChargeSlider(_timer);
    }

    private void PlayerAndEnemyDis()//距離計算
    {
        if (Vector2.Distance(transform.position, new Vector2(_enemyPos.x - _returnPillowDisToEnemy, _enemyPos.y))
        >= Vector2.Distance(transform.position, new Vector2(_enemyPos.x + _returnPillowDisToEnemy, _enemyPos.y)))
        {
            _returnPillowPos = new Vector2(_enemyPos.x + _returnPillowDisToEnemy, _enemyPos.y);
        }
        else
        {
            _returnPillowPos = new Vector2(_enemyPos.x - _returnPillowDisToEnemy, _enemyPos.y);
        }
    }
    private void TranslatePlayerPos()
    {
        if (!_pillowEnemyObject)//Enemy情報を持っていたら
        {
            _returnPillowDisToPlayer = Vector2.Distance(transform.position, _returnPillowPos);
            if (_returnPillowDisToPlayer > toleranceDis)//誤差範囲
            {
                if (Mathf.Abs(transform.position.x - _returnPillowPos.x) > toleranceDis)
                {
                    if (transform.position.x > _returnPillowPos.x)
                    {
                        transform.Translate(Vector2.left * Time.deltaTime * _childMoveSpeed);
                    }
                    else if (transform.position.x < _returnPillowPos.x)
                    {
                        transform.Translate(Vector2.right * Time.deltaTime * _childMoveSpeed);
                    }
                }
                else
                {
                    if (transform.position.y > _returnPillowPos.y)
                    {
                        transform.Translate(Vector2.down * Time.deltaTime * _childMoveSpeed);
                    }
                    else if (transform.position.y < _returnPillowPos.y)
                    {
                        transform.Translate(Vector2.up * Time.deltaTime * _childMoveSpeed);
                    }
                }
            }
        }
    }
}
