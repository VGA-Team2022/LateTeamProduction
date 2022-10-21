using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform[] _points = default;
    [SerializeField] float _moveSpeed = 1.0f;
    [SerializeField] float _stoppingDistance = 0.05f;
    int n = 0;
    
    void Update()
    {
        //MoveToTarget0();
        Patrol();
    }

    void MoveToTarget0()
    {
        float distance = Vector2.Distance(this.transform.position, _points[1].position);
        
        if(distance > _stoppingDistance)
        {
            Vector2 dir = (_points[0].transform.position - this.transform.position).normalized * _moveSpeed;

            this.transform.Translate(dir * Time.deltaTime);
        }
    }

    void Patrol()
    {
        float distance = Vector2.Distance(this.transform.position, _points[n].position);
        if(distance > _stoppingDistance)
        {
            Vector2 dir = (_points[n].transform.position - this.transform.position).normalized * _moveSpeed;
            this.transform.Translate(dir * Time.deltaTime);
        }
        else
        {
            n = (n + 1) % _points.Length;
        }
    }
}

