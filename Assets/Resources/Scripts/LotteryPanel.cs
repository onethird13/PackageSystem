using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LotteryPanel : BasePanel
{
    [SerializeField] private Transform close;
    [SerializeField] private Transform lottery10Btn;
    [SerializeField] private Transform lottery1Btn;
    [SerializeField] private Transform center;
    [SerializeField] private Transform lotteryCellPrefab;


    private void Awake()
    {
        lottery1Btn.GetComponent<Button>().onClick.AddListener(OnLottery1BtnClick);
        lottery10Btn.GetComponent<Button>().onClick.AddListener(OnLottery10BtnClick);
        close.GetComponent<Button>().onClick.AddListener(OnCloseBtnOnClick);
    }

    private void Start()
    {
        for (int i = 0; i < center.childCount; i++)
        {
            center.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void OnLottery1BtnClick()
    {
            PackageLocalItem item = GameManager.instance.LotteryRandom1();
            center.GetChild(0).gameObject.SetActive(true);
            center.GetChild(0).GetComponent<LotteryCell>().PackageLocalItem = item;
            center.GetChild(0).GetComponent<LotteryCell>().RefreshUI();
            PackageLocalData.Instance.items.Add(item);
            PackageLocalData.Instance.SavePackage();
        
    }

    private void OnLottery10BtnClick()
    {
        for (int i = 0; i < center.childCount; i++)
        {
            center.GetChild(i).gameObject.SetActive(true);
            PackageLocalItem item=GameManager.instance.LotteryRandom1();
            center.GetChild(i).GetComponent<LotteryCell>().PackageLocalItem = item;
            center.GetChild(i).GetComponent<LotteryCell>().RefreshUI();
            PackageLocalData.Instance.items.Add(item);
            PackageLocalData.Instance.SavePackage();
        }
    }

    public void OnCloseBtnOnClick()
    {
        this.ClosePanel(UIManager.UIConst.LotteryPanel);
        UIManager.Instance.OpenPanel(UIManager.UIConst.Menu);
    }
}
