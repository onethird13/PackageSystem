using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PackageDetail : MonoBehaviour
{
    [SerializeField]
    private Transform UITitle;
    [SerializeField]
    private Transform UIStars;
    [SerializeField]
    private Transform UIDescription;
    [SerializeField] 
    private Transform UIIcon;
    [SerializeField] 
    private Transform UIDetailedDescription;
    [SerializeField] 
    private Transform UILevelText;
    
    private PackageLocalItem packageLocalItem;
    private PackageTableItem packageTableItem;
    private PackagePanel uiParent;

    private void Awake()
    {
        /*Test();*/
    }

    private void Test()
    {
        RefreshUI(GameManager.instance.GetPackageLocalData()[1],null);
    }
    public void RefreshUI(PackageLocalItem item,PackagePanel uiParent)
    {
        //初始化数据
        packageLocalItem  = item;
        this.uiParent = uiParent;
        packageTableItem=GameManager.instance.GetPackageItemById(packageLocalItem.id);
        //刷新ui内容
        UITitle.GetComponent<TextMeshProUGUI>().text = packageTableItem.itemName.ToString();
        RefreshStars();
        UIDescription.GetComponent<TextMeshProUGUI>().text = packageTableItem.description.ToString();
        RefreshIcon();
        UIDescription.GetComponent<TextMeshProUGUI>().text = packageTableItem.skillDescription.ToString();
        UILevelText.GetComponent<TextMeshProUGUI>().text="Lv."+packageLocalItem.level.ToString();
        
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

    public void RefreshIcon()
    {
        string path = packageTableItem.imagePath.Substring(17);
        Debug.Log(packageTableItem.imagePath.Substring(17));
        Texture2D t = Resources.Load<Texture2D>(path.Substring(0,path.Length-4));
        Sprite sprite = Sprite.Create(t,new Rect(0,0,t.height,t.width),new Vector2(0.5f,0.5f));
        UIIcon.GetComponent<Image>().sprite=sprite;
    }
}
