using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public VariableJoystick _joyStick;
    [SerializeField, Header("子供状態の動くスピード"), Tooltip("動く速度（子供）")]
    float _childMoveSpeed = 10f;
    [SerializeField, Header("大人状態の動くスピード"), Tooltip("動く速度（大人）")]
    float _adultMoveSpeed = 15f;
    [SerializeField, Header("誤差の許容範囲"), Tooltip("誤差の許容範囲")]
    float _toleranceDis = 0.5f;
    [Tooltip("枕を返す時のカウント用タイマー")]
    float _returnCountTime = 0f;
    ///// <summary>敵からどれだけ離れた距離から枕を返すかの間隔</summary>
    //float _returnPillowDisToEnemy = 0.5f;
    [Tooltip("プレイヤーと_returnPillowPosの距離")]
    float _returnPillowDisToPlayer;
    [Tooltip("移動速度計算結果")]
    Vector2 _moveVelocity;
    [Tooltip("動かなくなった時の最後に進んでいた方向")]
    Vector2 _lastMoveVelocity;
    [Tooltip("プレイヤーが枕を返せる位置情報")]
    Vector2 _returnPillowPos = default;
    [Tooltip("寝ている敵のscript情報")]
    Returnpillow _pillowEnemy = null;
    [Tooltip("寝ている敵そのもの")]
    GameObject _pillowEnemyObject = null;
    [SerializeField, Header("プレイヤーが大人か子供か"), Tooltip("大人の時True")]
    bool _adultState = false;
    [SerializeField, Header("枕を返そうとしている時True"), Tooltip("枕を返せる位置にいてスペースキーを押しているときTrue")]
    bool _returnPillowInPos = false;
    [SerializeField, Header("枕の横に自動的に移動しているときにtrue"), Tooltip("枕の横に自動的に移動しているときにtrue")]
    bool _autoAnim = false;
    [SerializeField, Header("プレイヤーが動いている時True"), Tooltip("プレイヤーのvelocityが0ではない場合True")]
    bool _playerMove = false;
    [SerializeField, Tooltip("スライダーに値を渡すために使用")]
    UIManager _ui = null;
    [SerializeField, Tooltip("敵の範囲内に入ったとき、出たときに使用")]
    SoundManager _sound = null;
    Rigidbody2D _rb;
    Animator _playerAnim = null;
    [Tooltip("プレイヤーの状態確認、外部参照用")]
    public bool AdultState { get => _adultState; }
    [Tooltip("寝ている敵のscript情報、外部参照用")]
    public Returnpillow PillowEnemy { get => _pillowEnemy; set => _pillowEnemy = value; }
    [Tooltip("寝ている敵そのもの、外部参照用")]
    public GameObject PillowEnemyObject { get => _pillowEnemyObject; }
    [Tooltip("枕を返せる位置にいてスペースキーを押しているときTrue, 外部参照用")]
    public Vector2 ReturnPillowPos { get => _returnPillowPos; }
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        float _joyX = _joyStick.Horizontal;
        float _joyY = _joyStick.Vertical;
        ModeCheck(_joyX, _joyY);
        if (!_autoAnim)
        {
            _rb.velocity = _moveVelocity;
        }
        VelocitySave(_rb.velocity);
        if (Input.GetButton("Jump"))//スペース長押し
        {
            if (_pillowEnemy)//枕返し圏内にいたら
            {
                if (!_adultState)
                    TranslatePlayerPos(_childMoveSpeed);
                else
                    TranslatePlayerPos(_adultMoveSpeed);
            }
        }
        else
        {
            _returnPillowInPos = false;
        }
        if (Input.GetButtonDown("Jump"))//自動で動くために距離計算を行う,スペースキー一回
        {
            if (_pillowEnemy)//枕返し圏内にいたら
            {
                PlayerAndEnemyDis();
            }
        }
        if (!_playerAnim)
        {
            return;
        }
        else
        {
            _playerAnim.SetFloat("veloX", _rb.velocity.x);
            _playerAnim.SetFloat("veloY", _rb.velocity.y);
            _playerAnim.SetFloat("LastVeloX", _lastMoveVelocity.x);
            _playerAnim.SetFloat("LastVeloY", _lastMoveVelocity.y);
            _playerAnim.SetBool("playerMove", _playerMove);
            _playerAnim.SetBool("adultState", _adultState);
            _playerAnim.SetBool("returnPillowInPos", _returnPillowInPos);
            _playerAnim.SetBool("autoMode", _autoAnim);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)//寝ている敵の情報を取る
    {
        if (collision.TryGetComponent<Returnpillow>(out Returnpillow enemy))
        {
            _sound.SleepingVoice();
            _pillowEnemyObject = collision.gameObject;
            _pillowEnemy = enemy;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        InformationReset();
        _sound.KillSleeping();
    }

    private void ModeCheck(float h, float v)
    {
        if (h != 0 && v != 0)
        {
            _autoAnim = false;
        }
        _moveVelocity = !_adultState ?
           new Vector2(h, v).normalized * _childMoveSpeed : new Vector2(h, v).normalized * _adultMoveSpeed;
    }
    private void VelocitySave(Vector2 velo)
    {
        _playerMove = velo == Vector2.zero ? false : true;
        if (velo.x != 0)
        {
            _lastMoveVelocity.x = velo.x;
        }
        if (velo.y != 0)
        {
            _lastMoveVelocity.y = velo.y;
        }
    }
    public void ModeChange(bool change)//大人化、子供化するときに呼び出す関数
    {
        _adultState = change;
    }
    public void InformationReset()//取得したデータ全消し、スライダーの初期化
    {
        _pillowEnemyObject = null;
        _pillowEnemy = null;
        _returnCountTime = 0;
        _ui.ChargeSlider(_returnCountTime);
    }
    private void PlayerAndEnemyDis()//距離計算
    {
        if (!_pillowEnemyObject)
            return;
        _returnPillowPos = Vector2.Distance(transform.position, _pillowEnemy.ReturnPillouPos[0].position)
        >= Vector2.Distance(transform.position, _pillowEnemy.ReturnPillouPos[1].position) ?
        _pillowEnemy.ReturnPillouPos[1].position : _pillowEnemy.ReturnPillouPos[0].position;
    }
    private void TranslatePlayerPos(float speed)
    {
        if (!_pillowEnemyObject)
            return;
        _autoAnim = true;
        _returnPillowDisToPlayer = Vector2.Distance(transform.position, _returnPillowPos);
        if (_returnPillowDisToPlayer > _toleranceDis)//誤差範囲
        {
            if (Mathf.Abs(transform.position.x - _returnPillowPos.x) > _toleranceDis)
            {
                if (transform.position.x > _returnPillowPos.x)
                {
                    transform.Translate(Vector2.left * Time.deltaTime * speed);
                    _rb.velocity = Vector2.left * Time.deltaTime * speed;
                }
                else if (transform.position.x < _returnPillowPos.x)
                {
                    transform.Translate(Vector2.right * Time.deltaTime * speed);
                    _rb.velocity = Vector2.right * Time.deltaTime * speed;
                }
            }
            else
            {
                if (transform.position.y > _returnPillowPos.y)
                {
                    transform.Translate(Vector2.down * Time.deltaTime * speed);
                    _rb.velocity = Vector2.down * Time.deltaTime * speed;
                }
                else if (transform.position.y < _returnPillowPos.y)
                {
                    transform.Translate(Vector2.up * Time.deltaTime * speed);
                    _rb.velocity = Vector2.up * Time.deltaTime * speed;
                }
            }
        }
        else
            _returnPillowInPos = true;
        _returnCountTime += Time.deltaTime;
        _ui.ChargeSlider(_returnCountTime);
    }
    /// <summary>見つかった場合呼ぶ,アニメーションイベント専用関数</summary>
    public void PlayerFind()
    {
        IsGame.GameManager.Instance.GameOver();
    }
}
