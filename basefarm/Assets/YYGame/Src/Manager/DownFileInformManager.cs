using System.Collections.Generic;
using System.IO;

namespace YY
{

    /// <summary>
    /// 下载完成 --后改变 版本号  保存文件
    /// </summary>
    public class DownFileInformManager
    {
        private static object sLock = new object();

        private static DownFileInformManager instance;

        //public GameVersionInfo localGameVersionInfo = null;

        //public GameVersionInfo serverGameVersionInfo = null;


        public string versionFliesUrl = string.Empty;

        public static DownFileInformManager GetInstance()
        {
            if (instance == null)
            {
                instance = new DownFileInformManager();
            }
            return instance;
        }

        public void InitLoaclFiles()
        {
            versionFliesUrl = Util.DataPath + "files.txt";
            string path = Path.GetDirectoryName(versionFliesUrl);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            bool hasVersionTxt = File.Exists(versionFliesUrl);
            if (hasVersionTxt)
            {
                //localGameVersionInfo = UnityEngine.JsonUtility.FromJson(File.ReadAllText(versionFliesUrl), typeof(GameVersionInfo)) as GameVersionInfo;
            }

        }
        /// <summary>
        /// 检测是否需要 下载 资源
        /// </summary>
        /// <returns></returns>
        public bool CheckNeedDownRes()
        {
            //if (HPSGConst.AppResVer == GameUserMode.Review_Style)
            //{
            //    return false;
            //}
            //if (localGameVersionInfo != null &&  localGameVersionInfo.style == 2)
            //{
            //    if (localGameVersionInfo.version.Equals(GameUserMode.ServerResourceVer) && localGameVersionInfo.version.Equals(GameUserMode.Remote_Resource_Version))
            //    {
            //        return false;
            //    }
            //}
            return true;
        }



        void OnSaveFilesInfo()
        {
            //if (localGameVersionInfo != null)
            //{
            //    try
            //    {
            //        string content = UnityEngine.JsonUtility.ToJson(localGameVersionInfo);
            //        FileStream fs = new FileStream(versionFliesUrl, FileMode.Create);
            //        StreamWriter sw = new StreamWriter(fs);
            //        sw.Write(content);
            //        sw.Close();
            //        fs.Close();
            //    }
            //    catch (System.Exception e)
            //    {

            //    }

            //}
        }

        /// <summary>
        /// 检测需要下载的资源
        /// </summary>
        //public void OnCheckUpdateOnLineFile(out List<DownLoadFileUnit> m_downLoadList, out long m_downloadSize)
        //{
        //    string downfileUrl = GameUserMode.GetServerResUrl() + "{0}?v=" + serverGameVersionInfo.version;
        //    //long
        //    m_downLoadList = new List<jmz.DownLoadFileUnit>();
        //    m_downloadSize = 0;

        //    if (localGameVersionInfo == null)
        //    {
        //        //下载 allgame.zip
        //        if (serverGameVersionInfo.allpack != null && serverGameVersionInfo.allpack.Length > 0)
        //        {
        //            m_downloadSize += serverGameVersionInfo.allsize;
        //            m_downLoadList.Add(new jmz.DownLoadFileUnit(
        //                         string.Format(downfileUrl, serverGameVersionInfo.allpack),
        //            serverGameVersionInfo.allpack,
        //           GameDownloadManager.Init_Res_Group,
        //            Util.DataPath + serverGameVersionInfo.allpack,
        //             "",
        //            serverGameVersionInfo.allsize,
        //            "zip"));
        //            return;
        //        }
        //    }

        //    for (int j = 0; j < serverGameVersionInfo.files.Count; j++)
        //    {
        //        GameVersionInfo.FilesAttribute file = serverGameVersionInfo.files[j];
        //        bool nofile = true;
        //        if (localGameVersionInfo != null)
        //        {
        //            for (int i = 0; i < localGameVersionInfo.files.Count; i++)
        //            {
        //                GameVersionInfo.FilesAttribute f = localGameVersionInfo.files[i];

        //                if (f.name.Equals(file.name))
        //                {
        //                    nofile = false;
        //                    if (f.md5.Equals(file.md5) == false)
        //                    {
        //                        m_downloadSize += file.size;
        //                        m_downLoadList.Add(new jmz.DownLoadFileUnit(
        //                                     string.Format(downfileUrl, file.name),
        //                        file.name,
        //                       GameDownloadManager.Init_Res_Group,
        //                        Util.DataPath + file.name,
        //                         file.md5,
        //                        file.size,
        //                        ""));
        //                    }
        //                    break;
        //                }
        //            }
        //        }
        //        if (nofile)
        //        {
        //            m_downloadSize += file.size;
        //            m_downLoadList.Add(new jmz.DownLoadFileUnit(
        //                         string.Format(downfileUrl, file.name),
        //            file.name,
        //           GameDownloadManager.Init_Res_Group,
        //            Util.DataPath + file.name,
        //             file.md5,
        //            file.size,
        //            ""));
        //        }
        //    }
        //}

        /// <summary>
        /// 更新场景资源
        /// </summary>
        //public void OnCheckUpateHallFile(out List<DownLoadFileUnit> m_downLoadList, out long m_downloadSize)
        //{
        //    string downfileUrl = GameUserMode.GetServerResUrl() + "{0}?v=" + serverGameVersionInfo.version;
        //    //long
        //    m_downLoadList = new List<jmz.DownLoadFileUnit>();
        //    m_downloadSize = 0;
        //    for (int j = 0; j < serverGameVersionInfo.exafiles.Count; j++)
        //    {
        //        GameVersionInfo.FilesAttribute file = serverGameVersionInfo.exafiles[j];
        //        bool nofile = true;
        //        if (localGameVersionInfo != null)
        //        {
        //            for (int i = 0; i < localGameVersionInfo.files.Count; i++)
        //            {
        //                GameVersionInfo.FilesAttribute f = localGameVersionInfo.files[i];

        //                if (f.name.Equals(file.name))
        //                {
        //                    nofile = false;
        //                    if (f.md5.Equals(file.md5) == false)
        //                    {
        //                        m_downloadSize += file.size;
        //                        m_downLoadList.Add(new jmz.DownLoadFileUnit(
        //                                     string.Format(downfileUrl, file.name),
        //                        f.name,
        //                       GameDownloadManager.Hall_Res_Group,
        //                        Util.DataPath + file.name,
        //                        file.md5,
        //                        file.size,
        //                        ""));
        //                    }
        //                    break;
        //                }
        //            }
        //        }
        //        if (nofile)
        //        {
        //            m_downloadSize += file.size;
        //            m_downLoadList.Add(new jmz.DownLoadFileUnit(
        //                         string.Format(downfileUrl, file.name),
        //            file.name,
        //           GameDownloadManager.Hall_Res_Group,
        //            Util.DataPath + file.name,
        //             file.md5,
        //            file.size,
        //            ""));
        //        }
        //    }
        //}


        public void UpdateLocalFiles(DownLoadFileUnit file)
        {
            lock (sLock)
            {
                //if (localGameVersionInfo == null)
                //{
                //    localGameVersionInfo = new GameVersionInfo();
                //}
                //localGameVersionInfo.OnChangeFileAttr(new GameVersionInfo.FilesAttribute(file.FileName, file.Hash, file.Length));
                OnSaveFilesInfo();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ntype"> 1 初始载完成 2 大厅下载完成 </param>
        public void ChangeLocalStyle(int ntype, string version)
        {
            lock (sLock)
            {
                //if (localGameVersionInfo == null)
                //{
                //    localGameVersionInfo = new GameVersionInfo();
                //}
                //localGameVersionInfo.style = ntype;
                //localGameVersionInfo.version = GameUserMode.Remote_Resource_Version;
                OnSaveFilesInfo();
            }
        }



        //public void CheckGameUpdate(GameVersionInfo serverfile, GameVersionInfo localfile, string gamename, out List<DownLoadFileUnit> m_downLoadList, out long m_downloadSize)
        //{
        //    m_downLoadList = new List<jmz.DownLoadFileUnit>();
        //    m_downloadSize = 0;
        //    string saveFileUrl = Util.DataPath + "games/";

        //    string downfileUrl = GameUserMode.GetServerResUrl() + "games/" + "{0}?v=" + serverfile.version;

        //    for (int i = 0; i < serverfile.files.Count; i++)
        //    {
        //        GameVersionInfo.FilesAttribute file = serverfile.files[i];
        //        bool nofile = true;
        //        if (localfile != null)
        //        {
        //            for (int j = 0; j < localfile.files.Count; j++)
        //            {
        //                GameVersionInfo.FilesAttribute f = localfile.files[j];
        //                if (file.name.Equals(f.name))
        //                {
        //                    nofile = false;
        //                    if (!file.md5.Equals(f.md5))
        //                    {
        //                        m_downloadSize += file.size;
        //                        m_downLoadList.Add(new jmz.DownLoadFileUnit(
        //                                     string.Format(downfileUrl, file.name),
        //                        file.name,
        //                       gamename,
        //                        saveFileUrl + file.name,
        //                         file.md5,
        //                        file.size,
        //                        ""));
        //                    }
        //                }
        //            }
        //        }
        //        if (nofile)
        //        {
        //            m_downloadSize += file.size;
        //            m_downLoadList.Add(new jmz.DownLoadFileUnit(
        //                         string.Format(downfileUrl, file.name),
        //            file.name,
        //           gamename,
        //            saveFileUrl + file.name,
        //             file.md5,
        //            file.size,
        //            ""));
        //        }
        //    }
        //}
    }
}
