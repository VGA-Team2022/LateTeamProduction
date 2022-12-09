using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>残り時間の初期値</summary>
    [SerializeField] float _timeLimit = 300;
    /// <summary>残りの敵（枕）の数</summary>
    [SerializeField] int _sleepingEnemy = 0;
    /// <summary>ゲームオーバー時に表示するUI </summary>
    [SerializeField] GameObject _gameOverUI = null;
    [SerializeField] UIManager _uIManager = default;
    PlayerController _player = default;
    public int SleepingEnemy { get => _sleepingEnemy; set => _sleepingEnemy = value; }
    public PlayerController Player { get => _player; }

    public bool _isGame = false; 

    MapInstance _mapInstance = default;


    /// <summary>残り時間</summary>
    float _time = 0;
    // Start is called before the first frame update
    void Start()
    {
        //_mapInstance = 
        //_sleepingEnemy = _mapInstance.Entity.SleeperValue;
        _player = FindObjectOfType<PlayerController>();
        _gameOverUI.SetActive(false);
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
        if(_sleepingEnemy == 0)
        {
            Clear();
        }
    }

    private void Clear()
    {
        _uIManager.Clear(_timeLimit - _time);
        //Scene移動用関数を使用

    }

    private void Timer()
    {
        _time += Time.deltaTime;
        _uIManager.TimerText(_timeLimit-_time);
    }
    public void GameOver()
    {
        _gameOverUI.SetActive(true);
        _isGame = false;
    }
}
