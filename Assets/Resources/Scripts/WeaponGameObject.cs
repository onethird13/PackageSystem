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
        lifeTime = 10f;
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Init()
    {
        item= GameManager.instance.GetRandomPackageLocalItem();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        Debug.Log(item.id);
        PackageLocalData.Instance.items.Add(item);
       PackageLocalData.Instance.SavePackage();
        Destroy(gameObject);
    }
}
