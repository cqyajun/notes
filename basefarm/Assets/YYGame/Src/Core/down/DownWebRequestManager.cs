using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
namespace YY
{
    public class DownWebRequestManager
    {
        private MonoBehaviour mono;

        private static int MAX_REQUEST = 3;
        private Stopwatch sw = new Stopwatch();
        private const int MAX_REC_Count = 3;


        private int curcheckTimeout = 0;

       public string downSpeed = "";
        /// <summary>
        /// 请求列表
        /// </summary>
        private List<DownLoadFileUnit> webRequestList;

        private List<DownWebRequestItem> agentList;

        /// <summary>
        /// 最后请求索引
        /// </summary>
        private int lastWebRequestIndex = 0;
        /// <summary>
        /// 当前重连次数
        /// </summary>
        private int curReconnectionCount = 0;

        /// <summary>
        /// 下载成功数据
        /// </summary>
        private long successSize;

        /// <summary>
        /// 单个下载成功事件
        /// </summary>
        private Action<DownLoadFileUnit> OneFileDownloadSuccess;
        //private Action<long> UpdateWebRequestProgress;
        private Action<DownLoadFileUnit> WebRequestSuccess;
        private Action WebRequestFailure;

        public DownWebRequestManager(MonoBehaviour mono)
        {
            this.mono = mono;
            agentList = new List<DownWebRequestItem>();
        }

        public void SetDownloadEvent(Action<DownLoadFileUnit> _WebRequestSuccess, Action _WebRequestFailure, Action<DownLoadFileUnit> _OneFileDownloadSuccess)
        {
            this.OneFileDownloadSuccess = _OneFileDownloadSuccess;
            this.WebRequestFailure = _WebRequestFailure;
            this.WebRequestSuccess = _WebRequestSuccess;
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="webRequestList"></param>
        public void Requests(List<DownLoadFileUnit> _webRequestList)
        {
            Clear();
            webRequestList = _webRequestList;
            lastWebRequestIndex = 0;
            curReconnectionCount = 0;
            sw.Start();
            Check();
        }
        public void Check()
        {
            for (int i = lastWebRequestIndex; lastWebRequestIndex < webRequestList.Count; i++, lastWebRequestIndex++)
            {
                if (!CheckFreeWebRequest())
                {
                    return;
                }
                DownLoadFileUnit downLoadUnit = webRequestList[lastWebRequestIndex];
                Request(downLoadUnit);
            }
        }

        public bool Request(DownLoadFileUnit downLoadFileUnit)
        {
            if (string.IsNullOrEmpty(downLoadFileUnit.DownLoadUrl))
            {
                return false;
            }

            if (string.IsNullOrEmpty(downLoadFileUnit.FilePath))
            {
                return false;
            }

            if (!CheckFreeWebRequest())
            {
                return false;
            }

            DownWebRequestItem webRequest = GetFreeReqItem();
            webRequest.Send(downLoadFileUnit);
            return true;
        }
        public void WebRequestSuccessHandle(DownLoadFileUnit downLoadFileUnit,bool isErro, DownWebRequestItem item)
        {
            if (!isErro)
            {
                item.Destroy();
                curReconnectionCount = 0;
                this.successSize += downLoadFileUnit.Length;
                if (OneFileDownloadSuccess != null)
                {
                    OneFileDownloadSuccess(downLoadFileUnit);
                }
                if (!IsSuccess())
                {
                    Check();
                    return;
                }
                if (WebRequestSuccess != null)
                {
                    WebRequestSuccess(downLoadFileUnit);
                }
              
            }
            else
            {
                item.Reconnection();
            }
            
        }

        public bool IsSuccess()
        {
            if (webRequestList != null && lastWebRequestIndex < webRequestList.Count)
            {
                return false;
            }
            if (IsRun())
            {
                return false;
            }
            return true;
        }
        public long GetCurDownloadSize
        {
            get
            {
                long curSize = 0;
                long v = successSize;
                int costTime = Convert.ToInt32(sw.Elapsed.TotalSeconds);

                for (int i = 0; i < agentList.Count; i++)
                {
                    curSize += agentList[i].GetCurrentLength();
                   
                }
                long hasDown = curSize + v;
                downSpeed = (hasDown / 1024d / sw.Elapsed.TotalSeconds).ToString("0.00");

                return hasDown;
            }
        }


        public DownWebRequestItem GetFreeReqItem()
        {
            for (int i = 0; i < agentList.Count; i++)
            {
                if (!agentList[i].IsRun)
                {
                    return agentList[i];
                }
            }
            if (agentList.Count < MAX_REQUEST)
            {
                DownWebRequestItem webRequest = new DownWebRequestItem(this.mono, WebRequestSuccessHandle);
                webRequest.index = agentList.Count;
                agentList.Add(webRequest);
                return webRequest;
            }
            return null;
        }
        public bool CheckFreeWebRequest()
        {
            return GetFreeReqItem() != null;
        }

        public bool IsRun()
        {
            for (int i = 0; i < agentList.Count; i++)
            {
                if (agentList[i].IsRun)
                {
                    return true;
                }
            }
            return false;
        }

        public int GetRunWebItemNum()
        {
            int n = 0;
            for (int i = 0; i < agentList.Count; i++)
            {
                if (agentList[i].IsRun)
                {
                    n++;
                }
            }
            return n;
        }
        public void Clear()
        {
            if (this.webRequestList != null)
            {
                this.webRequestList.Clear();
            }
            for (int i = 0; i < agentList.Count; i++)
            {
                agentList[i].Destroy();
                
            }
            successSize = 0;
            curReconnectionCount = 0;
            lastWebRequestIndex = 0;
            sw.Reset();
        }
    }
}