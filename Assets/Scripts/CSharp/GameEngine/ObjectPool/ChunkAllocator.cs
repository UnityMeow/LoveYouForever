#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	ChunkAllocator
** 版  权:	(C)  
** 创建人:  Unity喵
** 日  期:	
** 描  述: 	
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
	public class ChunkAllocator : InstanceNull<ChunkAllocator>
    {
        Dictionary<string, Chunk> chunkList;
        public ChunkAllocator()
        {
            chunkList = new Dictionary<string, Chunk>();
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        public void Revert(string poolName, Object obj)
        {
            if (IsHavePool(poolName))
            {
                chunkList[poolName].RevertObj(obj);
            }
            else
            {
                Chunk chunk = new Chunk();
                chunk.RevertObj(obj);
                chunkList.Add(poolName, chunk);
            }
        }
        
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="poolName"></param>
        /// <returns></returns>
        public Object GetObject(string poolName)
        {
            if (!IsHavePool(poolName))
            {
                return new Object();
            }
            return chunkList[poolName].GetObj();
        }

        /// <summary>
        /// 获取资源对象
        /// </summary>
        /// <param name="poolName"></param>
        /// <param name="resName"></param>
        /// <returns></returns>
        public T GetObj<T>(string poolName, string resName)
            where T : Object
        {
            if (!IsHavePool(poolName))
            {
                return AssetManager.GetAsset<T>(resName);
            }
            return chunkList[poolName].GetObj() as T;
        }

        /// <summary>
        /// 获取预设体对象
        /// TODO:待修改 池满操作待移动
        /// </summary>
        /// <param name="poolName"></param>
        /// <returns></returns>
        public GameObject GetPrefab(string poolName, string resName, Transform parent = null)
        {
            GameObject go = null;
            if (!IsHavePool(poolName))
            {
                go = AssetManager.LoadPrefab(resName);
            }
            else 
            {
                if (chunkList[poolName].IsHave)
                    go = chunkList[poolName].GetObj() as GameObject;
                else
                    go = AssetManager.LoadPrefab(resName);
            }
            if(parent != null)
                go.transform.SetParent(parent);
            go.transform.localScale = Vector3.one;
            go.transform.position = Vector3.zero;
            go.SetActive(true);
            return go;
        }

        /// <summary>
        /// 清空缓存池
        /// </summary>
        /// <param name="poolName"></param>
        public void ClearPool(string poolName = "")
        {
            if (poolName == "")
            {
                chunkList.Clear();
                return;
            }
            if (IsHavePool(poolName))
                chunkList.Remove(poolName);
        }

        /// <summary>
        /// 池子是否存在
        /// </summary>
        /// <param name="poolName"></param>
        /// <returns></returns>
        bool IsHavePool(string poolName)
        {
            return chunkList.ContainsKey(poolName);
        }
    }
}
