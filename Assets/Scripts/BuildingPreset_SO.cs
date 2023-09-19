using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildingPreset", menuName = "New Building Preset")]
public class BuildingPreset_SO : ScriptableObject
{
    public int cost;
    public int costPerTurn;
    public GameObject prefab;
    public BuildingType buildingType;

    public int population;
    public int jobs;
    public int food;
}
