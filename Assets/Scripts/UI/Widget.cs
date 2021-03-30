using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Widget : MonoBehaviour
{
    CanvasGroup _cg;

    private void Start()
    {
        _cg = GetComponent<CanvasGroup>();
    }

    public void Show()
    {
        _cg.alpha = 1;
        _cg.interactable = true;
        _cg.blocksRaycasts = true;
    }

    public void Hide()
    {
        _cg.alpha = 0;
        _cg.interactable = false;
        _cg.blocksRaycasts = false;
    }
}
