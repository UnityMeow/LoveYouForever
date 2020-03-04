#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	Bootstrap
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
	public class Bootstrap : GameEntry
    {
        protected override void OnInit()
        {
            base.OnInit();
            //注册信息
        }

        protected override void OnStart()
        {
            base.OnStart();
            DontDestroyOnLoad(this);
            startGame();
        }

        private void startGame()
        {
            var loader = new AssetLoader();
            // 添加资源配置
            loader.AddLoadConfig(new AssetLoader.AssetLoadConfig[]
            {
                new AssetLoader.AssetLoadConfig{type = typeof(Sprite),Label = "Test"},
                new AssetLoader.AssetLoadConfig{type = typeof(GameObject),Label = "UI"},
            });
            // 开始加载资源
            loader.StartLoad(onCompleted);
        }
        
        /// <summary>
        /// 加载结束回调
        /// </summary>
        private void onCompleted()
        {
            UIController.Instance.Init();
            Debug.Log("准备显示Loading界面");
            EventManager.Instance.SendEvent(EventType.UILoading);
        }
    }
}
