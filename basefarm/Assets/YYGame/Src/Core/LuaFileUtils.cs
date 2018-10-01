using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
namespace YY {
    public class LuaFileUtils
    {
        protected static LuaFileUtils instance = null;

        public  const string Lua_Key_Ras_Pub = "BgIAAACkAABSU0ExAAQAAAEAAQBVCeo5bMUnZm6dXuMKkeEfE9wAvnAc0Zuq4l7HmQmTwi99IFkXNL2muKIfi2SkgtaRYYO0z0+ZD+f1vwX5t8pQMuKmJoqNKWL1lys67CZUJFO5bHajxbS++1zXGUgPAojAcnVNI567nRiK87KWjziAvQmq3/dfja2X6RrgEp0hyQ==";

        public static LuaFileUtils Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LuaFileUtils();
                }

                return instance;
            }
            protected set
            {
                instance = value;
            }
        }

        protected List<string> searchPaths = new List<string>();
        protected Dictionary<string, AssetBundle> zipMap = new Dictionary<string, AssetBundle>();


        public LuaFileUtils()
        {
            instance = this;
        }


        public byte[] CustomLoader(ref string filepath)
        {
            Debug.Log("filepath = >" + filepath);
          
            return ReadFile(filepath);
        }

        public void AddSearchBundle(string name, AssetBundle bundle)
        {
            zipMap[name] = bundle;
        }
        public virtual byte[] ReadFile(string fileName)
        {
            //!AppConst.LuaBundleMode ||
            if ( AppConst.DebugMode)
            {
                string path = FindFile(fileName);
                byte[] str = null;

                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                {
#if !UNITY_WEBPLAYER
                    str = File.ReadAllBytes(path);
#else
				throw new LuaException("can't run in web platform, please switch to other platform");
#endif
                }
                return str;
            }
            else
            {
                return ReadZipFile(fileName);
            }
        }
        public string FindFile(string fileName)
        {
            if (fileName == string.Empty)
            {
                return string.Empty;
            }

            if (!fileName.EndsWith(".lua"))
            {
                fileName += ".lua";
            }

            string fullPath = null;

            for (int i = 0; i < searchPaths.Count; i++)
            {
                fullPath = searchPaths[i].Replace("?", fileName);

                if (File.Exists(fullPath))
                {
                    return fullPath;
                }
            }

            return null;
        }

        byte[] ReadZipFile(string fileName)
        {
            AssetBundle zipFile = null;
            byte[] buffer = null;
            string zipName = null;
            StringBuilder sb = new StringBuilder();
            sb.Append("lua");
            int pos = fileName.LastIndexOf('/');

            if (pos > 0)
            {
                sb.Append("_");
                sb.Append(fileName.Substring(0, pos).ToLower());        //shit, unity5 assetbund'name must lower
                sb.Replace('/', '_');
                fileName = fileName.Substring(pos + 1);
            }

            if (!fileName.EndsWith(".lua"))
            {
                fileName += ".lua";
            }
            fileName += ".bytes";
            zipName = sb.ToString();
            zipMap.TryGetValue(zipName, out zipFile);

            if (zipFile != null)
            {
                TextAsset luaCode = zipFile.LoadAsset(fileName, typeof(TextAsset)) as TextAsset;

                if (luaCode != null)
                {
                    buffer = Util.Decryption(luaCode.bytes, Lua_Key_Ras_Pub);

                    Resources.UnloadAsset(luaCode);
                }
            }

            return buffer;
        }


        public bool AddSearchPath(string path, bool front = false)
        {
            int index = searchPaths.IndexOf(path);

            if (index >= 0)
            {
                return false;
            }

            if (front)
            {
                searchPaths.Insert(0, path);
            }
            else
            {
                searchPaths.Add(path);
            }

            return true;
        }


        public void loadLuaBundle(string path)
        {
            DirectoryInfo rootDirInfo = new DirectoryInfo(path);
            DirectoryInfo[] DirSub = rootDirInfo.GetDirectories();

            FileInfo[] thefileInfo = rootDirInfo.GetFiles("*.unity3d", SearchOption.AllDirectories);
            foreach (FileInfo NextFile in thefileInfo) //遍历文件
            {
                string allPath = NextFile.Name;
                if (NextFile.Name.StartsWith("lua"))
                {
                    AddBundle(NextFile.Name, NextFile.DirectoryName);
                }
            }
   
        }

         void AddBundle(string bundleName, string directoryName)
        {
            string url = directoryName + "/" + bundleName.ToLower();
            if (File.Exists(url))
            {
                bundleName = bundleName.Replace("lua/", "").Replace(".unity3d", "").ToLower();
                OnUnloadBundleByName(bundleName);
                try
                {
                    var bytes = File.ReadAllBytes(url);
                    //AssetBundle bundle = AssetBundle.LoadFromMemory(bytes);
                    AssetBundle bundle = AssetBundle.LoadFromFile(url);

                    if (bundle != null)
                    {
                        zipMap[bundleName] = bundle;
                    }
                }
                catch (IOException e)
                {
                    Util.LogError("AddBundle error " + url);
                }
            }
           
        }

        void OnUnloadBundleByName(string name)
        {
            AssetBundle zipFile = null;
            if (zipMap.TryGetValue(name, out zipFile))
            {
                zipMap.Remove(name);
                zipFile.Unload(true);
            }
        }

    }
}


