#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	TestAnim
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
	public class TestAnim : MeowAnimation
	{
		protected override void Awake()
		{
			base.Awake();
			Invoke("test",1.0f);
		}

		void test()
		{
			SetDefaultAnim("Hero/attack");
			PlayDefaultAnim(); 
		}
	}
}
