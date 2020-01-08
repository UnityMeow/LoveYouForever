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
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LoveYouForever
{
	public class TestScript : MonoBehaviour
	{
		private Sheet1 data;
		private void Start()
		{
			var loader = new AssetLoader();
			// 添加资源配置
			loader.AddLoadConfig(new AssetLoader.AssetLoadConfig[]
			{
				new AssetLoader.AssetLoadConfig{type = typeof(Sprite),Label = "Test"},
				new AssetLoader.AssetLoadConfig{type = typeof(GameObject),Label = "UI"},
				new AssetLoader.AssetLoadConfig{type = typeof(ScriptableObject),Label = "Excel"},
			});
			// 开始加载yiy
			loader.StartLoad(onCompleted);
		}

		/// <summary>
		/// 加载结束回调
		/// </summary>
		private void onCompleted()
		{
			UIManager.Instance.ShowPanel<UITest>("TestMain","TestMainPanel");
			GetComponent<SpriteRenderer>().sprite = AssetManager.GetAsset<Sprite>("123");
			data = ExcelDataManager.GetData<Sheet1>("Sheet1");
			Debug.Log(data.dataArray[2].Name);
		}
		
		private void Update()
		{
			
		}
	}
}
