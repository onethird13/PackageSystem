using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu:BasePanel
{
    [SerializeField] private Button lotteryButton;
    [SerializeField] private Button openPackageButton;
    
    public static Menu instance{get; private set;}

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        lotteryButton.onClick.AddListener(lotteryButtonOnClick);
        openPackageButton.onClick.AddListener(openPackageButtonOnClick);
    }

    private void lotteryButtonOnClick()
    {
        UIManager.Instance.OpenPanel(UIManager.UIConst.LotteryPanel);
        UIManager.Instance.ClosePanel(UIManager.UIConst.Menu);
    }

    private void openPackageButtonOnClick()
    {
        UIManager.Instance.ClosePanel(UIManager.UIConst.Menu);
        UIManager.Instance.OpenPanel(UIManager.UIConst.PackagePanel);
    }


}
