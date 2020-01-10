#region --------------------------文件信息--------------------------------------

/******************************************************************
** 文件名:	AssetManager
** 版  权:	(C)  
** 创建人:  Unity喵
** 日  期:	
** 描  述: 	
*******************************************************************/

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace LoveYouForever
{
    /// <summary>
    /// 资源管理
    /// </summary>
    public static class AssetManager
    {

        /// <summary>
        /// 不可释放资源
        /// </summary>
        private static Dictionary<string, Dictionary<Type, AsyncOperationHandle>> staticAssetDic = new Dictionary<string, Dictionary<Type, AsyncOperationHandle>>();
        
        /// <summary>
        /// 可释放资源
        /// </summary>
        private static Dictionary<string, Dictionary<Type, AsyncOperationHandle>> dynamicAssetDic = new Dictionary<string, Dictionary<Type, AsyncOperationHandle>>();

        /// <summary>
        /// 根据不同类型载入资源
        /// </summary>
        private static Dictionary<Type, Func<string, bool, AsyncOperationHandle>> assetsTypeLoader =
            new Dictionary<Type, Func<string, bool, AsyncOperationHandle>>
            {
                {typeof(Texture), (lable, releasable) => LoadAssetsAsync<Texture>(lable, releasable)},
                {typeof(Texture2D), (lable, releasable) => LoadAssetsAsync<Texture2D>(lable, releasable)},
                {typeof(Sprite), (lable, releasable) => LoadAssetsAsync<Sprite>(lable, releasable)},
                {typeof(GameObject), (lable, releasable) => LoadAssetsAsync<GameObject>(lable, releasable)},
                {typeof(ScriptableObject), (lable, releasable) => LoadAssetsAsync<ScriptableObject>(lable, releasable)},
                {typeof(Text), (lable, releasable) => LoadAssetsAsync<Text>(lable, releasable)},
                {typeof(TextAsset), (lable, releasable) => LoadAssetsAsync<TextAsset>(lable, releasable)},
            };

        /// <summary>
        /// 获取已加载的资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetAsset<T>(string key)
        {
            // 获取加载类型
            var type = typeof(T);
            // 获取资源handle
            var handle = getLoadedAssetHandle(key, type);
            // 获取handle失败
            if (!handle.IsValid())
            {
                // 根据资源key 资源类型 获取资源定位
                var resourceLocations = Addressables.LoadResourceLocationsAsync(key, type);
                if (resourceLocations.Result != null && resourceLocations.Result.Count > 0)
                {
                    handle = Addressables.LoadAssetAsync<T>(resourceLocations.Result[0]);
                    // 存储handle
                    addAssetHandle(key, type, handle);
                    Addressables.Release(resourceLocations);
                }
                else
                {
                    Addressables.Release(resourceLocations);
                    return default;
                }
            }

            // 如果该资源未提前加载
            if (!handle.IsDone)
            {
                throw new Exception("必须提前加载资源");
            }
            
            return (T)handle.Result;
        }

        public static GameObject LoadPrefab(string key)
        {
            GameObject go = GetAsset<GameObject>(key + ".prefab");
            return GlobalMonoManager.Instance.GoInstan(go);
        }


        /// <summary>
        /// 储存资源Handle
        /// </summary>
        /// <param name="key"></param>
        /// <param name="type"></param>
        /// <param name="handle"></param>
        private static void addAssetHandle(string key,Type type, AsyncOperationHandle handle)
        {
            // 获取是否有key对应的typeHandle
            if (!dynamicAssetDic.TryGetValue(key, out var typeHandle))
            {
                // 新建typeHandle
                typeHandle = new Dictionary<Type, AsyncOperationHandle>();
                dynamicAssetDic.Add(key,typeHandle);
            }
            // 类型 handle 添加
            typeHandle.Add(type,handle);
        }

        /// <summary>
        /// 获取资源Handle
        /// </summary>
        private static Dictionary<Type, AsyncOperationHandle> getAssetHandle(string lable, Type type, bool releasable, out AsyncOperationHandle handle)
        {
            // 根据释放类型获取相应dic
            var handlesDic = releasable ? dynamicAssetDic : staticAssetDic;
            // 如果没有该lable资源handleDic 则新建一个
            if (!handlesDic.TryGetValue(lable, out var typeHandleDic))
            {
                typeHandleDic = new Dictionary<Type, AsyncOperationHandle>();
                handlesDic.Add(lable,typeHandleDic);
            }
            // 获取资源handle
            typeHandleDic.TryGetValue(type, out handle);
            // 返回资源类型HandleDic
            return typeHandleDic;
        }

        /// <summary>
        /// 获取已加载资源Handle
        /// 首次读取为空
        /// </summary>
        /// <returns></returns>
        private static AsyncOperationHandle getLoadedAssetHandle(string key,Type type)
        {
            // 新建handle
            AsyncOperationHandle handle = new AsyncOperationHandle();

            // 在不可释放资源中查找资源key是否存在
            if (!staticAssetDic.TryGetValue(key, out var typeHandle))
            {
                dynamicAssetDic.TryGetValue(key, out typeHandle);
            }

            // 不为空则获取相应handle
            typeHandle?.TryGetValue(type, out handle);
            return handle;
        }



        /// <summary>
        /// 加载资源
        /// </summary>
        /// <returns></returns>
        public static AsyncOperationHandle LoadAssetsAsync(Type type, string lable, bool releasable = false)
        {
            return assetsTypeLoader[type].Invoke(lable, releasable);
        }

        /// <summary>
        /// 异步加载多个资源
        /// </summary>
        /// <param name="lable"></param>
        /// <param name="releasable"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static AsyncOperationHandle<IList<T>> LoadAssetsAsync<T>(string lable, bool releasable = false)
        {
            // 获取资源类型
            var type = typeof(T);
            // 获取资源类型Handle
            var typeHandleDic = getAssetHandle(lable, type, releasable, out var handle);
            // 是否不为空并具有相同版本的该句柄
            if (!handle.IsValid())
            {
                // 加载资源
                handle = Addressables.LoadAssetsAsync<T>(lable,null);
                // 存储资源
                typeHandleDic.Add(type,handle);
            }

            return handle.Convert<IList<T>>();
        }
    }
}