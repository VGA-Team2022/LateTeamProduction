using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseContainer
{
    public HouseBase houseBase => new HouseBase();
    public HouseInBaby houseInBaby => new HouseInBaby();
    public HouseOnSolt houseOnSolt => new HouseOnSolt();
    public HouseOnDevilArrow houseOnDevilArrow => new HouseOnDevilArrow();
}
