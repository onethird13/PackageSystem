using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance{get; private set;}
    public int damage=10;
    public int defence=10;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        

    }

    [SerializeField] private AssetReference WeaponReference;
    private void Start()
    {
        WeaponReference.InstantiateAsync().Completed += (handle) =>
        {
            print("done");
        };
    }

    private void Update()
    {
        
    }
}
