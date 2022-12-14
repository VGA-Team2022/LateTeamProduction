using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField,Tooltip("残り時間の初期値")] float _timeLimit = 300;
    
    [SerializeField,Tooltip("残りの敵（枕）の数")] int _sleepingEnemy = 0;
    
    
    
    [SerializeField] UIManager _uIManager = default;
    PlayerController _player = default;
    

    public bool _isGame = false; 

    MapInstance _mapInstance = default;


    public int SleepingEnemy { get => _sleepingEnemy; set => _sleepingEnemy = value; }
    public PlayerController Player { get => _player; }

    /// <summary>残り時間</summary>
    float _time = 0;
    // Start is called before the first frame update
    void Awake()
    {
        //_mapInstance = 
        //_sleepingEnemy = _mapInstance.Entity.SleeperValue; マップインスタンスが完成していないため保留
        _player = FindObjectOfType<PlayerController>();
        GameStart();
    }

    void GameStart()
    {
        _isGame = true;
    }
    // Update is called once per frame
    void Update()
    {
        Timer();
        
    }
    public void CheckSleepingEnemy() 
    {
        _sleepingEnemy--;
        if(_sleepingEnemy == 0)
        {
            Clear();
        }
    }
    private void Clear()
    {
        _uIManager.Clear(_timeLimit - _time);
        _isGame = false;
        //Scene移動用関数を使用

    }
    public void GameOver()
    {
        _uIManager.GameOver();
        _isGame = false;
    }

    private void Timer()
    {
        _time += Time.deltaTime;
        _uIManager.TimerText(_timeLimit-_time);
        if (_time <= 0) GameOver();
    }
    
}
