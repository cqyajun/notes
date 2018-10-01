using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace YY {
    [LuaCallCSharp]
    [Hotfix]
    public class LuaManager : Base
    {
        internal static LuaEnv luaEnv = new LuaEnv();
        private LuaTable scriptEnv;

        public  bool isReady = false;

        private void Awake()
        {
            mLuaManager = this;

            LuaTable meta = luaEnv.NewTable();
            scriptEnv = luaEnv.NewTable();
            meta.Set("__index", luaEnv.Global);
             
            scriptEnv.SetMetaTable(meta);
            meta.Dispose();

            scriptEnv.Set("self", this);

            LuaFileUtils.Instance.AddSearchPath(Util.DataPath + AppConst.LuaTempDir + "?");

       
            luaEnv.AddLoader(LuaFileUtils.Instance.CustomLoader);

        }

        public object[] DoString(string luaString)
        {
            return luaEnv.DoString(luaString);
        }

        public void CallFunction(string funcName, params object[] args)
        {
            System.Action<object> action = scriptEnv.GetInPath<System.Action<object>>(funcName);
            if (action != null)
            {
                action(args);
            }
        }

        private void Start()
        {

            InitLuaBundle();
            luaEnv.DoString("require 'Main'");
              
            CallFunction("Main.Start2",100000,200);
            isReady = true;
           
            
        }

        public LuaTable GetLuaTable(string tableName)
        {
            return luaEnv.Global.Get<LuaTable>(tableName);
        }

        void InitLuaBundle()
        {
            if (AppConst.LuaBundleMode && !AppConst.DebugMode)
            {
                LuaFileUtils.Instance.loadLuaBundle(Util.DataPath);
            }
        }

        private void Update()
        {
            if (luaEnv != null)
            {
                luaEnv.Tick();
            }
        }


        public void Close()
        {
            if (luaEnv != null)
            {
                luaEnv.Dispose();
            }
            luaEnv = null;
        }
    }

}


