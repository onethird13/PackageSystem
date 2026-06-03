using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class GMCommand
{
    [MenuItem("GMCommand/读取表格")]
    public static void ReadTable()
    {
        PackageTable packageTable=Resources.Load<PackageTable>("TableData/PackageTable");
        foreach (var packageTableItem in packageTable.Datalist)
        {
            Debug.Log("id:"+packageTableItem.id+" name"+packageTableItem.itemName);
        }
    }

    [MenuItem("GMCommand/存本地背包数据")]
    public static void CreatePackageLocalData()
    {
        //创建数据
        PackageLocalData.Instance.items = new List<PackageLocalItem>();
        for (int i=1;i<4;i++)
        {
            PackageLocalItem packageLocalItem = new PackageLocalItem()
            {
                id =i,
                level = i,
                num = i,
                uid = Guid.NewGuid().ToString(),
                isNew =  false,
            };
            PackageLocalData.Instance.items.Add(packageLocalItem);
        }
        PackageLocalData.Instance.SavePackage();
        
    }

    [MenuItem("GMCommand/读取本地背包数据")]
    public static void ReadPackageLocalData()
    {
        //读取数据
        foreach (PackageLocalItem item in PackageLocalData.Instance.LoadPackage())
        {
            Debug.Log(item.ToString());
        }
    }

    [MenuItem("GMCommand/打开背包界面")]
    public static void OpenPackagePanel()
    {
        UIManager.Instance.OpenPanel(UIManager.UIConst.PackagePanel);
    }
    
}
