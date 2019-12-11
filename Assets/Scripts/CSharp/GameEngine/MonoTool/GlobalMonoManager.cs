#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	GlobalMonoManager
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
	public class GlobalMonoManager : InstanceMono<GlobalMonoManager>
    {
        event CallBack eventUpdate;
        event CallBack eventFixedUpdate;
        void Update()
        {
            eventUpdate?.Invoke();
        }
        void FixedUpdate()
        {
            eventFixedUpdate?.Invoke();
        }
        public void AddUpdateListener(CallBack function)
        {
            eventUpdate += function;
        }
        public void RemoveUpdateListener(CallBack function)
        {
            eventUpdate -= function;
        }
        public void AddFixedUpdateListener(CallBack function)
        {
            eventFixedUpdate += function;
        }
        public void RemoveFixedUpdateListener(CallBack function)
        {
            eventFixedUpdate -= function;
        }
    }
}
