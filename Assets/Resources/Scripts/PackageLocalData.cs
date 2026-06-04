using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageLocalData
{
    private static PackageLocalData _instance;
    private PackageLocalData(){}
    public static PackageLocalData Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PackageLocalData();
            }

            return _instance;
        }
    }

    public List<PackageLocalItem> items=new List<PackageLocalItem>();
    

    public void SavePackage()
    {
        string inventoryJson = JsonUtility.ToJson(this);
        PlayerPrefs.SetString("PackageLocalData", inventoryJson);
        PlayerPrefs.Save();
    }

    public List<PackageLocalItem> LoadPackage()
    {
        if (items != null)
        {
            return items;
        }

        if (PlayerPrefs.HasKey("PackageLocalData"))
        {
            string inventoryJson = PlayerPrefs.GetString("PackageLocalData");
            
            PackageLocalData packageLocalData=JsonUtility.FromJson<PackageLocalData>(inventoryJson);
            items = packageLocalData.items;
            return items;
        }
        else
        {
            items = new List<PackageLocalItem>();
            return items;
        }
    }






}
[Serializable]
public class PackageLocalItem
{
    public string  uid;
    public int id;
    public int num;
    public int level;
    public bool isNew;
    public override string ToString()
    {
        return string.Format("[id:]{0},[num:]{1},[level:]{2}", id, num, level);
    }
}


   
