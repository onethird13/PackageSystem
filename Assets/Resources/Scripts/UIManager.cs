using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private static UIManager _instance;

    private UIManager()
    {
        InitDicts();
    }

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UIManager();
            }

            return _instance;
        }
    }

    private Transform uiRoot;

    //路径配置字典
    public Dictionary<string, string> pathDict;

    //预制体缓存字典
    public Dictionary<string, GameObject> prefabDict;

    //已打开界面的缓存字典
    public Dictionary<string, BasePanel> panelDict;

    public Transform UIRoot
    {
        get
        {
            if (uiRoot == null)
            {
                if (GameObject.Find("Canvas"))
                {
                    uiRoot = GameObject.Find("Canvas").transform;
                }
                else
                {
                    uiRoot = new GameObject("Canvas").transform;
                }
            }

            Debug.Log(uiRoot.name);
            return uiRoot;
        }
    }

    private void InitDicts()
    {
        panelDict = new Dictionary<string, BasePanel>();
        pathDict = new Dictionary<string, string>()
        {
            {UIConst.PackagePanel,"Package/PackagePanel"},
            {UIConst.Menu,"Package/Menu"},
            {UIConst.LotteryPanel,"Package/LotteryPanel"},
        };
        prefabDict = new Dictionary<string, GameObject>();
    }

    public BasePanel GetPanel(string panelName)
    {
        BasePanel panel = null;
        if (panelDict.TryGetValue(panelName, out panel))
        {
            return panel;
        }

        return null;
    }

    public BasePanel OpenPanel(string panelName)
    {
        BasePanel panel = null;
        //检查是否已打开
        if (panelDict.TryGetValue(panelName, out panel))
        {
            Debug.Log(panelName + "已打开");
            ClosePanel(panelName);
            return panel;
        }
        //检查路径是否配置
        string path = "";
        if (!pathDict.TryGetValue(panelName, out path))
        {
            Debug.Log(panelName+"界面名称错误或未配置");
            return null;
        }
        //使用缓存预制件
        GameObject panelPrefab = null;
        if (!prefabDict.TryGetValue(panelName, out panelPrefab))
        {
            //有路径，但是没有缓存的预制件
            string realPath = "" + path;
            panelPrefab= Resources.Load<GameObject>(realPath);
            prefabDict.Add(panelName, panelPrefab);
        }
        //打开界面
        GameObject panelObject = GameObject.Instantiate(panelPrefab,UIRoot, false);
        panel=panelObject.GetComponent<BasePanel>();
        panelDict.Add(panelName, panel);
        panel.OpenPanel(panelName);
        return panel;
    }

    public BasePanel ClosePanel(string panelName)
    {
        BasePanel panel = null;
        if (!panelDict.TryGetValue(panelName, out panel))
        {
            Debug.Log(panelName+ "界面未打开");
            return null;
        }
        panel.ClosePanel(panelName);
        return panel;

    }
public class UIConst
{
   public const string PackagePanel="PakagePanel";
   public const string Menu="Menu";
   public const string LotteryPanel = "LotteryPanel";
   

}



}
