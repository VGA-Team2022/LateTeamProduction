using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class NakaiEnemy : MonoBehaviour
{

    float _moveSpeed = 5f;
    Rigidbody2D _rb = null;
    float z = 90;
    int number = 0;
    [SerializeField]
    bool playerFind = false;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerFind)
        {
            switch (number % 4)//0%4 = 0;1%4 = 1;...
            {
                case 0:
                    {
                        _rb.velocity = Vector2.up * _moveSpeed;
                        break;
                    }
                case 1:
                    {
                        _rb.velocity = Vector2.left * _moveSpeed;
                        break;
                    }
                case 2:
                    {
                        _rb.velocity = Vector2.down * _moveSpeed;
                        break;
                    }
                case 3:
                    {
                        _rb.velocity = Vector2.right * _moveSpeed;
                        break;
                    }
            }
        }
        else
        {

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerFind = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.Rotate(0.0f, 0.0f, z);
        number++;
    }
}
