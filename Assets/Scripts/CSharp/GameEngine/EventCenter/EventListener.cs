#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	EventListener
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
	public class EventListener
	{
		/// <summary>
		/// 监听事件对象
		/// </summary>
		public object Listener { get; }

		/// <summary>
		/// 回调
		/// </summary>
		public EventCallBack CallBack { get; }

		
		/// <summary>
		/// 构造
		/// </summary>
		/// <param name="listener"></param>
		/// <param name="callback"></param>
		public EventListener(object listener, EventCallBack callback)
		{
			Listener = listener;
			CallBack = callback;
		}
		
	}
}
