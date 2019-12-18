#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	UIBase
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
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LoveYouForever
{
	public class UIBase : MonoBehaviour
	{
        /// <summary>
        /// 显示类型
        /// </summary>
        public enum ShowTypeEnum
        {
            /// <summary>
            /// 正常
            /// </summary>
            Normal = 0,
            /// <summary>
            /// 渐隐渐显
            /// </summary>
            Fade,
            /// <summary>
            /// 遮罩
            /// </summary>
            Mask,
        }

        /// <summary>
        /// 当前面板显示类型 
        /// </summary>
        public ShowTypeEnum showType;

        /// <summary>
        /// 面板物品
        /// </summary>
        Dictionary<string, GameObject> itemList;

        public virtual void Init()
        {
            itemList = new Dictionary<string, GameObject>();
            showType = ShowTypeEnum.Normal;
            List<Transform> list = new List<Transform>();
            FindChild(transform, list);
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].CompareTag("Event"))
                {
                    EventTrigger ET = list[i].gameObject.AddComponent<EventTrigger>();
                    if (ET.triggers.Count == 0)
                    {
                        ET.triggers = new List<EventTrigger.Entry>();
                    }
                    itemList.Add(list[i].name, list[i].gameObject);
                }
                else if (list[i].CompareTag("UIGO"))
                {
                    itemList.Add(list[i].name, list[i].gameObject);
                }
            }
        }

        /// <summary>
        /// 显示面板
        /// </summary>
        public virtual void Show()
        { }

        /// <summary>
        /// 隐藏面板
        /// </summary>
        public virtual void Hide()
        { }

        /// <summary>
        /// 销毁面板
        /// </summary>
        public virtual void Destroy()
        { }

        /// <summary>
        /// 获取面板控件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        protected T GetControl<T>(string name)
        where T : MonoBehaviour
        {
            if (itemList[name] == null)
                return null;
            return itemList[name].GetComponent<T>();
        }

        /// <summary>
        /// 获取面板物体
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public GameObject GetGameObject(string name)
        {
            if (itemList[name] == null)
                return null;
            return itemList[name];
        }

        /// <summary>
        /// 添加Event事件
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="type"></param>
        /// <param name="callBack"></param>
        public void AddEventTrigger(string controlName, EventTriggerType type, UnityAction<BaseEventData> callBack)
        {
            if (!itemList.ContainsKey(controlName))
                return;
            if (itemList[controlName].gameObject.GetComponent<EventTrigger>() == null)
                itemList[controlName].AddComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.callback = new EventTrigger.TriggerEvent();
            entry.callback.AddListener(callBack);
            entry.eventID = type;
            itemList[controlName].GetComponent<EventTrigger>().triggers.Add(entry);
        }

        /// <summary>
        /// 得到所有子物体
        /// </summary>
        /// <param name="father"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        List<Transform> FindChild(Transform father, List<Transform> list)
        {
            if (father.childCount == 0)
                return list;
            int len = father.childCount;
            for (int i = 0; i < len; i++)
            {
                list.Add(father.GetChild(i));
                FindChild(father.GetChild(i), list);
            }
            return list;
        }
    }
}
