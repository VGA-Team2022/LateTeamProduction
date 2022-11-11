using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseOnSolt : HouseBase
{
    Collider2D _collider = null;

    public override void OnEnable()
    {
        
    }
    public void OnDisable()
    {
        
    }
    public override void ObjectEntryMotion(PlayerController player)
    {
        base.ObjectEntryMotion(player);
    }
}
