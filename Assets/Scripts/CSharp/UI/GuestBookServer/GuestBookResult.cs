#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	GuestBookResult
** 版  权:	(C)  
** 创建人:  Unity喵
** 日  期:	
** 描  述: 	
*******************************************************************/
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoveYouForever
{
	/// <summary>
	/// 服务器通用留言板数据
	/// </summary>
	public struct GuestBookCommonData
	{
		/// <summary>
		/// 所在场景名
		/// </summary>
		public string scene;
		/// <summary>
		/// 时间线
		/// </summary>
		public float time;
		/// <summary>
		/// 弹幕文本
		/// </summary>
		public string text;
	}
	
	/// <summary>
	/// 留言板数据
	/// </summary>
	public struct GuestBookData
	{
		/// <summary>
		/// 弹幕文本
		/// </summary>
		public string text;
		
		/// <summary>
		/// 出现次数
		/// </summary>
		public int count;
	}
	
	public class Result
	{
		/// <summary>
		/// 当前加载状态
		/// </summary>
		public string status;
	}
	
	/// <summary>
	/// 移除留言板数据
	/// </summary>
	public class RemoveData
	{
		/// <summary>
		/// 所在场景
		/// </summary>
		public string scene;
		
		/// <summary>
		/// 弹幕文本
		/// </summary>
		public string text;
		
		/// <summary>
		/// 密码
		/// </summary>
		public string password;
	}

	/// <summary>
	/// 清空留言板数据
	/// </summary>
	public struct ClearData
	{
		/// <summary>
		/// 所在场景
		/// </summary>
		public string scene;

		/// <summary>
		/// 密码
		/// </summary>
		public string password;
	}

	/// <summary>
	/// 留言板数据集合
	/// </summary>
	public class GuestBookResult : Result
	{
		public GuestBookData[] list;
	}
}
