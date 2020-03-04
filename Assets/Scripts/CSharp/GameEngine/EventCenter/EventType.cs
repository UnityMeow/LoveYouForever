#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	EventType
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
	/// <summary>
	/// 事件类型
	/// </summary>
	public enum EventType
	{
		/// <summary>
		/// 游戏初始
		/// </summary>
		GameInit = 1,
		/// <summary>
		/// 游戏运行
		/// </summary>
		GameRun = 2,
		/// <summary>
		/// 游戏暂停
		/// </summary>
		GamePause = 3,
		
		/// <summary>
		/// 季节变换
		/// </summary>
		Season = 10,
		
		/// <summary>
		/// 加载界面
		/// </summary>
		UILoading = 1001,
		/// <summary>
		/// 主界面
		/// </summary>
		UIMain = 1002,
		/// <summary>
		/// 游戏界面
		/// </summary>
		UIGame = 1003,
		/// <summary>
		/// 留言板界面
		/// </summary>
		UIGuestBook = 1004,
		/// <summary>
		/// 设置界面
		/// </summary>
		UISetting = 1005,
		/// <summary>
		/// 暂停界面
		/// </summary>
		UIPause = 1006,
		/// <summary>
		/// 记忆碎片界面
		/// </summary>
		UIMemento = 1007,
	}
}
