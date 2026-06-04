using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackagePanel:BasePanel
{
    
    public enum PackageState 
    {
        Normal,
        Delete
    }
    public PackageState state = PackageState.Normal;
    public List<string> deleteChoosedUids = new List<string>();
    
    
    
    
    private Transform UIMenu;
    private Transform UITopMenusWeapon;
    private Transform UITopMenusFood;
    private Transform UICurPageIcon;
    private Transform UICurPageText;
    private Transform UICapacityText;
    private Transform UICloseBtn;
    /*------------centUIer------------*/
    private Transform UIScrollView;
    private Transform UIScrollContent;
    private Transform UIDetailPanel;
    private Transform UILeftBtn;
    private Transform UIRightBtn;
    /*---------bottom-UI-------*/
    private Transform UIDeleteBtn;
    private Transform UIDetailBtn;//详情
    private Transform UIDeletePanel;
    private Transform UIDeleteBackBtn;
    private Transform UIInfoIcon;
    private Transform UIInfoText;
    private Transform UIDeleteConfirmBtn;

    [SerializeField] private GameObject packageUIItemPrefab;
    private string _deleteChoosedUid;

    public string DeleteChoosedUid
    {
        get
        {
            return _deleteChoosedUid;
        }
        set
        {
            _deleteChoosedUid = value;
            RefreshDetail();    
        }
    }

    private void Awake()
    {
        InitUI();
    }

    private void Start()
    {
        RefreshUI();
    }

    private void RefreshUI()
    {
        RefreshScroll();
        
    }

    private void RefreshDetail()
    {
        //拿到数据
        PackageLocalItem packageLocalItem=GameManager.instance.GetPackageLocalItemByUid(DeleteChoosedUid);
        PackageTableItem packageTableItem=GameManager.instance.GetPackageItemById(packageLocalItem.id);
        //刷新内容
        UIDetailPanel.GetComponent<PackageDetail>().RefreshUI(packageLocalItem,this);
    }

    private void RefreshScroll()
    {
        Transform content=UIScrollView.GetComponent<ScrollRect>().content;
        //清理滚动容器中的所有物体
        foreach (Transform child  in content)
        {
            Destroy(child.gameObject);
        }
        //得到排序好的本地数据，并把他们初始化到滚动容器里
        foreach (PackageLocalItem item in GameManager.instance.GetSortPackageLocalData())
        {
            GameObject uiItem = GameObject.Instantiate(packageUIItemPrefab, content, false);
            PackageCell packageCell = uiItem.GetComponent<PackageCell>();
            packageCell.RefreshCell(item,this);
        }
    }
    //添加删除物品
    public void AddDeleteChooseUid(string uid)
    {
        if (deleteChoosedUids.Contains(uid))
        {
            deleteChoosedUids.Remove(uid);
        }
        else
        {
            deleteChoosedUids.Add(uid);
        }
        
    }
    //刷新滚动容器中的物品的删除选中状态
    public void RefreshDeletePanel()
    {
        foreach (Transform child in UIScrollContent )
        {
            PackageCell packageCell = child.GetComponent<PackageCell>();
            packageCell.RefreshDeleteState();
        }
    }
    
    

    private void InitUI()
    {
        InitUIName();
        InitClick();
    }

    

    private void WeaponBtnOnClick()
    {
    }

    private void FoodBtnOnClick()
    {
    }

    private void CloseBtnOnClick()
    {
        UIManager.Instance.ClosePanel(UIManager.UIConst.PackagePanel);
        UIManager.Instance.OpenPanel(UIManager.UIConst.Menu);
    }

    private void LeftBtnOnClick()
    {
    }

    private void RightBtnOnClick()
    {
    }

    private void DeleteBtnOnClick()
    {
        UIDeletePanel.gameObject.SetActive(true);
        state = PackageState.Delete;
        

    }

    private void DetailBtnOnClick()
    {
    }

    private void DeleteBackBtnOnClick()
    {
        UIDeletePanel.gameObject.SetActive(false);
        state = PackageState.Normal;
        deleteChoosedUids.Clear();  
        RefreshDeletePanel();
    }

    private void DeleteConfirmBtnOnClick()
    {
        UIDeletePanel.gameObject.SetActive(false);
        GameManager.instance.DeletePackageItems(deleteChoosedUids);
        //删除完刷新
        RefreshUI();
    }
    
    private void InitClick()
    {
        UITopMenusWeapon.GetComponent<Button>().onClick.AddListener(WeaponBtnOnClick);
        UITopMenusFood.GetComponent<Button>().onClick.AddListener(FoodBtnOnClick);
        UICloseBtn.GetComponent<Button>().onClick.AddListener(CloseBtnOnClick);
        UILeftBtn.GetComponent<Button>().onClick.AddListener(LeftBtnOnClick);
        UIRightBtn.GetComponent<Button>().onClick.AddListener(RightBtnOnClick);
        UIDeleteBtn.GetComponent<Button>().onClick.AddListener(DeleteBtnOnClick);
        UIDetailBtn.GetComponent<Button>().onClick.AddListener(DetailBtnOnClick);
        UIDeleteBackBtn.GetComponent<Button>().onClick.AddListener(DeleteBackBtnOnClick);
        UIDeleteConfirmBtn.GetComponent<Button>().onClick.AddListener(DeleteConfirmBtnOnClick);
        
    }

    private void InitUIName()
    {
        UIMenu = transform.Find("TopCenter/Menus");
        UITopMenusWeapon = transform.Find("TopCenter/Menus/Weapon");
        UITopMenusFood = transform.Find("TopCenter/Menus/Food");
        UICurPageIcon = transform.Find("TopCenter/CurPageDescription/CurrentIcon");
        UICurPageText = transform.Find("TopCenter/CurPageDescription/CurrentText");
        UICapacityText = transform.Find("TopCenter/CapacityText/Text");
        UICloseBtn = transform.Find("TopCenter/CloseBtn");
        
        UIScrollView = transform.Find("Center /Scroll View");
        UIDetailPanel = transform.Find("Center /DetailPanel");
        UILeftBtn = transform.Find("Center /LeftBtn");
        UIRightBtn = transform.Find("Center /RightBtn");

        UIDeleteBtn = transform.Find("Bottom/BottomMenus/DeleteButton");
        UIDetailBtn = transform.Find("Bottom/BottomMenus/DetailButton");
        UIDeleteConfirmBtn = transform.Find("Bottom/DeletePanel/ConfirmButton");
        UIDeleteBackBtn = transform.Find("Bottom/DeletePanel/Back");
        UIInfoIcon = transform.Find("Bottom/DeletePanel/InfoIcon");
        UIInfoText = transform.Find("Bottom/DeletePanel/InfoText");
        UIDeletePanel = transform.Find("Bottom/DeletePanel");
        UIScrollContent = transform.Find("Center/ScrollView/Viewport/Content");
        
        UIDeletePanel.gameObject.SetActive(false);

    }
}
