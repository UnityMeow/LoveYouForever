#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	Chunk
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
	public class Chunk
	{
        /// <summary>
        /// 池子容器
        /// </summary>
        List<Object> objectList;

        /// <summary>
        /// 初始化容器
        /// </summary>
        public Chunk()
        {
            objectList = new List<Object>();
        }
        /// <summary>
        /// 是否存在对象
        /// </summary>
        public bool IsHave => objectList.Count > 0;

        /// <summary>
        /// 从池子里取出对象
        /// </summary>
        /// <returns></returns>
        public Object GetObj()
        {
            // 取第一个
            Object obj = objectList[objectList.Count - 1];
            // 从池子中移除
            objectList.RemoveAt(objectList.Count - 1);
            return obj;
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        public void RevertObj(Object obj)
        {
            objectList.Add(obj);
        }
    }
}
