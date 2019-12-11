#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	EventCenter
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
	public class EventCenter : InstanceNull<EventCenter>
    {
        //事件监听器表
        Dictionary<string, List<EventListener>> m_eventListenerList = new Dictionary<string, List<EventListener>>();
        //清空事件中心
        public void ClearEventCenter()
        {
            m_eventListenerList.Clear();
        }
        //添加事件监听
        public void AddEventListener(string eventType, object listener, EventCenterFunction callback)
        {
            if (listener == null || eventType == null)
                return;
            //记录事件
            AddEvent(eventType);
            //添加事件监听器
            List<EventListener> list = m_eventListenerList[eventType];
            EventListener e = new EventListener(listener, callback);
            list.Add(e);
        }
        //删除事件监听
        public void RemoveEventListener(string eventType, object listener)
        { }
        //清空事件监听
        public void ClearEventListener(object listener)
        { }
        //派发事件(事件类型，附带数据可传可不传)
        public void DispatchEvent(string eventType, object data = null)
        {
            //检测事件是否存在
            if (!CheckIsHaveEvent(eventType))
                return;
            //得到监听者表
            List<EventListener> listenerList = m_eventListenerList[eventType];
            for (int i = 0; i < listenerList.Count; ++i)
            {
                EventListener listenerObj = listenerList[i];
                if (listenerObj.listener != null)
                {
                    listenerObj.function();
                }
            }
        }
        //检查事件是否存在
        bool CheckIsHaveEvent(string eventType)
        {
            if (m_eventListenerList.ContainsKey(eventType))
                return true;
            return false;
        }
        //添加事件
        bool AddEvent(string eventType)
        {
            //检测事件是否存在
            if (!CheckIsHaveEvent(eventType))
            {
                //记录事件
                m_eventListenerList.Add(eventType, new List<EventListener>());
            }
            return true;
        }
        //删除事件
        void RemoveEvent(string eventType)
        {
            //检测事件是否存在
            if (!CheckIsHaveEvent(eventType))
                return;
            //移除事件
            m_eventListenerList.Remove(eventType);
        }
    }
}
