using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PackageTable", menuName = "Scriptable Objects/Package Table")]
public class PackageTable:ScriptableObject
{
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
