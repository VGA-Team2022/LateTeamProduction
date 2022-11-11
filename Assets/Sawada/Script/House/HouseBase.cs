using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HouseBase : MonoBehaviour
{
    Returnpillow[] _returnPillows = new Returnpillow[3];
    bool _hangingScroll = false;

    public virtual void OnEnable()
    {
        
    }
    public virtual void ObjectEntryMotion(PlayerController player) { }
    public virtual void OnTriggerEnter2D(Collider2D collision) { }
    public virtual void OnTriggerExit2D(Collider2D collision) { }
}
public enum HouseType
{
    None = 0,
    Baby = 1,
    Solt = 2,
    DemonArrow = 3
}

