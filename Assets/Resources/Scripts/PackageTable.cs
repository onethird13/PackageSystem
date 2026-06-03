using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PackageTable", menuName = "Scriptable Objects/Package Table")]

public class PackageTable:ScriptableObject
{
    ///<summary>
    /// 储存静态数据的列表
    /// </summary>
    
   public List<PackageTableItem> Datalist = new List<PackageTableItem>();
}

[Serializable]
public class PackageTableItem
{
    public int id;
    public int type;
    public int star;
    public string itemName;
    public string description;
    public string skillDescription;
    
    public string imagePath;
   
}
