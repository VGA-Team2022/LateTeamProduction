using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
/// <summary>徘徊する敵の動きを制御するscript</summary>
public class NakaiEnemy : MonoBehaviour//辺りを見回すのはアニメーション内でコライダーの向きを変更すれば良い。
{
    [SerializeField]
    float _moveSpeed = 5f;
    [Tooltip("目標との距離の余裕")]
    float _pointDis = 0.5f;
    float timer = 0f;
    [Tooltip("ナカイの動きが変わる時のプレイヤーのレベル")]
    int _stageLevelBorder = 0;
    /// <summary>配列番号</summary>
    int arrayNumber = 0;
    [SerializeField, Header("プレイヤーを見つけているかどうか")]
    bool _playerFind = false;
    bool _levelBorder = false;
    [Tooltip("アニメーションイベント用,徘徊アニメーションが一周したらtrue")]
    bool _lookAround = false;
    Transform[] points = null;
    Vector2 _dir = default;
    /// <summary>最後に移動していた方向</summary>
    Vector2 _lastMoveVelocity = default;
    Animator _anim = null;
    Rigidbody2D _rb = null;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (points != null)//ポイントを受け取っている
        {
            if (!_playerFind)//プレイヤーを見つけていない
            {
                if (!_levelBorder)//見渡す処理をする必要がないステージのレベルじゃなかった場合
                {
                    GotoPoint(points);
                }
                else if(_levelBorder && !_lookAround)//見渡す処理をする必要がないステージのレベルだが、歩いている時
                {
                    GotoPoint(points);
                }
            }
        }
    }
    private void LateUpdate()
    {
        if (!_anim)
            return;
        _anim.SetFloat("lastVeloX", Mathf.Abs(_lastMoveVelocity.x));//のちに名前を決める
        _anim.SetFloat("lastVeloY", Mathf.Abs(_lastMoveVelocity.y));
        _anim.SetBool("_levelBorder", _levelBorder);
        _anim.SetBool("_lookAround", _lookAround);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerFind = true;
            Debug.Log("プレイヤーを見つけました");
            collision.GetComponent<PlayerController>().PlayerFind();
        }
    }
    /// <summary>渡す側は順番に気を付けること</summary>
    /// <param name="pointsArray"></param>
    public void GetPoints(Transform[] pointsArray)
    {
        points = new Transform[pointsArray.Length];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = pointsArray[i];
        }
    }
    public void GetPlayerLevel(int level)
    {
        _levelBorder = _stageLevelBorder <= level ? true : false; 
    }
    /// <summary>渡されたpoint順に進む</summary>
    /// <param name="pointsArray"></param>
    void GotoPoint(Transform[] pointsArray)
    {
        //自分自身とポイントの距離を求める
        float distance = Vector2.Distance(transform.position, pointsArray[pointsArray.Length % arrayNumber].position);
        if (distance >= _pointDis)//距離がなくなる、到達するまで
        {
            _dir = (pointsArray[Mathf.Abs(pointsArray.Length % arrayNumber)].position - transform.position).normalized * _moveSpeed;
            //方向を定める
            transform.Translate(_dir * Time.deltaTime);//一定の速さ
            _rb.velocity = _dir;
        }
        else//到達したら
        {
            //GameManagerの判定用変数で逆に回るようになった場合はarrayNumber--;にする。
            arrayNumber++;
        }
    }
    public void LookAroundIsActive()//アニメーションイベント用
    {
        _lookAround = !_lookAround;
    }
    private void VelocitySave(Vector2 velo)
    {
        if (velo != Vector2.zero)
            _lastMoveVelocity = velo;
    }
}
