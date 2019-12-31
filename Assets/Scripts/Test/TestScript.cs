#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	TestScript
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
	public class TestScript : MonoBehaviour
	{
		private void Start()
		{
			var loader = new AssetLoader();
			// 添加资源配置
			loader.AddLoadConfig(new AssetLoader.AssetLoadConfig[]
			{
				new AssetLoader.AssetLoadConfig{type = typeof(Texture),Label = ""}, 
				new AssetLoader.AssetLoadConfig{type = typeof(GameObject),Label = ""}, 
			});
			// 开始加载
			loader.StartLoad(onCompleted);
		}

		/// <summary>
		/// 加载结束回调
		/// </summary>
		private void onCompleted()
		{
			AssetManager.GetAsset<Texture>("");
		}

		private void Update()
		{
			
		}
	}
}
