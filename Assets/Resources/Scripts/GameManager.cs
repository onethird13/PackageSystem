using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


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
        GetPackageTable();
        /*UIManager.Instance.OpenPanel(UIManager.UIConst.Menu);*/
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            UIManager.Instance.OpenPanel(UIManager.UIConst.Menu);
        }
    }

    public void DeletePackageItems(List<string> uids)
    {
        foreach (string uid in uids)
        {
            foreach ( PackageLocalItem item in PackageLocalData.Instance.items)
            {
                if (uid == item.uid)
                {
                    PackageLocalData.Instance.items.Remove(item);
                    PackageLocalData.Instance.SavePackage();
                    break;
                }
            }
        }
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
        List<PackageLocalItem> items = PackageLocalData.Instance.LoadPackage();
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
    //根据类型找静态数据的表
    public List<PackageTableItem> GetPackageTableDataByType(int type)
    {
        List<PackageTableItem> items=new List<PackageTableItem>();
        foreach (var item in GetPackageTable().Datalist )
        {
            if (item.type == type)
            {
                items.Add(item);
            }
        }
        return items;
    }
    //单抽
    public PackageLocalItem LotteryRandom1()
    {
        //先拿到所有武器type
     List<PackageTableItem> items= GetPackageTableDataByType(GameTypeConst.WEAPON);
     //随机拿到一个里面的东西
     PackageTableItem item = null;
     int index=UnityEngine.Random.Range(0,items.Count);
     item = items[index];
     PackageLocalItem packageLocalItem=new PackageLocalItem()
     {
         id = item.id,
         uid=System.Guid.NewGuid().ToString(),
         num=1,
         level = 1,
         isNew = false
     };
     PackageLocalData.Instance.items.Add(packageLocalItem);
     PackageLocalData.Instance.SavePackage();
     return packageLocalItem;
    }
    //十连抽
    public List<PackageLocalItem> LotteryRandom10()
    {
        List<PackageLocalItem> items=new List<PackageLocalItem>();
        for (int i = 0; i < 10; i++)
        {
            items[i] = LotteryRandom1();
            PackageLocalData.Instance.items.Add(items[i]);
        }
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

public class GameTypeConst
{
    public const int WEAPON = 1;
    public const int FOOD = 2;
}


















