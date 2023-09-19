using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public BuildingPreset_SO buildingPreset_SO;
}

public enum BuildingType
{
    Road,
    House,
    Factory,
    Barn
}
