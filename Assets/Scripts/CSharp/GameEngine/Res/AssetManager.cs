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
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

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
        private static Dictionary<string, Dictionary<Type, AsyncOperationHandle>> staticAssetDic;
        
        /// <summary>
        /// 可释放资源
        /// </summary>
        private static Dictionary<string, Dictionary<Type, AsyncOperationHandle>> dynamicAssetDic;
        
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
                if (resourceLocations.Result != null)
                {
                    handle = Addressables.LoadAssetAsync<T>(resourceLocations.Result);
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
            // TODO:待修改
            return new AsyncOperationHandle();
        }
    }
}