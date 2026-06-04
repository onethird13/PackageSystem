using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LotteryCell : MonoBehaviour
{
    [SerializeField] private Transform stars;
    [SerializeField] private Transform icon;
    public PackageLocalItem PackageLocalItem;
    public PackageTableItem PackageTableItem;

    private void Start()
    {
        
    }

    public void RefreshUI()
    {
        PackageTableItem=GameManager.instance.GetPackageItemById(PackageLocalItem.id);
        string path = PackageTableItem.imagePath.Substring(17);
        Texture2D t = Resources.Load<Texture2D>(path.Substring(0,path.Length-4));
        icon.GetComponent<Image>().sprite=Sprite.Create(t,new Rect(0,0,t.width,t.height),new Vector2(0.5f,0.5f));
        RefreshStars();
    }
    
    
    public void RefreshStars()
    {
        for (int i = 0; i < stars.childCount; i++)
        {
            Transform star = stars.transform.GetChild(i); 
            if (i<PackageTableItem.star)
            {
                star.gameObject.SetActive(true);
            }
            else
            {
                star.gameObject.SetActive(false);
            }
        }
    }
}
