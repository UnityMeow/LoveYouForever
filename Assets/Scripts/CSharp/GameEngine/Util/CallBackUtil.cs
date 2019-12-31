#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	CallBackUtil
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
using UnityEngine.ResourceManagement.AsyncOperations;

namespace LoveYouForever
{
    //事件中心委托回调
    public delegate void EventCallBack();
    //无参数委托回调
    public delegate void CallBack();
    /// <summary>
    /// 资源加载回调
    /// </summary>
    public delegate void AssetHandleEvent(AsyncOperationHandle operationHandle);
}
