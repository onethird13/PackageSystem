using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
    public static GameObjectPool instance{get; private set;}
    [SerializeField] private int initCount;
    private Queue<GameObject> gameObjectQueue = new Queue<GameObject>();
    [SerializeField]private GameObject weaponPrefab;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Init();
    }

    public GameObject CreateWeaponGameObject()
    {
        GameObject weapon= Instantiate(weaponPrefab,new Vector3(0,1,0),Quaternion.identity);
        weapon.SetActive(false); 
        gameObjectQueue.Enqueue(weapon);
        return weapon;
    }
    
    private void Init()
    {
        for (int i = 0; i < initCount; i++)
        {
            CreateWeaponGameObject();
        }
    }

    public GameObject GetGameObject(Vector3 position, Quaternion rotation)
    {
        if (gameObjectQueue.Count <= 0)
        {
            CreateWeaponGameObject();
        }
        GameObject weapon = gameObjectQueue.Dequeue();
        weapon.SetActive(true);
        weapon.transform.SetPositionAndRotation(position, rotation);
        return weapon;
    }

    public void Release(GameObject weapon)
    {
        weapon.SetActive(false);
        Rigidbody rb=weapon.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        gameObjectQueue.Enqueue(weapon);
        weapon.transform.parent = this.transform;
    }
    
    
    
}
