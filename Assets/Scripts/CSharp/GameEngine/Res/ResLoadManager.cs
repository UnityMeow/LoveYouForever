#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	ResLoadManager
** 版  权:	(C)  
** 创建人:  Unity喵
** 日  期:	2019.11.13（待修改）
** 描  述: 	Resources资源加载管理器
**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
*******************************************************************/
#endregion
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoveYouForever
{
	public class ResLoadManager : InstanceMono<ResLoadManager>
    {
        //AB加载模式状态
        public static bool IsLoadAssetBundle = false;
        //预设体加载
        public GameObject LoadPrefab(string pathName, string resName, string bagName = "")
        {
            if (IsLoadAssetBundle)
            {
                return null;
                //return Instantiate(AssetBundleManager.Instance.LuaLoadAssetBundle(pathName, bagName, resName) as GameObject);
            }
            else
            {
                //通过路径加载预设体
                GameObject go = Resources.Load<GameObject>(pathName + "/" + resName);
                if (go == null)
                {
                    GlobalUtil.Log("CResLoadMgr: Resources加载路径加载失败");
                    return null;
                }
                return Instantiate(go);
            }
        }
        //资源加载
        public T LoadObject<T>(string pathName, string resName, string bagName = "")
            where T : Object
        {
            if (IsLoadAssetBundle)
            {
                return null;
                //return AssetBundleManager.Instance.LuaLoadAssetBundle(pathName, bagName, resName);
            }
            else
            {
                //通过路径加载资源
                T go = Resources.Load<T>(pathName + "/" + resName);
                if (go == null)
                {
                    GlobalUtil.Log("CResLoadMgr: Resources加载路径加载失败");
                    return null;
                }
                return go;
            }
        }
    }
}
