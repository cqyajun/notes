using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace YY
{
    public class Base : MonoBehaviour
    {
        public static LuaManager mLuaManager;

        public static LoadManager mLoadManager;


        public static LoadManager GetLoadManagerInstance()
        {
           return mLoadManager;
        }

        public static LuaManager GetLuaManagerInstance()
        {
            return mLuaManager;
        }
    }
}


