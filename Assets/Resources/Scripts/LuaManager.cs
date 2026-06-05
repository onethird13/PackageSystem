using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
public class LuaManager : MonoBehaviour
{
    private XLua.LuaEnv luaEnv;
    private CallDamage callDamage;
    [CSharpCallLua]
    public delegate float CallDamage();
    private void Start()
    {
        luaEnv=new XLua.LuaEnv();
        luaEnv.DoString(@"
            a=10;
            print(a+5);
        ");
        //lua获取unity信息
        luaEnv.DoString(@"
    function CallDamage()    
    Status=CS.LuaGame.StatusManager()
        damage=Status:GetDamage()
        return (damage*10)
        end 
        ");
        //unity call lua 获取计算后的数值
         callDamage  =luaEnv.Global.Get<CallDamage>("CallDamage");
       
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            var damage = callDamage();
            Debug.Log($"damage:{damage}");
        }
    }
}
