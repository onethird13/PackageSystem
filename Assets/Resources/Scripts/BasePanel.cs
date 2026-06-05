using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    protected bool isRemoved=false;
    protected  string basePanelName;
    public bool isOpen = false;

    public virtual void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public virtual void OpenPanel(string panelName)
    {
        this.name = panelName;
        gameObject.SetActive(true);
    }

    public virtual void ClosePanel(string panelName)
    {
        isRemoved=true;
        gameObject.SetActive(false);
        Destroy(gameObject);
        if (UIManager.Instance.panelDict.ContainsKey(panelName))
        {
            UIManager.Instance.panelDict.Remove(panelName);
        }
        
    }
}
