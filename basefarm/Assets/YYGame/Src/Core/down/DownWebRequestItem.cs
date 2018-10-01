using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace YY
{


    public class DownWebRequestItem
    {

        private UnityWebRequest m_webRequest;
        private MonoBehaviour mono;
        private DownLoadFileUnit downLoadFileUnit;

        public bool IsRun = false;
        public int index = 0;

        /// <summary>
        /// 请求结果回调
        /// </summary>
        public Action<DownLoadFileUnit, bool, DownWebRequestItem> WebRequestResultEvent;


        public DownWebRequestItem(MonoBehaviour _mono, Action<DownLoadFileUnit,bool, DownWebRequestItem> WebRequestSuccessEvent)
        {
            this.WebRequestResultEvent = WebRequestSuccessEvent;
            this.mono = _mono;
        }


        public void Send( DownLoadFileUnit downLoadFileUnit)
        {
            
            this.downLoadFileUnit = downLoadFileUnit;
            mono.StartCoroutine(StartDownload(downLoadFileUnit.DownLoadUrl));
        }

        /// <summary>
        /// 重连
        /// </summary>
        public void Reconnection()
        {

            if (m_webRequest != null)
            {
                m_webRequest.Dispose();
                m_webRequest = null;
            }
            Send(this.downLoadFileUnit);
        }

        public IEnumerator StartDownload(string url)
        {
            if (m_webRequest == null)
            {
                m_webRequest = UnityWebRequest.Get(url);
                m_webRequest.chunkedTransfer = true;
                m_webRequest.disposeDownloadHandlerOnDispose = true;
                m_webRequest.downloadHandler = new DownloadHandlerBuffer();
                //m_webRequest.timeout = 10;
                //m_webRequest.SetRequestHeader("Range", "bytes=" + downBytes.Length + "-");
            }
            IsRun = true;
            yield return m_webRequest.Send(); //协程操作
            bool isErro = false;
            if (m_webRequest.isError || !m_webRequest.isDone) // 失败
            {
                Debug.Log("Download Error:" + m_webRequest.error+"" + url);
                isErro = true;
            }
            else
            {
                if (m_webRequest.responseCode == 200)
                {
                    byte[] bytes = m_webRequest.downloadHandler.data;
                        isErro = !Save(bytes);
                        //-- 保存数据
                }
                else
                {
                    isErro = true;
                }

            }
            this.WebRequestResultEvent(this.downLoadFileUnit, isErro,this);
         
        }


        private byte[] CopyToBig(byte[] bytes0, byte[] bytes1)
        {
            byte[] resultBytes = new byte[bytes0.Length + bytes1.Length];
            Buffer.BlockCopy(bytes0, 0, resultBytes, 0, bytes0.Length);
            Buffer.BlockCopy(bytes1, 0, resultBytes, bytes0.Length, bytes1.Length);
            return resultBytes;
        }

        private bool Save(byte[] bytes)
        {
            try
            {
                if (string.IsNullOrEmpty(downLoadFileUnit.FilePath))
                {
                    Debug.LogError("path is empty");
                    return false;
                }
                if (File.Exists(downLoadFileUnit.FilePath))
                {
                    File.Delete(downLoadFileUnit.FilePath);
                }
                string directory = Path.GetDirectoryName(downLoadFileUnit.FilePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                File.WriteAllBytes(downLoadFileUnit.FilePath, bytes);
                return true;
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.ToString() + downLoadFileUnit.FileName);
                return false;
            }
        }

        public float GetProcess()
        {
            if (m_webRequest != null)
            {
          
                return m_webRequest.downloadProgress;
            }
            return 0;

        }

        public long GetCurrentLength()
        {
            if (m_webRequest != null)
            {
                return (long)m_webRequest.downloadedBytes;
            }
            return 0;
        }


        public void Stop()
        {
            if (m_webRequest != null)
            {
                if (IsRun == false)
                {
                    m_webRequest.Abort();
                }
            }
            Destroy();
        }
        public void Destroy()
        {
            if (m_webRequest != null)
            {
                m_webRequest.Dispose();
                m_webRequest = null;
            }
            IsRun = false;
        }

    }

}
