#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	ConfigsManager
** 版  权:	(C)  
** 创建人:  Unity喵
** 日  期:	
** 描  述: 	
*******************************************************************/
#endregion
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoveYouForever
{
    /// <summary>
    /// 配置文件管理
    /// </summary>
	public class ConfigsManager : Single<ConfigsManager>
    {
        /// <summary>
        /// 配置类型
        /// </summary>
        public enum Type
        {
            UIDataMain,
            TestData,
        }

        private Dictionary<Type, ConfigsBase> configsDic = new Dictionary<Type, ConfigsBase>();

        /// <summary>
        /// 加载所有配置文件
        /// </summary>
        public void Load()
        {
            LoadConfig<UIDataMainConfigs>("UIDataMain",Type.UIDataMain);
            LoadConfig<TestDataConfigs>("TestData", Type.TestData);
            //LoadConfig<UIDataMainConfigs>("UIDataMain", Type.UIDataMain);
        }

        /// <summary>
        /// 获取配置文件数据
        /// </summary>
        public T Get<T>(Type type)
            where T : ConfigsBase
        {
            if (configsDic.ContainsKey(type))
            {
                return configsDic[type] as T;
            }
            Debug.Log( $"{type}资源获取失败");
            return null;
        }

        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">资源名</param>
        /// <param name="type"></param>
        private void LoadConfig<T>(string name, Type type)
            where T : ConfigsBase
        {
            var config = AssetManager.GetAsset<ScriptableObject>("Configs/" + name + ".asset") as T;
            configsDic.Add(type, config);
        }
        
	}
}
