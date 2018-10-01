using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using YY;

public class ResourcesEditor : Editor
{
    static Dictionary<string, string> names;

    private static string ResUrlBase = "Assets/" + AppConst.AppName + "/";
    


    [MenuItem("PackTool/Set res ab name", false, 301)]
    public static void SetResABName()
    {
        #region 多个资源       
        string[] dirsMult = Directory.GetDirectories(ResUrlBase+"Resources_AB/multiple", "*", SearchOption.AllDirectories);
        List<string> dirsMultList = dirsMult.ToList();
        List<string> removeDirsMultListPub = new List<string>();
        for (int i = 0; i < dirsMultList.Count; i++)
        {
            dirsMultList[i] = dirsMultList[i].Replace('\\', '/');
        }

        //剔除上级目录
        for (int i = 0; i < dirsMultList.Count; i++)
        {
            string temp = dirsMultList[i];
            for (int j = 0; j < dirsMultList.Count; j++)
            {
                if (dirsMultList[j].Contains(temp) && temp.Length < dirsMultList[j].Length)
                {
                    removeDirsMultListPub.Add(temp);
                }
            }
        }

        for (int i = 0; i < removeDirsMultListPub.Count; i++)
        {
            dirsMultList.Remove(removeDirsMultListPub[i]);
        }

        //遍历每个子目录中文件
        for (int i = 0; i < dirsMultList.Count; i++)
        {
            string abName = dirsMultList[i].Replace(ResUrlBase+"Resources_AB/", "");
            setABName(dirsMultList[i], abName);
        }
        #endregion

        #region 单个资源
        string[] dirsSingle = Directory.GetDirectories(ResUrlBase+"Resources_AB/single", "*", SearchOption.AllDirectories);
        List<string> dirsSingleList = dirsSingle.ToList();
        List<string> removeDirsSingleListPub = new List<string>();
        for (int i = 0; i < dirsSingleList.Count; i++)
        {
            dirsSingleList[i] = dirsSingleList[i].Replace('\\', '/');
        }

        for (int i = 0; i < dirsSingleList.Count; i++)
        {
            string temp = dirsSingleList[i];
            for (int j = 0; j < dirsSingleList.Count; j++)
            {
                if (dirsSingleList[j].Contains(temp) && temp.Length < dirsSingleList[j].Length)
                {
                    removeDirsSingleListPub.Add(temp);
                }
            }
        }

        for (int i = 0; i < removeDirsSingleListPub.Count; i++)
        {
            dirsSingleList.Remove(removeDirsSingleListPub[i]);
        }

        for (int i = 0; i < dirsSingleList.Count; i++)
        {
            setABName(dirsSingleList[i]);
        }
        #endregion
    }


    private static void setABName(string filePath, string abName = null)
    {
        var files = Directory.GetFiles(filePath, "*.*", SearchOption.AllDirectories);
        foreach (string s in files)
        {
            string fileName = Path.GetFileName(s);
            if (!fileName.Contains(".meta"))
            {
                AssetImporter importer = AssetImporter.GetAtPath(filePath + "/" + fileName);
                if (abName != null)
                {
                    if (abName.Contains(".prefab"))
                    {
                        abName = abName.Replace(".prefab", "");
                    }
                    importer.assetBundleName = abName + ".unity3d";
                }
                else
                {
                    string tempStr = filePath.Replace(ResUrlBase+"Resources_AB/", "") + "/" + fileName + ".unity3d";
                    if (tempStr.Contains(".prefab"))
                    {
                        tempStr = tempStr.Replace(".prefab", "");
                    }
                    importer.assetBundleName = tempStr;
                }
            }
        }
    }

    //[MenuItem("Tools/FrameWork/SetRescourcesMsg")]
    [MenuItem("PackTool/SetRescourcesMsg", false, 302)]
    public static void SetRescourcesMsg()
    {
        names = new Dictionary<string, string>();
        string str = "ResourceMsg = {";
        SetRescourcesMsg(ref str, Application.dataPath+"/"+AppConst.AppName + "/Resources/", true);
        SetRescourcesMsg(ref str, Application.dataPath + "/" + AppConst.AppName + "/Resources_AB/", false);
        if (str != "ResourceMsg = {")
        {
            str = str.Substring(0, str.Length - 2);
        }
        str += "};";

        string luaRes = "Assets/" + AppConst.AppName + "/Lua/config/rescourcesmsg.lua";
        using (var s = System.IO.File.Create(luaRes))
        {
            //UnityEngine.Debug.Log(str);
            byte[] data = System.Text.Encoding.UTF8.GetBytes(str);
            s.Write(data, 0, data.Length);
        }

        AssetDatabase.Refresh();

    }

    static void SetRescourcesMsg(ref string str, string resPath, bool isLocal, string bootfolderPath = null)
    {
        DirectoryInfo folder = new DirectoryInfo(resPath);

        string bootPath = string.Empty;
        if (bootfolderPath == null)
        {
            bootPath = folder.FullName;
        }
        else
        {
            bootPath = bootfolderPath;
        }
        foreach (FileInfo file in folder.GetFiles())
        {
            if (file.Extension.Contains(".DS_Store"))
            {
                continue;
            }
            if (file.Extension.Contains(".svn") || file.Extension == ".meta" || (file.Extension == ".manifest" && file.Name != "StreamingAssets.manifest") || file.Extension == ".zip" || file.Extension == ".asset" || file.Name == "StreamingAssets") continue;

            if (file.Name.IndexOf(" ") >= 0)
            {
                //Debug.LogError("资源名称有空格：" + file.Name);
            }
            string tempname = file.Name;
            if (file.Extension != string.Empty)
            {
                tempname = file.Name.Replace(file.Extension, "");
            }
            if (Regex.IsMatch(tempname, "[A-Z]"))
            {
                Debug.LogWarning("资源名称有大写字母：" + tempname);
            }
            if (names.ContainsKey(tempname))
            {
                if (file.Extension.Contains(".txt"))
                {
                    continue;
                }
                else
                {
                    Debug.LogError("资源名称重复：" + file.FullName + "\n" + names[tempname]);
                }
            }
            else
            {
                names.Add(tempname, file.FullName);
            }

            //UnityEngine.Debug.Log(file.FullName);
            string name = string.Empty;
            string path = string.Empty;
            if (isLocal)
            {
                if (file.Extension != string.Empty)
                {
                    name = file.Name.Replace(file.Extension, "");
                    path = file.FullName.Replace(bootPath, "").Replace(file.Name, "").Replace("\\", "/").ToLower() + file.Name;
                }
                else
                {
                    name = file.Name;
                    path = file.FullName.Replace(bootPath, "").Replace("\\", "/").Replace(file.Name, "").ToLower() + file.Name;
                }
            }
            else
            {
                name = file.Name.Replace(file.Extension, "");
                string _source = Replace(file.FullName);
                string _assetPath = "Assets" + _source.Substring(Application.dataPath.Length);
                path = AssetImporter.GetAtPath(_assetPath).assetBundleName;

                if (path != "")
                {
                }
                else if (file.Extension != string.Empty)
                {
                    name = file.Name.Replace(file.Extension, "");
                    path = file.FullName.Replace(bootPath, "").Replace(file.Name, "").Replace("\\", "/").ToLower() + file.Name;

                }
                else
                {
                    name = file.Name;
                    path = file.FullName.Replace(bootPath, "").Replace("\\", "/").Replace(file.Name, "").ToLower() + file.Name;
                }

                if (path.Contains("multiple"))
                {

                }
                else if (path.Contains("single"))
                {
                    path = path.Replace(Path.GetExtension(path), ".unity3d");
                }
            }

            if (name == "" || path.Contains(".svn"))
            {
                continue;
            }

            if (isLocal)
            {
                str += "[\"" + name.Replace(".unity3d", "") + "\"]" + " = {id = \"" + name.Replace(".unity3d", "") + "\" ,path = \"" + path.Replace(file.Extension, "") + "\",isLocal = " + isLocal.ToString().ToLower() + "},\n";
            }
            else
            {//UnityEngine.Debug.Log(path + "===" + name + "===" + isLocal);
                str += "[\"" + name.Replace(".unity3d", "") + "\"]" + " = {id = \"" + name.Replace(".unity3d", "") + "\" ,path = \"" + path + "\",isLocal = " + isLocal.ToString().ToLower() + "},\n";
            }
        }

        foreach (DirectoryInfo NextFolder in folder.GetDirectories())
        {
            if (NextFolder.Name != "lua")
                SetRescourcesMsg(ref str, NextFolder.FullName, isLocal, bootPath);
        }
    }

    static string Replace(string s)
    {
        return s.Replace("\\", "/");
    }


}
