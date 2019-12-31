#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	GameEnry
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
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace LoveYouForever
{
    /// <summary>
    /// 游戏启动程序
    /// </summary>
	public class GameEnry : MonoBehaviour
    {
        // 待修改
        public GameEnry Instance { get; private set; }
        private void Awake()
        {
            Instance = this;
            OnInit();
        }

        private void Start()
        {
            OnStart();
        }

        protected virtual void OnInit()
        { }

        protected virtual void OnStart()
        { }

        public void StartGame()
        {
            // 资源加载完成
            // 显示主页界面
        }
    }
}
