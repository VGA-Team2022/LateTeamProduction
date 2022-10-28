using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class NakaiEnemy : MonoBehaviour
{
    [SerializeField]
    float _moveSpeed = 5f;
    Rigidbody2D _rb = null;
    float z = 90;
    int number = 0;
    [SerializeField]
    bool playerFind = false;
    bool pointsCheck = false;
    Transform[] points = null;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
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
            else
            {

            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerFind = true;
            Debug.Log("プレイヤーを見つけました");
        }
    }
    public void GetPoints(Transform[] pointsArray)
    {
        points = new Transform[pointsArray.Length];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = pointsArray[i];
        }
    }

    void GotoPoint(Transform[] pointsArray)
    {
        //自分自身とポイントの距離を求める
        float distance = Vector2.Distance(transform.position, pointsArray[number % pointsArray.Length].position);
        if (distance >= 0)//距離がなくなる、到達するまで
        {
            //方向を定める
            Vector2 dir = (pointsArray[number].position - transform.position).normalized * _moveSpeed;
            transform.Translate(dir * Time.deltaTime);//一定の速さ
        }
        else//到達したら
        {
            number++;    
        }
    }
}
