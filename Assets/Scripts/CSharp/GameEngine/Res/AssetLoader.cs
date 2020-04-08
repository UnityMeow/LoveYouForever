#region --------------------------文件信息--------------------------------------

/******************************************************************
** 文件名:	AssetLoader
** 版  权:	(C)  
** 创建人:  Unity喵
** 日  期:	
** 描  述: 	
*******************************************************************/

#endregion

using System;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace LoveYouForever
{
    /// <summary>
    /// 资源加载器
    /// </summary>
    public class AssetLoader
    {
        /// <summary>
        /// 资源加载配置
        /// </summary>
        public class AssetLoadConfig
        {
            /// <summary>
            /// 资源标签
            /// </summary>
            public string Label;

            /// <summary>
            /// 资源是否可释放
            /// 默认为不可释放
            /// </summary>
            public bool Release = false;

            /// <summary>
            /// 资源类型 待定
            /// </summary>
            public Type type;
        }

        /// <summary>
        /// 是否加载完成
        /// </summary>
        public bool IsDone { get; private set; }

        /// <summary>
        /// 加载结束后回调
        /// </summary>
        private Action completed;

        /// <summary>
        /// 资源加载配置表
        /// </summary>
        private List<AssetLoadConfig> assetLoadConfigs;

        /// <summary>
        /// 当前加载下标
        /// </summary>
        private int curLoadIndex = -1;

        /// <summary>
        /// 当前加载资源Handle
        /// </summary>
        private AsyncOperationHandle curHandle;

        /// <summary>
        /// 添加资源加载的配置信息
        /// </summary>
        public void AddLoadConfig(AssetLoadConfig[] assetLoadConfig)
        {
            assetLoadConfigs = assetLoadConfigs ?? new List<AssetLoadConfig>();
            assetLoadConfigs.AddRange(assetLoadConfig);
        }

        /// <summary>
        /// 开始加载资源
        /// </summary>
        public void StartLoad(Action completed = null)
        {
            this.completed = completed;
            nextLoad();
        }

        /// <summary>
        /// 继续加载资源
        /// </summary>
        private void nextLoad()
        {
            // 根据当前加载索引获取加载配置
            var assetConfig = assetLoadConfigs[++curLoadIndex];

            // 根据资源管理器获取当期加载资源Handle
            curHandle = AssetManager.LoadAssetsAsync(assetConfig.type, assetConfig.Label, assetConfig.Release);

            // 当前资源Handle加载完成时间添加
            curHandle.Completed += onLoadCompleted;
        }

        /// <summary>
        /// 加载完成后事件回调
        /// </summary>
        private void onLoadCompleted(AsyncOperationHandle handle)
        {
            // 加载失败
            if (handle.Status == AsyncOperationStatus.Failed)
            {
                throw new Exception($"资源加载失败{assetLoadConfigs[curLoadIndex].Label}");
            }

            // 是否加载完成
            if (curLoadIndex == assetLoadConfigs.Count - 1)
            {
                IsDone = true;
                completed?.Invoke();
            }
            else
            {
                // 继续加载
                nextLoad();
            }
        }
    }
}