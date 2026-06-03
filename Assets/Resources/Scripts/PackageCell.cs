using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PackageCell:MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    private Transform UISelect;
    private Transform UIDeleteSelect;
    private Transform UIIcon;
    private Transform UINew;
    private Transform UIHead;
    private Transform UILevelText;
    private Transform UIStars;
    
    //动态数据
    private PackageLocalItem packageLocalItem;
    //静态数据
    private PackageTableItem packageTableItem;
    //父物体
    private PackagePanel uiParent;

    private void Awake()
    {
        InitUI();
    }

    private void InitUI()
    {
        InitUIName();
    }

    private void InitUIName()
    {
        UISelect = transform.Find("Select");
        UIDeleteSelect = transform.Find("DeleteSelect");
        UIIcon = transform.Find("Top/Icon");
        UINew = transform.Find("Top/New");
        UIHead = transform.Find("Top/Head");
        UILevelText = transform.Find("Buttom/LevelText");
        UIStars = transform.Find("Buttom/Stars");
    }
    
    
    public void RefreshCell(PackageLocalItem localItem,PackagePanel uiParent)
    {
        //把数据传入
        this.packageLocalItem = localItem;
        this.uiParent = uiParent;
        this.packageTableItem=GameManager.instance.GetPackageItemById(localItem.id);
        //写入等级信息
        UILevelText.GetComponent<TextMeshProUGUI>().text="Lv."+packageLocalItem.level.ToString();
        //是否新获得，是的话显示new并刷新isNew状态，不是则否
        if(packageLocalItem.isNew)
        {
            UINew.gameObject.SetActive(true);
            packageLocalItem.isNew = false;
            
        }
        //根据路径加载image
        string path = packageTableItem.imagePath.Substring(17);
        Debug.Log(packageTableItem.imagePath.Substring(17));
        Texture2D t = Resources.Load<Texture2D>(path.Substring(0,path.Length-4));
        Sprite sprite = Sprite.Create(t,new Rect(0,0,t.height,t.width),new Vector2(0.5f,0.5f));
        UIIcon.GetComponent<Image>().sprite=sprite;
        //加载星星
        RefreshStars();
        PackageLocalData.Instance.SavePackage();
    }
    public void RefreshStars()
    {
        for (int i = 0; i < UIStars.childCount; i++)
        {
            Transform star = UIStars.transform.GetChild(i); 
            if (i<packageTableItem.star)
            {
                star.gameObject.SetActive(true);
            }
            else
            {
                star.gameObject.SetActive(false);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }
}
