using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] float _timeLimit =300; //Žc‚èŽžŠÔ
    [SerializeField] UIManager _uIManager = default;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _timeLimit -= Time.deltaTime;
        _uIManager.TimerText(_timeLimit);
    }
}
