using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private AssetReference WeaponReference;

    private void Start()
    {
        WeaponReference.InstantiateAsync().Completed += (handle) =>
        {
            print("done");
        };
    }
}
