using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class MapElemant : ScriptableObject
{
	public List<MapElemantEntity> MapTable; // Replace 'EntityType' to an actual type that is serializable.
}
