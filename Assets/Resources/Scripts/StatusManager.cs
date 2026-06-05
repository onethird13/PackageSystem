using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace LuaGame
{


    [LuaCallCSharp]
    public class StatusManager 
    {
        public int GetDamage()
        {
            
            return PlayerManager.instance.damage;
        }

        public int GetDefense()
        {
            return PlayerManager.instance.defence;
        }
    }
}