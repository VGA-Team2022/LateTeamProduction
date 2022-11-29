using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class NakaiEnemy : MonoBehaviour
{
    [SerializeField]
    float _moveSpeed = 5f;
    Rigidbody2D _rb = null;
    /// <summary>配列番号</summary>
    int arrayNumber = 0;
    [SerializeField]
    bool playerFind = false;
    Transform[] points = null;
    Vector2 _dir;
    Animator _anim = null;

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
            if (!playerFind)//プレイヤーを見つけていない
            {
                GotoPoint(points);
            }
        }
    }
    private void LateUpdate()
    {
        if (!_anim)
            return;
        _anim.SetFloat("", _dir.x);//のちに名前を決める
        _anim.SetFloat("", _dir.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerFind = true;
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
    /// <summary>渡されたpoint順に進む</summary>
    /// <param name="pointsArray"></param>
    void GotoPoint(Transform[] pointsArray)
    {
        //自分自身とポイントの距離を求める
        float distance = Vector2.Distance(transform.position, pointsArray[arrayNumber % pointsArray.Length].position);
        if (distance >= 0.5f)//距離がなくなる、到達するまで,0.5fはマジックナンバーになっているためのち変更
        {
            //方向を定める
            _dir = (pointsArray[arrayNumber].position - transform.position).normalized * _moveSpeed;
            transform.Translate(_dir * Time.deltaTime);//一定の速さ
        }
        else//到達したら
        {
            arrayNumber++;
        }
    }
 
}
