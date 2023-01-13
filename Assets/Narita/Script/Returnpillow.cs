using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Returnpillow : MonoBehaviour, IRevers
{
    [SerializeField, Header("枕を返す時のプレイヤーの位置"), Tooltip("枕を返す時のプレイヤーの位置情報")]
    Transform _returnPillouPosLeft;
    [SerializeField, Header("枕を返す時のプレイヤーの位置"), Tooltip("枕を返す時のプレイヤーの位置情報")]
    Transform _returnPillouPosRight;
    [SerializeField, Header("枕を返されたかどうか"), Tooltip("枕を返されたかどうか")]
    bool _returnPillow = false;
    [SerializeField, Header("起きたかどうか"), Tooltip("起きたかどうか")]
    bool _getUp = false;
    [SerializeField, Header("Playerが侵入している場合True")]
    bool _playerIntrusion = false;
    [SerializeField, Header("起きる時間（基準）"), Tooltip("起きる時間（関数内で値を決める）")]
    float _getupTime = 5f;
    [SerializeField,Header("playerを見つけてから怒るまでの時間")]
    float _resultDelayTime = 4f;
    [Tooltip("reactionを表示する秒数のborder、_getupCountTimerの半分")]
    float _reactionTime = 2.5f;
    //
    [SerializeField, Header("プレイヤーの状態で変化する時間"), Tooltip("プレイヤーが大人の時値を変化させる")]
    float _timeInPlayerStats = 0f;
    /// <summary>赤ん坊がいた場合使用する</summary>
    [SerializeField, Header("部屋の中に赤ん坊がいた場合変化する時間")]
    float _timeInBaby = 0f;
    //,「//」で囲っている変数はこのscriptが持つべきなのかわからないが、話に上がっているのにどこにも存在しないため定義している。
    [Tooltip("起きるまでの時間をカウントするタイマー")]
    float _getupCountTimer;
    [SerializeField, Tooltip("reactionする場所に設置してあるGameobject")]
    GameObject _reactionObject = null; 
    [SerializeField, Tooltip("もやもやの画像")]
    Sprite _reaction = null;
    [SerializeField,Tooltip("自身のanimator")]
    Animator _returnPillowAnim = null;
    [Tooltip("当たり判定の配列")]
    Collider2D[] collider = null;
    SleepingEnemyAnimControle _sleepHumanController = null;
    [SerializeField, Tooltip("playerを見つけたときに使用")]
    SoundManager _sound = null;
    [Tooltip("枕を返されたかどうか、外部参照用")]
    public bool ReturnPillow { get => _returnPillow; set => _returnPillow = value; }
    [Tooltip("枕を返す時のプレイヤーの位置情報、外部参照用")]
    public Transform ReturnPillouPosLeft { get => _returnPillouPosLeft; }
    public Transform ReturnPillouPosRight { get => _returnPillouPosRight; }
    // Start is called before the first frame update
    void Start()
    {
        _returnPillow = false;
        _reactionObject.SetActive(false);
        _sleepHumanController = GetComponent<SleepingEnemyAnimControle>();
        collider = GetComponents<Collider2D>();
    }


    void Update()
    {
        if(_playerIntrusion)
        {
            _getupCountTimer += Time.deltaTime;
            //カウントがreactionする時間のborderを超えていて、起きる時間より小さかったら
            if (_getupCountTimer > _reactionTime && _getupCountTimer < _getupTime)
            {
                _reactionObject.SetActive(true);
            }
            if (_getupTime < _getupCountTimer)//制限時間を超えた + 枕を返されていなかったら 
            {
                _sound.Discoverd();
                _sleepHumanController.Awaken();
                _getUp = true;
                _sleepHumanController.Discover();
            }
            if(_getupTime + _resultDelayTime < _getupCountTimer )
            {
                GetUp();
            }
        }
    }
    // Update is called once per frame

    private void OnTriggerStay2D(Collider2D collision)//プレイヤーが当たり判定の中にとどまったら
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player))
        {
            _playerIntrusion = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _getupCountTimer = 0;
        _playerIntrusion = false;
        _reactionObject.SetActive(false);
    }
    /// <summary>起きる時間を決める、プレイヤーの状態が変化したときに呼ぶ関数</summary>
    /// <param name="time"></param>
    public void GetUpTimeAndTimeInPlayerStats(float getuptime)
    {
        _getupTime = getuptime;
    }

    public void GetUp()//アニメーションイベント用
    {
        IsGame.GameManager.Instance.GameOver();
    }

    public void ObjectRevers()
    {
        _returnPillow = true;
        if (_returnPillowAnim)
            _returnPillowAnim.SetBool("returnPillowPlay", _returnPillow);
        foreach (var col in collider)
        {
            col.enabled = false;
        }
    }
}