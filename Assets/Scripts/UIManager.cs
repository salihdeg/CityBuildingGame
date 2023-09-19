using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private bool _isMenuOpen = false;

    public void ToggleMenu(RectTransform toolbarRect)
    {
        if (_isMenuOpen)
        {
            _isMenuOpen = false;
            StartCoroutine(MoveUI(toolbarRect, new Vector3(0f, 0f, 0f)));
        }
        else
        {
            StartCoroutine(MoveUI(toolbarRect, new Vector3(0f, -200f, 0f)));
            _isMenuOpen = true;
        }
    }

    private IEnumerator MoveUI(RectTransform toolbar, Vector3 target)
    {
        float t = 0f;
        while (t < 1.5f)
        {
            toolbar.anchoredPosition = Vector3.Lerp(toolbar.anchoredPosition, target, t);
            t += Time.deltaTime;
            yield return null;
        }
    }
}
