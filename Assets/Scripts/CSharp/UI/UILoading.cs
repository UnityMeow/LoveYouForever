#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	UILoading
** 版  权:	(C)  
** 创建人:  Unity喵
** 日  期:	
** 描  述: 	
*******************************************************************/
#endregion
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LoveYouForever
{
	public class UILoading : UIBase
    {
        public override void Init()
        {
            base.Init();
            showType = ShowType.Normal;
            var loader = new AssetLoader();
            // 添加资源配置
            loader.AddLoadConfig(new AssetLoader.AssetLoadConfig[]
            {
                new AssetLoader.AssetLoadConfig{type = typeof(Sprite),Label = "Test"},
                new AssetLoader.AssetLoadConfig{type = typeof(GameObject),Label = "UI"},
                new AssetLoader.AssetLoadConfig{type = typeof(ScriptableObject),Label = "Excel"},
                new AssetLoader.AssetLoadConfig{type = typeof(Texture2D),Label = "AnimPic"},
                new AssetLoader.AssetLoadConfig{type = typeof(TextAsset),Label = "AnimData"},
                new AssetLoader.AssetLoadConfig{type = typeof(GameObject),Label = "Game"},
                new AssetLoader.AssetLoadConfig{type = typeof(ScriptableObject),Label = "Configs"},
            });
            // 开始加载资源
            loader.StartLoad(onCompleted);
        }

        public override void Show()
        {
            base.Show();
        }

        private void onCompleted()
        {
            Debug.Log("资源加载成功");
            showType = ShowType.Fade;
            Hide(()=>EventManager.Instance.SendEvent(EventType.UIMain));
            GameManager.Instance.Init();
            Debug.Log("Configs初始化");
            ConfigsManager.Instance.Load();
        }
    }
}
