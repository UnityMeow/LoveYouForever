#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	UITest
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
	public class UITest : UIBase
	{
		public Text text;
		public override void Init()
		{
			base.Init();
			text = GetControl<Text>("Text");
		}

	}
}
