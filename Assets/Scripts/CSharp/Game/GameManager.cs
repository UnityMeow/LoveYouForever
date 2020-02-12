#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	GameManager
** 版  权:	(C)  
** 创建人:  Unity喵
** 日  期:	
** 描  述: 	
*******************************************************************/
#endregion
using System.Collections;
using System.Collections.Generic;
using LoveYouForever;
using UnityEngine;
using EventType = LoveYouForever.EventType;

public class GameManager : InstanceNull<GameManager>
{
    public enum GameState
    {
        /// <summary>
        /// 游戏开始
        /// </summary>
        Start,
        /// <summary>
        /// 游戏运行
        /// </summary>
        Run,
        /// <summary>
        /// 游戏暂停
        /// </summary>
        Pause,
        /// <summary>
        /// 游戏结束
        /// </summary>
        End,
        /// <summary>
        /// 游戏重置
        /// </summary>
        Reset,
    }

    /// <summary>
    /// 当前游戏状态
    /// </summary>
    public GameState CurState;

    /// <summary>
    /// 当前游戏数据
    /// </summary>
    public GameData CurData;

    public GameManager()
    {
        EventManager.Instance.Add(EventType.GameStart,this,onEventGameStart);
    }

    public void Init()
    {
        CurData = new GameData();
    }

    private void onEventGameStart()
    {
        Debug.Log("游戏启动");
        CurState = GameState.Start;
        // TODO: 游戏初始化
        
        // 根据游戏数据加载游戏场景Prefab
        var go = AssetManager.LoadPrefab(CurData.GameSceneName);
        go.name = "TestGameScene";
        // 初始化背景
        BackgroundController.Instance.Init();
    }

}

