#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	UIController
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
	public enum UIState
	{
		/// <summary>
		/// 加载界面
		/// </summary>
		UILoading,
		/// <summary>
		/// 主界面
		/// </summary>
		UIMain,
		/// <summary>
		/// 游戏界面
		/// </summary>
		UIGame,
		/// <summary>
		/// 留言板界面
		/// </summary>
		UIGuestBook,
		/// <summary>
		/// 设置界面
		/// </summary>
		UISetting,
		/// <summary>
		/// 暂停界面
		/// </summary>
		UIPause,
		/// <summary>
		/// 记忆碎片界面
		/// </summary>
		UIMemento,
	}

	public class UIController : InstanceNull<UIController>
	{
		
		/// <summary>
		/// 当前游戏状态
		/// </summary>
		public UIState CurState;

		public void Init()
		{
			EventManager.Instance.Add(EventType.UILoading,this,onEventUILoading);
			EventManager.Instance.Add(EventType.UIMain,this,onEventUIMain);
			EventManager.Instance.Add(EventType.UIGame,this,onEventUIGame);
			EventManager.Instance.Add(EventType.UIGuestBook,this,onEventUIGuestBook);
			EventManager.Instance.Add(EventType.UISetting,this,onEventUISetting);
			EventManager.Instance.Add(EventType.UIPause,this,onEventUIPause);
			EventManager.Instance.Add(EventType.UIMemento,this,onEventUIMemento);
			EventManager.Instance.Add(EventType.GuestBookData,this,onEventGuestBookData);
		}

		private void onEventGuestBookData()
		{
			UIDataGuestBook.Instance.Init();
		}

		private void onEventUILoading()
		{
			CurState = UIState.UILoading;
			UIManager.Instance.ShowPanel<UILoading>("LoadingUI","LoadingUI");
		}
		
		private void onEventUIMain()
		{
			CurState = UIState.UIMain;
			UIManager.Instance.ShowPanel<UIMain>("MainUI", "MainUI");
		}
		
		private void onEventUIGame()
		{
			CurState = UIState.UIGame;
			UIManager.Instance.ShowPanel<UIGame>("GameUI", "GameUI");
		}
		
		private void onEventUIGuestBook()
		{
			CurState = UIState.UIGuestBook;
			UIManager.Instance.ShowPanel<UIGuestBook>("GuestBookUI", "GuestBookUI");
		}
		
		private void onEventUISetting()
		{
			CurState = UIState.UISetting;
		}
		
		private void onEventUIPause()
		{
			CurState = UIState.UIPause;
		}
		
		private void onEventUIMemento()
		{
			CurState = UIState.UIMemento;
		}
		
	}
}
