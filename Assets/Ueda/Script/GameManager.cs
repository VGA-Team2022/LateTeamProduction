using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace IsGame
{
    public class GameManager
    {
        static GameManager _instance = new GameManager();

        PlayerController _player = default;

        [Tooltip("残り時間の初期値")] float _timeLimit = 300;

        [Tooltip("残りの敵（枕）の数")] int _sleepingEnemy = 9;

        [Tooltip("現在のステージ数")]static int _stageIndex = 0;

        UIManager _uIManager = default;


        public bool _isGame = false;

        MapInstance _mapInstance = default;

        public static int StageIndex => _stageIndex;

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("インスタンスがありません。");
                }
                return _instance;
            }
            set => _instance = value;
        }

        public void PlayerSet(PlayerController player)
        {
            _player = player;
        }

        public void UIManagerSet(UIManager uIManager)
        {
            _uIManager = uIManager;
        }

        public int SleepingEnemy { get => _sleepingEnemy; set => _sleepingEnemy = value; }
        public PlayerController Player { get => _player;}
        /// <summary>経過時間</summary>
        float _time = 0;

        // Start is called before the first frame update
        public void GameStart()
        {
            _isGame = true;
            _time = 0;
        }
        
        public void CheckSleepingEnemy()
        {
            _sleepingEnemy--;
            if (_sleepingEnemy == 0)
            {
                Clear();
            }
        }
        private void Clear()
        {
            _uIManager.Clear(_timeLimit - _time);
            _isGame = false;
            _stageIndex++;
        }
        public void GameOver()
        {
            _uIManager.GameOver();
            _isGame = false;
        }

        public void Timer()
        {
            if(_isGame)
            {
                _time += Time.deltaTime;
                _uIManager.TimerText(_timeLimit - _time);
                if (_time >= _timeLimit) GameOver();
            }
            
        }

    }
}

