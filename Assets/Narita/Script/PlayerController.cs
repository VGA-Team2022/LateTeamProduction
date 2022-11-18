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
    /// <summary>_joyStickのX軸の値</summary>
    float _joyX;
    /// <summary>_joyStickのY軸の値</summary>
    float _joyY;
    /// <summary>移動速度計算結果</summary>
    Vector2 _moveVelocity;
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
    Rigidbody2D _rb;
    UIManager _ui;
    GameManager _gm;
    PlayerAnimController _animController;

    /// <summary>_joyStickのX軸の値,外部参照用</summary>
    public float JoyX { get => _joyX; }
    /// <summary>_joyStickのY軸の値,外部参照用</summary>
    public float JoyY { get => _joyY; }
    /// <summary>プレイヤーの状態確認、外部参照用</summary>
    public bool AdultState { get => _adultState; }
    /// <summary>枕を返す標的のscript</summary>
    public Returnpillow PillowEnemy { get => _pillowEnemy; set => _pillowEnemy = value; }
    /// <summary>枕を返す標的のgameobject</summary>
    public GameObject PillowEnemyObject { get => _pillowEnemyObject; set => _pillowEnemyObject = value; }
    public Vector2 ReturnPillowPos { get => _returnPillowPos; }
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _ui = FindObjectOfType<UIManager>();
        _animController = GetComponent<PlayerAnimController>();
    }
    // Update is called once per frame
    void Update()
    {
        float _joyX = _joyStick.Horizontal;
        float _joyY = _joyStick.Vertical;
        ModeCheck(_joyX, _joyY);
        //↑ここで計算された_moveVelocityを代入
        _rb.velocity = _moveVelocity;

        if (Input.GetButton("Jump"))//スペース長押し
        {
            if (_pillowEnemy)//枕返し圏内にいたら
            {
                if (!_adultState)
                {
                    TranslatePlayerPos(_childMoveSpeed);
                }
                else
                {
                    TranslatePlayerPos(_adultMoveSpeed);
                }
                _timer += Time.deltaTime;
                _ui.ChargeSlider(_timer);
            }
        }
        if (Input.GetButtonDown("Jump"))//自動で動くために距離計算を行う,スペースキー一回
        {
            PlayerAndEnemyDis();
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
        _animController.MoveAnim(h, v);
    }
    public void ModeChange(bool change)//大人化、子供化するときに呼び出す関数
    {
        _animController.ModeChangeAnim();
        _adultState = change;
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
    private void TranslatePlayerPos(float speed)
    {
        if (_pillowEnemyObject)//Enemy情報を持っていたら
        {
            _returnPillowDisToPlayer = Vector2.Distance(transform.position, _returnPillowPos);
            if (_returnPillowDisToPlayer > toleranceDis)//誤差範囲
            {
                _animController.TranslatePlayerPosAnimPlay(speed, toleranceDis);
            }
            else
            {
                _animController.RightorLeft(_returnPillowPos);
                _timer += Time.deltaTime;
                _ui.ChargeSlider(_timer);
            }
        }
    }
    /// <summary>見つかった場合呼ぶ,アニメーションイベント専用関数</summary>
    public void PlayerFind()
    {
        _gm.GameOver();
    }
}
