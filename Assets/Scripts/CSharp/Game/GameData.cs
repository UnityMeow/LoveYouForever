#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	GameData
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
	public class GameData
	{
		public string GameSceneName = "GamePrefabs/TestGameScene";

		private string season;
		/// <summary>
		/// 当前季节
		/// </summary>
		public string CurSeason
		{
			set
			{
				season = value;
				EventManager.Instance.SendEvent(EventType.Season);
			}
			
			get => season;
		}
	}
}
