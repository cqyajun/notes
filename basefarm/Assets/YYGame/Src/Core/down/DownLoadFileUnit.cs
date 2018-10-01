using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace YY
{
    /// <summary>
    /// 下载文件信息
    /// </summary>
    public struct DownLoadFileUnit
    {
        /// <summary>
        /// 下载地址
        /// </summary>
        private readonly string m_downloadUrl;

        /// <summary>
        /// 文件名称
        /// </summary>
        private readonly string m_fileName;

        /// <summary>
        /// 文件大小
        /// </summary>
        private readonly long m_length;

        /// <summary>
        /// 文件保存路径
        /// </summary>
        private readonly string m_fileSavePath;

        /// <summary>
        /// 文件组
        /// </summary>
        private readonly string m_groupName;

        /// <summary>
        /// hash
        /// </summary>
        private readonly string m_hash;

        private readonly string m_msg;

        /// <summary>
        /// 创建下载信息
        /// </summary>
        /// <param name="downloadUrl">下载路径</param>
        /// <param name="filePath">保存路径</param>
        /// <param name="length">文件大小</param>
        public DownLoadFileUnit(
            string downloadUrl, string fileName,
            string groupName, string savePath,
            string hash, long length, string msg)
        {
            m_downloadUrl = downloadUrl;
            m_fileName = fileName;
            m_groupName = groupName;
            m_length = length;
            m_fileSavePath = savePath;
            m_hash = hash;
            m_msg = msg;
        }

        public string DownLoadUrl
        {
            get
            {
                return m_downloadUrl;
            }
        }

        public string FilePath
        {
            get
            {
                return m_fileSavePath;
            }
        }

        public string GroupName
        {
            get
            {
                return m_groupName;
            }
        }

        public string FileName
        {
            get
            {
                return m_fileName;
            }
        }

        public long Length
        {
            get
            {
                return m_length;
            }
        }

        public string Hash
        {
            get
            {
                return m_hash;
            }
        }

        public string Msg
        {
            get
            {
                return m_msg;
            }
        }
    }


}