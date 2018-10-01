using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YY
{
    public class AppConst 
    {
        public const bool DebugMode = false;                       //调试模式-用于内部测试


        public const bool LuaBundleMode = true;                    //Lua代码AssetBundle模式

        public const string AppName = "YYGame";
        public const string LuaTempDir="Lua/";

        public const string AssetDir = "StreamingAssets";           //素材目录 


        public static string FrameworkRoot
        {
            get
            {
                return Application.dataPath + "/" + AppName;
            }
        }
        public const string ExtName = ".unity3d";                   //素材扩展名
    }
}


