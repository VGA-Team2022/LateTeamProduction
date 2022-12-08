using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHousePool
{
    public void Activate();
    public void Desactivate();

    public void CreateHouseObject(HouseBase house, HouseBase house2);
}
