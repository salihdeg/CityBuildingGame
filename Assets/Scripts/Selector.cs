using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selector : MonoBehaviour
{
    public static Selector Instance { get; private set; }

    private Camera _camera;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    private void Start()
    {
        _camera = Camera.main;
    }

    public Vector3 GetCurrentTilePosition()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return new Vector3(0f, -99f, 0f);
        }

        Plane plane = new Plane(Vector3.up, Vector3.zero);

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (plane.Raycast(ray, out float rayOut))
        {
            Vector3 newPos = ray.GetPoint(rayOut) - new Vector3(0.05f, 0f, 0.05f);

            newPos = new Vector3(Mathf.CeilToInt(newPos.x), 0f, Mathf.CeilToInt(newPos.z));
            return newPos;
        }

        return new Vector3(0f, -99f, 0f);
    }

}
