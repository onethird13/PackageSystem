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
    public float moveSpeed = 5f;
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
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Transform cameraTransform = Camera.main.transform;
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 moveDirection = (cameraRight * horizontal + cameraForward * vertical).normalized;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
