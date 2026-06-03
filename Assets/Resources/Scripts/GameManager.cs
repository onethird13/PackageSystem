using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance{get; private set;}
    private PackageTable packageTable;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        /*UIManager.Instance.OpenPanel(UIManager.UIConst.PackagePanel);*/
    }

    public PackageTable GetPackageTable()
    {
        if (packageTable == null)
        {
           packageTable= Resources.Load<PackageTable>("TableData/PackageTable");
        }
        return packageTable;
    }

    public List<PackageLocalItem> GetPackageLocalData()
    {
       return PackageLocalData.Instance.LoadPackage();
    }
    //得到静态数据 根据id
    public PackageTableItem GetPackageItemById(int id)
    {
        List<PackageTableItem> items = GetPackageTable().Datalist;
        foreach (PackageTableItem i in items)
        {
            if (i.id == id)
            {
                return i;
            }
        }
        return null;
    }
    //得到动态数据，根据uid
    public PackageLocalItem GetPackageLocalItemByUid(string uid)
    {
        List<PackageLocalItem> items = PackageLocalData.Instance.items;
        foreach (var i in items)
        {
            if (i.uid == uid)
            {
                return i;
            }
        }
        return null;
    }

    public List<PackageLocalItem> GetSortPackageLocalData()
    {
        List<PackageLocalItem> items = PackageLocalData.Instance.LoadPackage();
        items.Sort(new PackageItemComparer());
        return items;
    }

}


public class PackageItemComparer : IComparer<PackageLocalItem>
{
    public int Compare(PackageLocalItem x, PackageLocalItem y)
    {
        PackageTableItem a = GameManager.instance.GetPackageItemById(x.id);
        PackageTableItem b = GameManager.instance.GetPackageItemById(y.id);
        //先按星级排序
        if (a.star != b.star)
        {
            return b.star.CompareTo(a.star);
        }
        //星级一样，按id排序
        if (a.id != b.id)
        {
            return b.id.CompareTo(a.id);
        }
        //星级，id一样，按等级排序
        return y.level.CompareTo(x.level);
    }
}


















