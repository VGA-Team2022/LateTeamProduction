using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController1 : MonoBehaviour
{
    public VariableJoystick _joyStick;
    /// <summary>動く速度（子供）</summary>
    [SerializeField, Header("子供状態の動くスピード")]
    float _childMoveSpeed = 10f;
    /// <summary>動く速度（大人）</summary>
    [SerializeField, Header("大人状態の動くスピード")]
    float _adultMoveSpeed = 15f;
    /// <summary>移動速度計算結果</summary>
    Vector2 _moveVelocity;
    /// <summary>最後に移動していた方向</summary>
    Vector2 _lastMoveVelocity;
    /// <summary>敵の位置情報</summary>
    Vector2 _enemyPos = default;
    /// <summary>大人か子供か</summary>
    [SerializeField, Header("プレイヤーが大人か子供か")]
    bool _adultState = false;
    [SerializeField,Header("枕の横に自動的に移動しているときにtrue")]
    public bool _autoAnim = false;
    Rigidbody2D _rb;
    GameManager _gm;
    UIManager _ui;
    Animator _anim = null;
    /// <summary>プレイヤーの状態確認、外部参照用</summary>
    public bool AdultState { get => _adultState; }
    /// <summary>枕を返す標的のscript</summary>

    //public Vector2 ReturnPillowPos { get => _returnPillowPos; }
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _gm = FindObjectOfType<GameManager>();
        _ui = FindObjectOfType<UIManager>();
    }
    // Update is called once per frame
    void Update()
    {
        float _joyX = _joyStick.Horizontal;
        float _joyY = _joyStick.Vertical;
        //Debug.Log(_rb.velocity);
        ModeCheck(_joyX, _joyY);
        //↑ここで計算された_moveVelocityを代入
        _rb.velocity = _moveVelocity;
        VelocitySave(_rb.velocity); 
    }
    private void LateUpdate()
    {
        if (!_anim)
            return;
        _anim.SetFloat("veloX", _rb.velocity.x);
        _anim.SetFloat("veloY", _rb.velocity.y);
        _anim.SetFloat("LastVeloX", _lastMoveVelocity.x);
        _anim.SetFloat("LastVeloY", _lastMoveVelocity.y);
        _anim.SetBool("adultState", _adultState);
        _anim.SetBool("autoMode", _autoAnim);
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
        if (velo.x != 0)
        {
            _lastMoveVelocity.x = velo.x;
        }
        if(velo.y != 0)
        {
            _lastMoveVelocity.y = velo.y;
        }
    }
    public void ModeChange(bool change)//大人化、子供化するときに呼び出す関数
    {
        _ui.CutIn(_adultState);
        _adultState = change;
    }

    /// <summary>見つかった場合呼ぶ,アニメーションイベント専用関数</summary>
    public void PlayerFind()
    {
        _gm.GameOver();
    }
}
