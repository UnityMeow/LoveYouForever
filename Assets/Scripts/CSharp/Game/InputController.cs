#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	InputController
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
	public class InputController : SingleMono<InputController>
	{
		public void Init()
		{
		}

		public void Update()
		{
			if (Input.GetKey(KeyCode.W))
			{
				EventManager.Instance.SendEvent(EventType.HeroMove,1);
			}
			else if (Input.GetKey(KeyCode.S))
			{
				EventManager.Instance.SendEvent(EventType.HeroMove,2);
			}
			if (Input.GetKey(KeyCode.A))
			{
				EventManager.Instance.SendEvent(EventType.HeroMove,3);
			}
			else if (Input.GetKey(KeyCode.D))
			{
				EventManager.Instance.SendEvent(EventType.HeroMove,4);
			}
		}
	}
}
