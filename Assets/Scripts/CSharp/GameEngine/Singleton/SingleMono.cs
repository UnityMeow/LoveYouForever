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
	public class SingleMono<T> : MonoBehaviour
        where T : SingleMono<T>
    {
        static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject go = GameObject.Find("SingleMgrMono");
                    if (go == null)
                    {
                        go = new GameObject("SingleMgrMono");
                        DontDestroyOnLoad(go);
                    }
                    instance = go.AddComponent<T>();
                }
                return instance;
            }
        }
    }
}
