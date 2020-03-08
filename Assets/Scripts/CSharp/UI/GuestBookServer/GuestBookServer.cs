#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	GuestBookServer
** 版  权:	(C)  
** 创建人:  Unity喵
** 日  期:	
** 描  述: 	
*******************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace LoveYouForever
{
	public class GuestBookServer : InstanceNull<GuestBookServer>
	{
		private readonly string baseURL = "http://lyf.unitymeow.com";

        /// <summary>
        /// 字典里是正在加载的url和对应的AsyncOperation
        /// </summary>
        private readonly Dictionary<string, UnityWebRequestAsyncOperation> cachingAsync = new Dictionary<string, UnityWebRequestAsyncOperation>();
        
        /// <summary>
        /// 字典里是URL和对应的文本缓存
        /// </summary>
        private readonly Dictionary<string, string> textCache = new Dictionary<string, string>();
        
        /// <summary>
        /// 字典里是URL和对应的上次缓存时间
        /// </summary>
        private readonly Dictionary<string, long> cachingTime = new Dictionary<string, long>();
        
        /// <summary>
        /// 默认的缓存超时时间
        /// </summary>
        private const long DefaultTimeout = 300000000;

        /// <summary>
        /// 正在加载异步句柄数量
        /// </summary>
        public int CachingCount => cachingAsync.Count;

        /// <summary>
        /// 添加弹幕数据
        /// </summary>
        /// <param name="commonData">弹幕数据</param>
        /// <param name="onCompleted">添加成功回调</param>
        /// <param name="onError">错误回调</param>
        public void AddDanamku(
            GuestBookCommonData commonData, 
            Action<Result> onCompleted, 
            Action<string, long, string> onError)
        {
            string body = JsonConvert.SerializeObject(commonData);
            SendWebRequestText(
                $"{baseURL}/loveyouforever/addDanmaku", 
                resultText =>
                {
                    var result = JsonConvert.DeserializeObject<Result>(resultText);
                    onCompleted?.Invoke(result);
                }, onError, body);
        }
        
        /// <summary>
        /// 删除弹幕数据
        /// </summary>
        /// <param name="removeData">删除数据</param>
        /// <param name="onCompleted">删除成功回调</param>
        /// <param name="onError">错误回调</param>
        public void RemoveDanamku(
            RemoveData removeData, 
            Action<Result> onCompleted, 
            Action<string, long, string> onError)
        {
            string body = JsonConvert.SerializeObject(removeData);
            SendWebRequestText(
                $"{baseURL}/loveyouforever/removeDanamku", 
                resultText =>
                {
                    var result = JsonConvert.DeserializeObject<Result>(resultText);
                    onCompleted?.Invoke(result);
                },onError,
                body);
        }
        
        /// <summary>
        /// 清空弹幕数据
        /// </summary>
        /// <param name="clearData">删除数据</param>
        /// <param name="onCompleted">删除成功回调</param>
        /// <param name="onError">错误回调</param>
        public void ClearDanamku(
            ClearData clearData, 
            Action<Result> onCompleted, 
            Action<string, long, string> onError)
        {
            string body = JsonConvert.SerializeObject(clearData);
            SendWebRequestText(
                $"{baseURL}/loveyouforever/clearDanamkus", 
                resultText =>
                {
                    var result = JsonConvert.DeserializeObject<Result>(resultText);
                    onCompleted?.Invoke(result);
                },onError,
                body);
        }
    
        /// <summary>
        /// 获取弹幕数据
        /// </summary>
        /// <param name="scene">场景名</param>
        /// <param name="onCompleted">获取成功回调</param>
        /// <param name="onError">获取失败回调</param>
        public void ListCountedDanmakus(
            Action<GuestBookResult> onCompleted, 
            Action<string, long, string> onError,
            string scene = "welcome")
        {
            SendWebRequestText(
                $"{baseURL}/loveyouforever/listCountedDanmakus/{scene}", 
                resultText =>
                {
                    var result = JsonConvert.DeserializeObject<GuestBookResult>(resultText);
                    onCompleted?.Invoke(result);
                }, onError);
        }
    
        #region 暂时无用
        /// <summary>
        /// 清空缓存
        /// </summary>
        public void ClearCache()
        {
            // 清空正在加载
            cachingAsync.Clear();
            // 清空文本缓存
            textCache.Clear();
            // 清空上一次缓存时间
            cachingTime.Clear();
        }
    
        // public void ListDanmakus(string scene, Action<ListDanmakusResult> onCompleted, Action<string, long, string> onError)
        // {
        //     SendWebRequestText($"{baseURL}/loveyouforever/listDanmakus/{scene}", resultText =>
        //     {
        //         var result = JsonConvert.DeserializeObject<ListDanmakusResult>(resultText);
        //         onCompleted?.Invoke(result);
        //     }, onError);
        // }
        #endregion
    
        // 1.加载A，第一次的回调是onCompleted1，那我会告诉我正在加载A
        // 2.又加载A，但是1的加载A还没完成，第二次的回调是onCompleted2，我不去真的加载A，只要在完成的时候做我的onCompleted2就可以了
        // 3.那么加载完成会发生什么呢？首先调用一次onCompleted1，移除正在加载状态，调用一次onCompleted2
        // 4.如果1,2都已经完成，并且成功了，并且缓存没过期，这个时候加载A，第三次回调是onCompleted3，那么我们这里直接做的是对缓存的内容做onCompleted3
        private void SendWebRequestText(string url, Action<string> onCompleted, Action<string, long, string> onError, string body = null, long timeout = DefaultTimeout)
        {
            // 是否正在加载
            if (cachingAsync.ContainsKey(url))
            {
                // 先拿到正在加载的请求
                var www = cachingAsync[url].webRequest;
                // 对正在加载的请求AsyncOperation加completed回调
                cachingAsync[url].completed += operation =>
                {
                    // 因为已经不是首次加载，没必要在后续的回调中去掉正在加载状态，只需要调用OnComplete方法
                    OnComplete(url, onCompleted, onError, www);
                };
            }
            else
            {
                // 是否POST(POST要传数据，不能缓存)， 是否没缓存过(上一次缓存时间不存在），是否上次缓存已过期（当前时间-上次缓存时间>超时时间）
                if (!string.IsNullOrEmpty(body) || !cachingTime.ContainsKey(url) || DateTime.UtcNow.Ticks - cachingTime[url] > timeout)
                {
                    // 如果有body为空或者空字符串，那么他是一个GET请求（不用传数据）；否则，他是一个POST请求（要传数据）
                    var www = new UnityWebRequest(url, string.IsNullOrEmpty(body) ? "GET" : "POST");
                    // 如果有body不为空或者空字符串，那么他是一个POST请求（要传数据）
                    if (!string.IsNullOrEmpty(body))
                    {
                        // 创建一个UploadHandlerRaw，参数是body的UTF-8的字节
                        UploadHandler uh = new UploadHandlerRaw(Encoding.UTF8.GetBytes(body));
                        // Content-Type为application/json
                        uh.contentType = "application/json";
                        // 赋值uh给www
                        www.uploadHandler = uh;
                    }
                    www.downloadHandler = new DownloadHandlerBuffer();
    
                    // 把创建的请求发出去
                    var request = www.SendWebRequest();
                    // 记住我已经在加载了，并且把这个AsyncOperation保存起来，方便下一次加completed回调
                    cachingAsync[url] = request;
                    // 首次加载，对正在加载的请求AsyncOperation加completed回调
                    request.completed += operation =>
                    {
                        // 调用OnComplete方法
                        OnComplete(url, onCompleted, onError, www);
                        // 把表示正在加载的AsyncOperation干掉，根据url来移除，所以代表我已经不是正在加载了
                        cachingAsync.Remove(url);
                    };
                }
                else // 是GET请求，并且缓存过，并且缓存没过期
                {
                    // 直接对缓存的内容调用onCompleted
                    onCompleted(textCache[url]);
                }
            }
        }
    
        private void OnComplete(string url, Action<string> onCompleted, Action<string, long, string> onError, UnityWebRequest www)
        {
            // 如果是错误
            if (www.isNetworkError || www.isHttpError)
            {
                // 调用onError的回调（url地址，返回码，错误信息）
                onError?.Invoke(www.url, www.responseCode, www.error);
            }
            else// 如果没错误
            {
                // 记住上一次缓存的时间
                cachingTime[url] = DateTime.UtcNow.Ticks;
                // 把请求返回的结果存到缓存里面
                textCache[url] = www.downloadHandler.text;
                // 调用onCompleted回调（请求返回的结果）
                onCompleted?.Invoke(textCache[url]);
            }
        }
	}
}
