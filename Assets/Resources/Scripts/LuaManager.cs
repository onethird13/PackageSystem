using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
public class LuaManager : MonoBehaviour
{
    public static LuaManager Instance{get;private set;}
    private XLua.LuaEnv luaEnv;
   
    private CallShootResult callShootResult;
    [CSharpCallLua]
    public delegate ShootResult CallShootResult(float timer);
    [CSharpCallLua]
    public delegate float CallDamage();
    private void Start()
    {
        if (Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitLua();
        }
        else
        {
            Destroy(gameObject);
        }
       
    }
    

    private void Update()
    {
    }

    private void InitLua()
    {
        luaEnv=new XLua.LuaEnv();
        TextAsset luascript = Resources.Load<TextAsset>(
            "Scripts/Lua/CreatorShootLogic.lua"
        );
        LuaTable shootLogic = luaEnv.DoString(luascript.text)[0] as LuaTable;
        callShootResult = shootLogic.Get<CallShootResult>("GetShootResult");
    }

    public ShootResult GetCreatorShootResult(float timer)
    {
        
        return callShootResult(timer);
    }
}
