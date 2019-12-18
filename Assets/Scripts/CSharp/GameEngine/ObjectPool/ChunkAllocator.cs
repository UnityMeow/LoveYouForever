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
        Dictionary<string, Chunk> _chunkList;
        public ChunkAllocator()
        {
            _chunkList = new Dictionary<string, Chunk>();
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        public void Revert(string poolName, Object obj)
        {
            if (IsHavePool(poolName))
            {
                _chunkList[poolName].RevertObj(obj);
            }
            else
            {
                Chunk chunk = new Chunk();
                chunk.RevertObj(obj);
                _chunkList.Add(poolName, chunk);
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
            return _chunkList[poolName].GetObj();
        }

        /// <summary>
        /// 获取资源对象
        /// </summary>
        /// <param name="poolName"></param>
        /// <param name="pathName"></param>
        /// <param name="resName"></param>
        /// <param name="bagName"></param>
        /// <returns></returns>
        public Object GetObj(string poolName, string pathName, string resName, string bagName = "")
        {
            if (!IsHavePool(poolName))
            {
                return ResLoadManager.Instance.LoadObject<Object>(pathName, resName, bagName);
            }
            return _chunkList[poolName].GetObj();
        }

        /// <summary>
        /// 清空缓存池
        /// </summary>
        /// <param name="poolName"></param>
        public void ClearPool(string poolName = "")
        {
            if (poolName == "")
            {
                _chunkList.Clear();
                return;
            }
            if (IsHavePool(poolName))
                _chunkList.Remove(poolName);
        }

        /// <summary>
        /// 池子是否存在
        /// </summary>
        /// <param name="poolName"></param>
        /// <returns></returns>
        bool IsHavePool(string poolName)
        {
            return _chunkList.ContainsKey(poolName);
        }
    }
}
