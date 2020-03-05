#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	UIDataGuestBook
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
	public class UIDataGuestBook : InstanceNull<UIDataGuestBook>
	{
		/// <summary>
		/// 留言板数据
		/// </summary>
		public List<GuestBookData> Datas => guestBookDatas;
		private List<GuestBookData> guestBookDatas = new List<GuestBookData>();

		/// <summary>
		/// 弹幕行数
		/// </summary>
		public static int Row = 8;

		/// <summary>
		/// 行间隔
		/// </summary>
		public static float RowSpace = 60;

		/// <summary>
		/// 当前弹幕下标
		/// </summary>
		public static int CurIndex = 0;

		/// <summary>
		/// 移动速度
		/// </summary>
		public static float Speed = 70;

		/// <summary>
		/// 列间隔
		/// </summary>
		public static float ColumnSpace = 40;

		/// <summary>
		/// 弹幕开关
		/// </summary>
		public static bool GuestBookOn;

		/// <summary>
		/// 留言板数据初始化
		/// </summary>
		public void Init()
		{
			GetGuestBookData();
		}

		/// <summary>
		/// 获取留言板数据
		/// 感觉有问题
		/// </summary>
		/// <returns></returns>
		private void GetGuestBookData()
		{
			guestBookDatas.Clear();
			GuestBookServer.Instance.ListCountedDanmakus(
				result =>
				{
					if (result.status == "ok")
					{
						guestBookDatas.AddRange(result.list);
						EventManager.Instance.SendEvent(EventType.UIGuestBook);
						// TODO: 测试代码待删除
						foreach (var guestBookData in result.list)
						{
							Debug.Log($"text: {guestBookData.text}, count: {guestBookData.count}");
						}
					}
				}, 
				(url, responseCode, error) =>
				{
					Debug.Log($"url: {url}, responseCode: {responseCode}, error: {error}");
				});
		}

		/// <summary>
		/// 添加留言板数据
		/// </summary>
		public void AddGuestBookData(string text)
		{
			GuestBookCommonData commonData = new GuestBookCommonData();
			commonData.text = text;
			commonData.time = 0;
			commonData.scene = "welcome";
			GuestBookServer.Instance.AddDanamku(commonData, result =>
			{
				if (result.status == "ok")
				{
					// 发送添加成功事件
					EventManager.Instance.SendEvent(EventType.InputSucceed);
				}
			}, null);
		}
	}
}
