#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	UIGame
** 版  权:	(C)  
** 创建人:  Unity喵
** 日  期:	
** 描  述: 	
*******************************************************************/
#endregion
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LoveYouForever
{
	public class UIGame : UIBase
	{
		public override void Init()
		{
			base.Init();
			showType = ShowType.Normal;
			GetControl<Button>("ButtonX").onClick.AddListener(()=>GameManager.Instance.CameraShake());
		}
	}
}
