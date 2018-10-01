using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using YY;

public class PackTool
{

    [MenuItem("PackTool/CreateAssetBundes", false, 101)]
    static void CreateAssetBundes()
    {
        string targetpath = Application.dataPath + "/StreamingAssets/";
        if (!System.IO.Directory.Exists(targetpath))
        {
            System.IO.Directory.CreateDirectory(targetpath);
        }


        BuildPipeline.BuildAssetBundles(targetpath, BuildAssetBundleOptions.None |
          BuildAssetBundleOptions.ChunkBasedCompression |
          BuildAssetBundleOptions.DeterministicAssetBundle |
          BuildAssetBundleOptions.StrictMode, BuildTarget.Android);

        AssetDatabase.Refresh();
    }

    [MenuItem("PackTool/PackLua", false, 201)]
    static void CreateLuaAssetBundes()
    {
        BuildTarget target;
#if UNITY_ANDROID
        target = BuildTarget.Android;
#elif UNITY_IPHONE
         target = BuildTarget.iOS;
#endif
        List<AssetBundleBuild> maps =  HandleLuaBundle();
        string resPath = "Assets/StreamingAssets/";
        BuildAssetBundleOptions options = BuildAssetBundleOptions.DeterministicAssetBundle |
                                   BuildAssetBundleOptions.ChunkBasedCompression |
                                   BuildAssetBundleOptions.DisableWriteTypeTree;
        BuildPipeline.BuildAssetBundles(resPath, maps.ToArray(), options, target);
        AssetDatabase.Refresh();
    }


    /// <summary>
    /// 处理Lua代码包  跳过games
    /// </summary>
    static List<AssetBundleBuild> HandleLuaBundle()
    {
         List<AssetBundleBuild> maps = new List<AssetBundleBuild>();
        string streamDir = Application.dataPath + "/" + AppConst.LuaTempDir;
        if (!Directory.Exists(streamDir)) Directory.CreateDirectory(streamDir);
        //copy 所以lua文件
        string[] srcDirs = { AppConst.FrameworkRoot+"/Lua" };
        for (int i = 0; i < srcDirs.Length; i++)
        {
            CopyLuaBytesFiles(srcDirs[i], streamDir);
        }
        string[] dirs = Directory.GetDirectories(streamDir, "*", SearchOption.AllDirectories);
        for (int i = 0; i < dirs.Length; i++)
        {
            string name = dirs[i].Replace(streamDir, string.Empty);
            name = name.Replace('\\', '_').Replace('/', '_');
            name = "lua/lua_" + name.ToLower() + AppConst.ExtName;
            //if (name.IndexOf("lua_games") != -1)
            //{
            //    continue;
            //}
            string path = "Assets" + dirs[i].Replace(Application.dataPath, "");
            AddBuildMap(name, "*.bytes", path, maps);

        }
        AddBuildMap("lua/lua" + AppConst.ExtName, "*.bytes", "Assets/" + AppConst.LuaTempDir, maps);

        //Directory.Delete(streamDir,true);
        return maps;

    }

    static void AddBuildMap(string bundleName, string pattern, string path, List<AssetBundleBuild> maps)
    {
        string[] files = Directory.GetFiles(path, pattern);
        if (files.Length == 0) return;

        for (int i = 0; i < files.Length; i++)
        {
            files[i] = files[i].Replace('\\', '/');
        }
        AssetBundleBuild build = new AssetBundleBuild();
        build.assetBundleName = bundleName;
        build.assetNames = files;
        maps.Add(build);
    }
    static void CopyLuaBytesFiles(string sourceDir, string destDir, bool appendext = true, string searchPattern = "*.lua", SearchOption option = SearchOption.AllDirectories)
    {
        if (!Directory.Exists(sourceDir))
        {
            return;
        }

        string[] files = Directory.GetFiles(sourceDir, searchPattern, option);
        int len = sourceDir.Length;

        if (sourceDir[len - 1] == '/' || sourceDir[len - 1] == '\\')
        {
            --len;
        }

        for (int i = 0; i < files.Length; i++)
        {
            string str = files[i].Remove(0, len);
            string dest = destDir + "/" + str;
            if (appendext) dest += ".bytes";
            string dir = Path.GetDirectoryName(dest);
            Directory.CreateDirectory(dir);
          //  File.Copy(files[i], dest, true);

            //--写文件操作
            byte[] allBytes =  File.ReadAllBytes(files[i]);

            byte[] resBytes = Util.Decryption(allBytes, LuaFileUtils.Lua_Key_Ras_Pub);
            File.WriteAllBytes(dest, resBytes);
        }
    }
}
