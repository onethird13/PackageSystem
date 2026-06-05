using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoBehaviour
{
    [SerializeField] private GameObject weaponPrefab;
   
    [SerializeField] private float timer;
    
    //只管lua让不让发射
    private void Update()
    {
        timer += Time.deltaTime;
        ShootResult result = LuaManager.Instance.GetCreatorShootResult(timer);
        if (!result.shouldShoot)
        {
            return;
        }

        timer = 0;
        GameObject weaponGameObject = GameObjectPool.instance.GetGameObject
            (new Vector3(transform.position.x,transform.position.y+1,transform.position.z),Quaternion.identity);
        weaponGameObject.GetComponent<WeaponGameObject>().Init();
        Rigidbody rb=weaponGameObject.GetComponent<Rigidbody>();
        rb.velocity=result.direction*result.speed;
    }
}
