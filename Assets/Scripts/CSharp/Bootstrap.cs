#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	Bootstrap
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
	public class Bootstrap : GameEnry
    {
        protected override void OnInit()
        {
            base.OnInit();
            //注册信息
        }

        protected override void OnStart()
        {
            base.OnStart();
            DontDestroyOnLoad(this);
            //Loading资源加载
            //UIManager.Instance.ShowPanel<UILoading>("", "");
        }
    }
}
