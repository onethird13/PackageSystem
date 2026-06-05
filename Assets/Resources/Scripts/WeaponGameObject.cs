using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGameObject : MonoBehaviour
{
    public PackageLocalItem item;
    public float lifeTime;

    private void Awake()
    {
       
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            GameObjectPool.instance.Release(this.gameObject);
        }
    }

    public void Init()
    {
        item= GameManager.instance.GetRandomPackageLocalItem();
        lifeTime = 10f;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        
        PackageLocalData.Instance.items.Add(item);
       PackageLocalData.Instance.SavePackage();
        GameObjectPool.instance.Release(this.gameObject);
    }
}
