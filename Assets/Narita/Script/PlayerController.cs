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
    [SerializeField, Header("誤差の許容範囲")]
    float _toleranceDis = 0.2f;
    /// <summary>時計</summary>
    float _timer = 0f;
    /// <summary>敵からどれだけ離れた距離から枕を返すかの間隔</summary>
    float _returnPillowDisToEnemy = 0.5f;
    /// <summary>プレイヤーと_returnPillowPosの距離</summary>
    float _returnPillowDisToPlayer;
    /// <summary>移動速度計算結果</summary>
    Vector2 _moveVelocity;
    /// <summary>最後に移動していた方向</summary>
    Vector2 _lastMoveVelocity;
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
    [SerializeField, Header("枕を返せる場所にいるかどうか")]
    bool _returnPillowInPos = false;
    bool _autoAnim = false;
    Rigidbody2D _rb;
    UIManager _ui;
    GameManager _gm;
    Animator _anim = null;
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
        _anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        float _joyX = _joyStick.Horizontal;
        float _joyY = _joyStick.Vertical;
        Debug.Log(_rb.velocity);
        ModeCheck(_joyX, _joyY);
        //↑ここで計算された_moveVelocityを代入
        _rb.velocity = _moveVelocity;
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
            PlayerAndEnemyDis();
        }
    }
    private void LateUpdate()
    {
        if (!_anim)
            return;
        _anim.SetFloat("veloX", _rb.velocity.x);
        _anim.SetFloat("veloY", _rb.velocity.y);
        _anim.SetFloat("LastVeloX", _lastMoveVelocity.x);
        _anim.SetFloat("LastVeloY", _lastMoveVelocity.y);
        _anim.SetBool("_adultState", _adultState);
        _anim.SetBool("_returnPillowInPos", _returnPillowInPos);
        _anim.SetBool("_autoMode", _autoAnim);
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
        if (h != 0 && v != 0)
        {
            _autoAnim = false;
        }
        _moveVelocity = !_adultState ?
            new Vector2(h, v).normalized * _childMoveSpeed : new Vector2(h, v).normalized * _adultMoveSpeed;
    }
    private void VelocitySave(Vector2 velo)
    {
        if (velo != Vector2.zero)
            _lastMoveVelocity = velo;
    }
    public void ModeChange(bool change)//大人化、子供化するときに呼び出す関数
    {
        _adultState = change;
    }
    public void InformationReset()//取得したデータ全消し、スライダーの初期化
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
            _returnPillowPos = Vector2.Distance(transform.position, new Vector2(_enemyPos.x - _returnPillowDisToEnemy, _enemyPos.y))
            >= Vector2.Distance(transform.position, new Vector2(_enemyPos.x + _returnPillowDisToEnemy, _enemyPos.y)) ?
            new Vector2(_enemyPos.x + _returnPillowDisToEnemy, _enemyPos.y) : new Vector2(_enemyPos.x - _returnPillowDisToEnemy, _enemyPos.y);
        else
            Debug.Log("敵の位置情報を取得出来ていません");
    }
    private void TranslatePlayerPos(float speed)
    {
        if (_pillowEnemyObject)//Enemy情報を持っていたら
        {
            _autoAnim = true;
            _returnPillowDisToPlayer = Vector2.Distance(transform.position, _returnPillowPos);
            if (_returnPillowDisToPlayer > _toleranceDis)//誤差範囲
            {
                if (Mathf.Abs(transform.position.x - _returnPillowPos.x) > _toleranceDis)
                {
                    if (transform.position.x > _returnPillowPos.x)
                    {
                        transform.Translate(Vector2.left * Time.deltaTime * speed);
                        _rb.velocity = Vector2.left;
                    }
                    else if (transform.position.x < _returnPillowPos.x)
                    {
                        transform.Translate(Vector2.right * Time.deltaTime * speed);
                        _rb.velocity = Vector2.right;
                    }
                }
                else
                {
                    if (transform.position.y > _returnPillowPos.y)
                    {
                        transform.Translate(Vector2.down * Time.deltaTime * speed);
                        _rb.velocity = Vector2.down;
                    }
                    else if (transform.position.y < _returnPillowPos.y)
                    {
                        transform.Translate(Vector2.up * Time.deltaTime * speed);
                        _rb.velocity = Vector2.up;
                    }
                }
            }
            else
                _returnPillowInPos = true;
                _timer += Time.deltaTime;
                _ui.ChargeSlider(_timer);
        }
    }
    /// <summary>見つかった場合呼ぶ,アニメーションイベント専用関数</summary>
    public void PlayerFind()
    {
        _gm.GameOver();
    }
}
