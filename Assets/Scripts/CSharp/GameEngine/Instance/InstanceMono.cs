#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	InstanceMono
** 版  权:	(C)  
** 创建人:  Unity喵
** 日  期:	2019.11.13
** 描  述: 	继承Mono的单例基类
**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
*******************************************************************/
#endregion
using UnityEngine;

namespace LoveYouForever
{
	public class InstanceMono<T> : MonoBehaviour
        where T : InstanceMono<T>
    {
        static T m_instance;
        public static T Instance
        {
            get
            {
                if (m_instance == null)
                {
                    GameObject go = GameObject.Find("InstanceMgrMono");
                    if (go == null)
                    {
                        go = new GameObject("InstanceMgrMono");
                        DontDestroyOnLoad(go);
                    }
                    m_instance = go.AddComponent<T>();
                }
                return m_instance;
            }
        }
    }
}
