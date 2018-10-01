using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace YY
{
    [LuaCallCSharp]
    [Hotfix]
    public class LoadManager : Base
    {

        LuaTable resTab;
        AssetBundleManifest manifest;
        public bool isReady = false;
        bool isUnload;
        Dictionary<string, List<string>> groups = new Dictionary<string, List<string>>();
        Dictionary<string, AssetBundle> abs = new Dictionary<string, AssetBundle>();
        Dictionary<string, int> abNum = new Dictionary<string, int>();

        void Awake()
        {
            mLoadManager = this;
        }

        IEnumerator Start()
        {
            while (!mLuaManager.isReady)
            {
                yield return 1;
            }
            StartCoroutine(Init());
        }
        public IEnumerator Init()
        {
            resTab = mLuaManager.GetLuaTable("ResourceMsg");
            LuaTable _tab = resTab.GetInPath<LuaTable>("DemoUI");
            if (_tab != null)
            {

                Debug.Log(_tab["id"]);
            }
            yield return 1;
            if (!AppConst.DebugMode)
            {
                string path = Util.DataPath + "/StreamingAssets";
                AssetBundle manifestBundle = AssetBundle.LoadFromFile(path);
                manifest = (AssetBundleManifest)manifestBundle.LoadAsset("AssetBundleManifest");
                manifestBundle.Unload(false);
            }
           
            isReady = true;

            mLuaManager.CallFunction("Main.CreateUI");
        }

        public UnityEngine.Object Load(string resName, string group, Type type = null)
        {
            try
            {
                if (resTab == null)
                {
                    Debug.LogError("没有资源配置表");
                    return null;
                }
                LuaTable tab = resTab.GetInPath<LuaTable>(resName);
                if (tab == null)
                {
                    Debug.LogError("资源配置表没有该资源" + resName);
                    return null;
                }
                if (!groups.ContainsKey(group))
                {
                    groups[group] = new List<string>();
                }
                if (!groups[group].Contains(resName))
                {
                    groups[group].Add(resName);
                }
                string id = tab.GetInPath<string>("id");
                string path = tab.GetInPath<string>("path");
                bool isLocal = tab.GetInPath<bool>("isLocal");
                if (isLocal)
                {
                    if (type == null)
                    {
                        return Resources.Load(path);
                    }
                    else
                    {
                        return Resources.Load(path, type);
                    }
                }
                else
                {
                    if (!abs.ContainsKey(path))
                    {
                        //加载AB资源
                        string[] depends = manifest.GetAllDependencies(path);
                        for (int i = 0; i < depends.Length; i++)
                        {
                            if (!abs.ContainsKey(depends[i]))
                            {
                                AssetBundle subAB = AssetBundle.LoadFromFile(Util.DataPath  + depends[i]);
                                if (subAB == null)
                                {
                                    Debug.LogError("没有加载到ab包：" + Util.DataPath  + depends[i]);
                                    return null;
                                }
                                abs[depends[i]] = subAB;
                                abNum[depends[i]] = 0;
                            }
                            abNum[depends[i]]++;
                        }

                        AssetBundle ab = AssetBundle.LoadFromFile(Util.DataPath  + path);
                        if (ab == null)
                        {
                            Debug.LogError("没有加载到ab包：" + Util.DataPath + path);
                            return null;
                        }
                        abs[path] = ab;
                        abNum[path] = 1;
                    }
                    if (type == null)
                    {
                        return abs[path].LoadAsset(id);
                    }
                    else
                    {
                        return abs[path].LoadAsset(id, type);
                    }
                }
            }
            catch (Exception)
            {
                Debug.LogError("下载失败" + name);
                return null;
            }
        }


        /// <summary>
        ///异步加载资源
        /// </summary>
        /// <param name="resName"></param>
        /// <param name="group"></param>
        /// <param name="type"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public IEnumerator LoadAsync(string resName, string group, Type type = null, Action<UnityEngine.Object> callback = null)
        {
            if (resTab == null)
            {
                Debug.LogError("没有资源配置表");
                yield break;
            }
            LuaTable tab = resTab.GetInPath<LuaTable>(resName);
            if (tab == null)
            {
                Debug.LogError("资源配置表没有该资源" + resName);
                yield break;
            }

            if (!groups.ContainsKey(group))
            {
                groups[group] = new List<string>();
            }
            if (!groups[group].Contains(resName))
            {
                groups[group].Add(resName);
            }

            string id = tab.GetInPath<string>("id");
            string path = tab.GetInPath<string>("path");
            bool isLocal = tab.GetInPath<bool>("isLocal");
            if (isLocal)
            {
                if (type == null)
                {
                    //return Resources.Load(path);
                    ResourceRequest rr = Resources.LoadAsync(path);
                    yield return rr;
                    if (rr == null)
                    {
                        Debug.LogError("加载失败" + path);
                        yield break;
                    }
                    if (callback != null)
                    {
                        callback(rr.asset);
                    }
                }
                else
                {
                    //return Resources.Load(path, type);
                    ResourceRequest rr = Resources.LoadAsync(path, type);
                    yield return rr;
                    if (rr == null)
                    {
                        Debug.LogError("加载失败" + path);
                        yield break;
                    }
                    if (callback != null)
                    {
                        callback(rr.asset);
                    }
                }
            }
            else
            {
                if (!abs.ContainsKey(path))
                {
                    //加载AB资源
                    string[] depends = manifest.GetAllDependencies(path);
                    for (int i = 0; i < depends.Length; i++)
                    {
                        if (!abs.ContainsKey(depends[i]))
                        {
                            AssetBundle subAB = AssetBundle.LoadFromFile(Util.DataPath + depends[i]);
                            if (subAB == null)
                            {
                                Debug.LogError("没有加载到ab包：" + Util.DataPath  + depends[i]);
                                yield break;
                            }
                            abs[depends[i]] = subAB;
                            abNum[depends[i]] = 0;
                        }
                        abNum[depends[i]]++;
                    }

                    AssetBundle ab = AssetBundle.LoadFromFile(Util.DataPath + path);
                    if (ab == null)
                    {
                        Debug.LogError("没有加载到ab包：" + Util.DataPath + path);
                        yield break;
                    }
                    abs[path] = ab;
                    abNum[path] = 1;
                }
                if (type == null)
                {
                    //return abs[path].LoadAsset(id);
                    AssetBundleRequest abr = abs[path].LoadAssetAsync(id);
                    yield return abr;
                    if (abr == null)
                    {
                        Debug.LogError("加载失败" + path);
                        yield break;
                    }
                    if (callback != null)
                    {
                        callback(abr.asset);
                    }
                }
                else
                {
                    //return abs[path].LoadAsset(id, type);
                    AssetBundleRequest abr = abs[path].LoadAssetAsync(id, type);
                    yield return abr;
                    if (abr == null)
                    {
                        Debug.LogError("加载失败" + path);
                        yield break;
                    }
                    if (callback != null)
                    {
                        callback(abr.asset);
                    }
                }
            }
        }


        /// <summary>
        /// 卸载资源
        /// </summary>
        /// <param name="group"></param>
        public void UnLoadGroup(string group)
        {
            if (groups.ContainsKey(group))
            {
                for (int i = 0; i < groups[group].Count; i++)
                {
                    string resName = groups[group][i];
                    LuaTable tab = resTab.GetInPath<LuaTable>(resName);
                    string id = tab.GetInPath<string>("id");
                    string path = tab.GetInPath<string>("path");
                    bool isLocal = tab.GetInPath<bool>("isLocal");
                    if (isLocal)
                    {
                        //UnityEngine.Object obj = Resources.Load(path);
                        //if (obj.GetType() == typeof(GameObject))
                        //{
                        //    isUnload = true;
                        //}
                        //else
                        //{
                        //    Resources.UnloadAsset(obj);
                        //}
                    }
                    else
                    {
                        //删除ab资源
                        if (abs.ContainsKey(path))
                        {
                            string[] depends = manifest.GetAllDependencies(path);
                            for (int j = 0; j < depends.Length; j++)
                            {
                                if (abs.ContainsKey(depends[j]))
                                {
                                    abNum[depends[j]]--;
                                    if (abNum[depends[j]] <= 0)
                                    {
                                        abs[depends[j]].Unload(true);
                                        abs.Remove(depends[j]);
                                        abNum.Remove(depends[j]);
                                    }
                                }
                            }
                            abNum[path]--;
                            if (abNum[path] <= 0)
                            {
                                abs[path].Unload(true);
                                abs.Remove(path);
                                abNum.Remove(path);
                            }
                        }
                    }
                }
                groups.Remove(group);
            }
        }
    }
}
