using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HouseBase : MonoBehaviour
{
    [SerializeField,Tooltip("Š|‚¯Ž²")]
    HangingScroll _hangingScroll = null;
    [Tooltip("GameManager‚ðŠi”[‚·‚é•Ï”")]
    GameManager _gameManager = null;
    [Tooltip("‰Æ‚Ì’†‚É‚¢‚é–‚Ì”")]
    protected Returnpillow[] _returnPillows = new Returnpillow[3];
    [Tooltip("‰Æ‚ÌŽí—Þ")]
    protected HouseType _type = HouseType.None;

    public HouseType Type => _type;

    public void SetValue(bool isHangingScroll,GameManager gameManager)
    {
        _gameManager = gameManager;
        _hangingScroll.IsActive(isHangingScroll);
        _returnPillows = GetComponentsInChildren<Returnpillow>();
        Init();
    }
    public virtual void Init() { }
    public virtual void PlayerEntryHouseMotion(PlayerController player) { }
    public virtual void PlayerInHouseMotion(PlayerController player) { }
    public virtual void PlayerExitHouseMotion(PlayerController player) { }

    public virtual void OnEnable()
    {
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        _returnPillows = GetComponentsInChildren<Returnpillow>();
        //_returnPillows.Select(x => x.SetGetUpTime(_getUpTime));
        Init();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player))
        {
            PlayerEntryHouseMotion(player);
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player))
        {

            PlayerInHouseMotion(player);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player))
        {

            PlayerExitHouseMotion(player);
        }
    }
}
public enum HouseType
{
    None = 0,
    Baby = 1,
    Solt = 2,
    DevilArrow = 3
}

