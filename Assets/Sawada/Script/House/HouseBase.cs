using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class HouseBase : MonoBehaviour
{
    [Tooltip("GameManagerÇäiî[Ç∑ÇÈïœêî")]
    GameManager _gameManager = null;
    [Tooltip("â∆ÇÃíÜÇ…Ç¢ÇÈñçÇÃêî")]
    protected Returnpillow[] _returnPillows = new Returnpillow[3];

    public virtual void Init() { }
    public virtual void PlayerEntryHouseMotion(PlayerController player) { }
    public virtual void PlayerInHouseMotion(PlayerController player) { }
    public virtual void PlayerExitHouseMotion(PlayerController player) { }
    public virtual void OnTriggerEnter2D(Collider2D collision) { }
    public virtual void OnTriggerStay2D(Collider2D collision) { }
    public virtual void OnTriggerExit2D(Collider2D collision) { }

    public virtual void OnEnable()
    {
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        _returnPillows = GetComponentsInChildren<Returnpillow>();
        //_returnPillows.Select(x => x.SetGetUpTime(_getUpTime));
        Init();
    }
}
public enum HouseType
{
    None = 0,
    Baby = 1,
    Solt = 2,
    DemonArrow = 3
}

