#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	EventManager
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
	public class EventManager : InstanceNull<EventManager>
	{
		/// <summary>
		/// 事件监听器字典
		/// </summary>
		Dictionary<EventType, List<EventListener>> eventListenerList = new Dictionary<EventType, List<EventListener>>();
		/// <summary>
		/// 注册事件
		/// </summary>
		/// <param name="eventType">事件类型</param>
		/// <param name="listener">事件监听者</param>
		/// <param name="callBack">事件回调</param>
		public void Add(EventType eventType, object listener, EventCallBack callBack)
		{
			if (listener == null)
				return;
			//记录事件
			AddEvent(eventType);
			//添加事件监听器
			eventListenerList[eventType].Add(new EventListener(listener, callBack));
		}

		/// <summary>
		/// 添加事件
		/// </summary>
		/// <param name="eventType">事件类型</param>
		private void AddEvent(EventType eventType)
		{
			if (!eventListenerList.ContainsKey(eventType))
			{
				eventListenerList.Add(eventType, new List<EventListener>());
			}
		}

		/// <summary>
		/// 发送事件
		/// </summary>
		/// <param name="eventType"></param>
		public void SendEvent(EventType eventType)
		{
			if (eventListenerList.ContainsKey(eventType))
			{
				//得到监听者列表
				List<EventListener> listenerList = eventListenerList[eventType];
				for (int i = 0; i < listenerList.Count; i++)
				{
					listenerList[i]?.CallBack();
				}
			}
		}

		/// <summary>
		/// 删除事件指定接收者  有问题待修改
		/// </summary>
		/// <param name="eventType"></param>
		/// <param name="listener"></param>
		public void RemoveEventListener(EventType eventType,object listener)
		{
			if (eventListenerList.ContainsKey(eventType))
			{
				//得到监听者列表
				List<EventListener> listenerList = eventListenerList[eventType];
				for (int i = 0; i < listenerList.Count; i++)
				{
					if (listenerList[i].Listener == listener)
					{
						listenerList.RemoveAt(i);
						return;
					}
				}
			}
		}

		/// <summary>
		/// 删除指定事件类型
		/// </summary>
		/// <param name="eventType"></param>
		public void RemoveEvent(EventType eventType)
		{
			if (eventListenerList.ContainsKey(eventType))
			{
				eventListenerList.Remove(eventType);
			}
		}

		/// <summary>
		/// 清空所有事件
		/// </summary>
		public void Clear()
		{
			eventListenerList.Clear();
		}

		// 删除指定接收者所有事件
		// 删除指定事件类型
		// 删除指定事件类型的事件回调
		// 发送事件
		// 向指定接收者发送事件
		// 清空所有事件
	}
}
