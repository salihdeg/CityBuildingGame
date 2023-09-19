using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{
    private bool _currentlyPlacing;
    private bool _currentlyDeleting;

    private BuildingPreset_SO _currentBuildingPreset;

    private float _indicaterUpdateTime = 0.05f;
    private float _lastUpdateTime;
    private Vector3 _currentIndicatorPos;

    [SerializeField] private GameObject _placamentIndicator;
    [SerializeField] private GameObject _deleteIndicator;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CancelBuildingPlacemant();
        }

        if (Time.time - _lastUpdateTime > _indicaterUpdateTime)
        {
            _lastUpdateTime = Time.time;

            _currentIndicatorPos = Selector.Instance.GetCurrentTilePosition();

            if (_currentlyPlacing)
            {
                _placamentIndicator.transform.position = _currentIndicatorPos;
            }
            else if (_currentlyDeleting)
            {
                _deleteIndicator.transform.position = _currentIndicatorPos;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            _placamentIndicator.transform.eulerAngles += new Vector3(0f, 90f, 0f);
        }

        if (Input.GetMouseButtonDown(0) && _currentlyPlacing && _currentIndicatorPos != new Vector3(0f, -99f, 0f))
        {
            PlaceBuilding();
        }

        if (Input.GetMouseButtonDown(0) && _currentlyDeleting && _currentIndicatorPos != new Vector3(0f, -99f, 0f))
        {
            DeleteObj();
        }
    }

    public void BeginNewBuildingPlacement(BuildingPreset_SO buildingPreset_SO)
    {
        if (City.Instance.money < buildingPreset_SO.cost)
            return;

        _currentlyPlacing = true;
        _currentBuildingPreset = buildingPreset_SO;

        CloseAllChilds(_placamentIndicator.transform);
        ActivateSpecificChild(_placamentIndicator.transform, (int)buildingPreset_SO.buildingType);

        _placamentIndicator.SetActive(true);

        // If Begin Placement Cancel Delete
        _currentlyDeleting = false;
        _deleteIndicator.SetActive(false);
    }

    public void ToggleDelete()
    {
        _currentlyDeleting = !_currentlyDeleting;
        _deleteIndicator.SetActive(_currentlyDeleting);

        if (_currentlyPlacing)
        {
            _placamentIndicator.SetActive(false);
            _currentlyPlacing = false;
        }
    }

    private void CloseAllChilds(Transform parentTransform)
    {
        foreach (Transform item in parentTransform)
        {
            item.gameObject.SetActive(false);
        }
    }

    private void ActivateSpecificChild(Transform parentTransform, int childIndex)
    {
        parentTransform.GetChild(childIndex).gameObject.SetActive(true);
    }

    private void PlaceBuilding()
    {
        // Check position is not empty
        if(City.Instance.buildings.Find(b => b.transform.position == _currentIndicatorPos) != null)
        {
            return;
        }

        GameObject prefab = _currentBuildingPreset.prefab;

        GameObject buildingObj = Instantiate(prefab, _currentIndicatorPos, Quaternion.Euler(_placamentIndicator.transform.eulerAngles));

        City.Instance.OnPlaceBuilding(buildingObj.GetComponent<Building>());
    }

    private void DeleteObj()
    {
        Building buildingToDestroy = City.Instance.buildings.Find(x=> x.transform.position == _currentIndicatorPos);
        if (buildingToDestroy != null)
        {
            City.Instance.OnRemoveBuilding(buildingToDestroy);
        }
    }

    private void CancelBuildingPlacemant()
    {
        _currentlyPlacing = false;
        _placamentIndicator.SetActive(false);
    }
}
