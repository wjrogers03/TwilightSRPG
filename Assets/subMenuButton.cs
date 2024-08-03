using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class subMenuButton : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI DisplayText;
    public Action callback;

    public void OnPointerClick(PointerEventData eventData)
    {
        onSelect();
    }

    public void onSelect()
    {
        callback();
    }
}
