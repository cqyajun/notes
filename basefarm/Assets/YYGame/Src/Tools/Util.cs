using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using XLua;

namespace YY
{
    [LuaCallCSharp]
    public class Util 
    {


        /// <summary>
        /// 计算字符串的MD5值
        /// </summary>
        public static string md5(string source)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(source);
            byte[] md5Data = md5.ComputeHash(data, 0, data.Length);
            md5.Clear();

            string destString = "";
            for (int i = 0; i < md5Data.Length; i++)
            {
                destString += System.Convert.ToString(md5Data[i], 16).PadLeft(2, '0');
            }
            destString = destString.PadLeft(32, '0');
            return destString;
        }

        /// <summary>
		/// 清除所有子节点
		/// </summary>
		public static void ClearChild(Transform go)
        {
            if (go == null) return;
            for (int i = go.childCount - 1; i >= 0; i--)
            {
                GameObject.Destroy(go.GetChild(i).gameObject);
            }
        }


        /// <summary>
        /// 网络可用
        /// </summary>
        public static bool NetAvailable
        {
            get
            {
                return Application.internetReachability != NetworkReachability.NotReachable;
            }
        }
        public static string DataPath
        {
            get
            {
                string game = AppConst.AppName.ToLower();
                if (AppConst.DebugMode)
                {
                    return Application.dataPath +"/"+ AppConst.AppName + "/";
                }
                if (Application.isMobilePlatform)
                {
                    return Application.persistentDataPath + "/" + game + "/";
                }
                if (Application.platform == RuntimePlatform.OSXEditor)
                {
                    int i = Application.dataPath.LastIndexOf('/');
                    return Application.dataPath.Substring(0, i + 1) + game + "/";
                }
               
                return "c:/" + game + "/";
            }
        }
        /// <summary>
        /// 应用程序内容路径
        /// </summary>
        public static string AppContentPath()
        {
            string path = string.Empty;
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    path = "jar:file://" + Application.dataPath + "!/assets/";
                    break;
                case RuntimePlatform.IPhonePlayer:
                    path = "file://" + Application.dataPath + "/Raw/";
                    break;
                default:
                    path = Application.dataPath + "/StreamingAssets" + "/";
                    break;
            }
            return path;
        }

        public static void LogError(string v)
        {
            Debug.LogError(v);
        }


        //加密 解密
        public static byte[] Decryption(byte[] data,string key)
        {
            byte[] tmp = new byte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                tmp[i] = data[i];
            }
            packXor(tmp, tmp.Length, key);
            return tmp;

        }
        public static void packXor(byte[] _data, int _len, string _pstr)
        {
            int length = _len;
            int strCount = 0;

            for (int i = 0; i < length; ++i)
            {
                if (strCount >= _pstr.Length)
                    strCount = 0;
                _data[i] ^= (byte)_pstr[strCount++];
            }
        }
    }

}
