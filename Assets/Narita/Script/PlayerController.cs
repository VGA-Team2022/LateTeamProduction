using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public VariableJoystick _joyStick;
    /// <summary>動く速度（子供）</summary>
    [SerializeField, Header("子供状態の動くスピード")]
    float _childMoveSpeed = 10f;
    /// <summary>動く速度（大人）</summary>
    [SerializeField, Header("大人状態の動くスピード")]
    float _adultMoveSpeed = 15f;
    /// <summary>枕を返すまでにかかる時間</summary>
    [SerializeField, Header("誤差の許容範囲")]
    float toleranceDis = 0.2f;
    /// <summary>時計</summary>
    float _timer = 0f;
    /// <summary>敵からどれだけ離れた距離から枕を返すかの間隔</summary>
    float _returnPillowDisToEnemy = 1f;
    /// <summary>プレイヤーと_returnPillowPosの距離</summary>
    float _returnPillowDisToPlayer;
    /// <summary>移動速度計算結果</summary>
    Vector2 _moveVelocity;
    /// <summary>止まる直前の速度方向</summary>
    Vector2 _lastMovejoyStick;
    /// <summary>敵の位置情報</summary>
    Vector2 _enemyPos = default;
    /// <summary>プレイヤーが枕を返せる位置</summary>
    Vector2 _returnPillowPos = default;
    /// <summary>枕を返す標的のscriptを保持する</summary>
    Returnpillow _pillowEnemy = null;
    /// <summary>枕を返す標的のgameobjectを保持する</summary>
    GameObject _pillowEnemyObject = null;
    /// <summary>大人か子供か</summary>
    [SerializeField, Header("プレイヤーが大人か子供か")]
    bool _adultState = false;
    Animator _anim = null;
    Rigidbody2D _rb;
    UIManager _ui;
    GameManager _gm;
    /// <summary>プレイヤーの状態確認、外部参照用</summary>
    public bool AdultState { get => _adultState; }
    /// <summary>枕を返す標的のscript</summary>
    public Returnpillow PillowEnemy { get => _pillowEnemy; set => _pillowEnemy = value; }
    /// <summary>枕を返す標的のgameobject</summary>
    public GameObject PillowEnemyObject { get => _pillowEnemyObject; set => _pillowEnemyObject = value; }
    public Animator Anim { get => _anim; set => _anim = value; }

    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _ui = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_pillowEnemy);
        float h = _joyStick.Horizontal;
        float v = _joyStick.Vertical;
        ModeCheck(h, v);
        //↑ここで計算された_moveVelocityを代入
        _rb.velocity = _moveVelocity;

        if (h != 0 && v != 0)
        {
            _lastMovejoyStick.x = h;
            _lastMovejoyStick.y = v;
        }

        if (Input.GetButton("Jump"))//スペース長押し
        {
            if (_pillowEnemy)//枕返し圏内にいたら
            {
                TranslatePlayerPos();
                _timer += Time.deltaTime;
                _ui.ChargeSlider(_timer);
            }
        }
        if (Input.GetButtonDown("Jump"))//自動で動くために距離計算を行う,スペースキー一回
        {
            PlayerAndEnemyDis();
        }
        //else if (Input.GetButtonUp("Jump"))
        //{
        //    _timer = 0;
        //    _ui.ChargeSlider(_timer);
        //}
    }


    //void LateUpdate()
    //{
    //    if (_anim)
    //    {
    //        {//動いている間
    //            _anim.SetFloat("floatの名前", _moveVelocity.x);
    //            _anim.SetFloat("floatの名前", _moveVelocity.y);
    //            //上の場合、＋Y
    //            //下の場合、ーY
    //            //右上、右下、右の場合、＋X
    //            //左上、左下、左の場合、ーX
    //        }
    //        {//止まっている間
    //            _anim.SetFloat("", _lastMoveVelocity.x);
    //            _anim.SetFloat("", _lastMoveVelocity.y);
    //            //上の条件に加えて、プレイヤーが動いていないこと。
    //        }
    //    }
    //}

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

    private void ModeCheck(float h, float v)
    {
        if (!_adultState)
        {
            _moveVelocity = new Vector2(h, v).normalized * _childMoveSpeed;
        }
        else
        {
            _moveVelocity = new Vector2(h, v).normalized * _adultMoveSpeed;
        }
        AnimPlay(h, v);
    }
    public void ModeChange(bool change)//大人化、子供化するときに呼び出す関数
    {
        if (!_adultState)
        {
            _anim.Play("");
        }
        else
        {
            _anim.Play("");
        }
        _adultState = change;
    }
    void AnimPlay(float x, float y)//+xが右、+yが上
    {
        if (!_anim)
        {
            return;
        }
        if (!_adultState)
        {
            if (x != 0 && y != 0)
            {
                if (-0.5 < y && y < 0.5)//左右
                {
                    if (0.5 < x && x < 1)//右
                    {
                        _anim.Play("ChildRight");
                    }
                    else if (-1 < x && x < -0.5)//左
                    {
                        _anim.Play("ChildLeft");
                    }
                }
                if (-0.5 < x && x < 0.5)//上下
                {
                    if (0.5 < y && y < 1)//上
                    {
                        _anim.Play("ChildUp");
                    }
                    else if (-1 < y && y < -0.5)//下
                    {
                        _anim.Play("ChildDown");
                    }
                }
            }
            else
            {
                if (-0.5 < _lastMovejoyStick.y && _lastMovejoyStick.y < 0.5)//左右
                {
                    if (0.5 < _lastMovejoyStick.x && _lastMovejoyStick.x < 1)//右
                    {
                        _anim.Play("Player-Idle-right");
                    }
                    else if (-1 < _lastMovejoyStick.x && _lastMovejoyStick.x < -0.5)//左
                    {
                        _anim.Play("Player-Idle-left");
                    }
                }
                if (-0.5 < _lastMovejoyStick.x && _lastMovejoyStick.x < 0.5)//上下
                {
                    if (0.5 < _lastMovejoyStick.y && _lastMovejoyStick.y < 1)//上
                    {
                        _anim.Play("Player-Idle-Up");
                    }
                    else if (-1 < _lastMovejoyStick.y && _lastMovejoyStick.y < -0.5)//下
                    {
                        _anim.Play("Player-Idle-down");
                    }
                }
            }
        }
        else
        {
            if (x != 0 && y != 0)
            {
                if (-0.5 < y && y < 0.5)//左右
                {
                    if (0.5 < x && x < 1)//右
                    {
                        _anim.Play("");
                    }
                    else if (-1 < x && x < -0.5)//左
                    {
                        _anim.Play("");
                    }
                }
                if (-0.5 < x && x < 0.5)//上下
                {
                    if (0.5 < y && y < 1)//上
                    {
                        _anim.Play("");
                    }
                    else if (-1 < y && y < -0.5)//下
                    {
                        _anim.Play("");
                    }
                }
            }
            else
            {
                if (-0.5 < _lastMovejoyStick.y && _lastMovejoyStick.y < 0.5)//左右
                {
                    if (0.5 < _lastMovejoyStick.x && _lastMovejoyStick.x < 1)//右
                    {
                        _anim.Play("");
                    }
                    else if (-1 < _lastMovejoyStick.x && _lastMovejoyStick.x < -0.5)//左
                    {
                        _anim.Play("");
                    }
                }
                if (-0.5 < _lastMovejoyStick.x && _lastMovejoyStick.x < 0.5)//上下
                {
                    if (0.5 < _lastMovejoyStick.y && _lastMovejoyStick.y < 1)//上
                    {
                        _anim.Play("");
                    }
                    else if (-1 < _lastMovejoyStick.y && _lastMovejoyStick.y < -0.5)//下
                    {
                        _anim.Play("");
                    }
                }
            }
        }
    }
    public void InformationReset()//全消し用
    {
        _pillowEnemyObject = null;
        _pillowEnemy = null;
        _enemyPos = default;
        _timer = 0;
        _ui.ChargeSlider(_timer);
    }

    private void PlayerAndEnemyDis()//距離計算
    {
        if (_enemyPos != default)
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
        else
        {
            Debug.Log("敵の位置情報を取得出来ていません");
        }
    }
    private void TranslatePlayerPos()
    {
        if (_pillowEnemyObject)//Enemy情報を持っていたら
        {
            _returnPillowDisToPlayer = Vector2.Distance(transform.position, _returnPillowPos);
            if (_returnPillowDisToPlayer > toleranceDis)//誤差範囲
            {
                if (Mathf.Abs(transform.position.x - _returnPillowPos.x) > toleranceDis)
                {
                    if (transform.position.x > _returnPillowPos.x)
                    {
                        transform.Translate(Vector2.left * Time.deltaTime * _childMoveSpeed);
                        if (!_adultState)
                        {
                            _anim.Play("ChildLeft");
                        }
                        else
                        {
                            _anim.Play("ChildLeft");
                        }
                    }
                    else if (transform.position.x < _returnPillowPos.x)
                    {
                        transform.Translate(Vector2.right * Time.deltaTime * _childMoveSpeed);
                        if (!_adultState)
                        {
                            _anim.Play("ChildRight");
                        }
                        else
                        {
                            _anim.Play("ChildLeft");
                        }
                    }
                }
                else
                {
                    if (transform.position.y > _returnPillowPos.y)
                    {
                        transform.Translate(Vector2.down * Time.deltaTime * _childMoveSpeed);
                        if (!_adultState)
                        {
                            _anim.Play("ChildDown");
                        }
                        else
                        {
                            _anim.Play("ChildLeft");
                        }
                    }
                    else if (transform.position.y < _returnPillowPos.y)
                    {
                        transform.Translate(Vector2.up * Time.deltaTime * _childMoveSpeed);
                        if (!_adultState)
                        {
                            _anim.Play("ChildUp");
                        }
                        else
                        {
                            _anim.Play("ChildLeft");
                        }
                    }
                }
            }
            else
            {
                if (_returnPillowPos == new Vector2(_enemyPos.x + _returnPillowDisToEnemy, _enemyPos.y))
                {
                    if (!_adultState)
                    {
                        _anim.Play("Player-Idle-left");//子供
                    }
                    else
                    {
                        _anim.Play("Player-Idle-left");
                    }
                }
                else
                {
                    if (!_adultState)
                    {
                        _anim.Play("Player-Idle-right");//子供
                    }
                    else
                    {
                        _anim.Play("Player-Idle-left");
                    }
                }
                _timer += Time.deltaTime;
                _ui.ChargeSlider(_timer);
            }
        }
    }
}
    /// <summary>見つかった場合呼ぶ,アニメーションイベント専用関数</summary>
    public void PlayerFind()
    {
        _gm.GameOver();
    }
}
