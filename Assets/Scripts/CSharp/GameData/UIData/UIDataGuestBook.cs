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
		private List<GuestBookData> guestBookDatas = new List<GuestBookData>();
		
		/// <summary>
		/// 留言板数据初始化
		/// </summary>
		public void Init()
		{
			GetGuestBookData();
		}

		/// <summary>
		/// 获取留言板数据
		/// 感觉有问题 - -  待修改
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
				}
			}, null);
		}
	}
}
