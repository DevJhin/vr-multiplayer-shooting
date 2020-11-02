using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WidgetSwitcher : MonoBehaviour
{
    public bool DisableAllOnAwake;
    public List<GameObject> SubWidgets
    {
        get;
        private set;
    }

    public int CurrentIndex
    {
        get;
        private set;
    }

    public GameObject ActiveWidget
    {
        get;
        private set;
    }

    int ActiveIndex = -1;

    public void Awake()
    {
        SubWidgets = new List<GameObject>();
        SubWidgets = FindChildrenGameObjects(gameObject);

        foreach (var widget in SubWidgets)
        {
            widget.SetActive(false);
        }

        if (SubWidgets.Count > 0 && !DisableAllOnAwake)
        {
            SetActiveWidget(0);
        }
    }

    public static List<GameObject> FindChildrenGameObjects(GameObject parentObject)
    {
        var parentTransform = parentObject.transform;
        int childCount = parentTransform.childCount;
        
        var childrenList = new List<GameObject>();
        for (int i = 0; i < childCount; i++)
        {
            childrenList.Add(parentTransform.GetChild(i).gameObject);    
        }

        return childrenList;
    }


    public void SetActiveWidget(int index)
    {
        if (!IsValidIndex(index)) return;
        if (ActiveIndex == index) return;

        if(ActiveWidget != null)
            ActiveWidget.SetActive(false);

        ActiveIndex = index;

        ActiveWidget = SubWidgets[index];
        ActiveWidget.SetActive(true);
    }

    private bool IsValidIndex(int index)
    {
        if (SubWidgets.Count == 0) return false;
        if (!(index >= 0 && index < SubWidgets.Count)) return false;
        return true;
    }
}
