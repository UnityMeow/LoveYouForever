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
using DG.Tweening;
using LoveYouForever;
using UnityEngine;
using EventType = LoveYouForever.EventType;

public class GameManager : Single<GameManager>
{
    public enum GameState
    {
        /// <summary>
        /// 游戏初始化
        /// </summary>
        Init,
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
    /// 游戏画面主相机
    /// </summary>
    public Camera MainCamera;

    /// <summary>
    /// 当前游戏数据
    /// </summary>
    public GameData CurData;

    public GameManager()
    {
        Debug.Log("注册游戏初始事件");
        EventManager.Instance.Add(EventType.GameInit,this,onEventGameStart);
        EventManager.Instance.Add(EventType.GameRun,this,onEventGameRun);
        CurData = new GameData();
    }

    
    /// <summary>
    /// 暂时保留
    /// </summary>
    public void Init()
    {
    }
    

    /// <summary>
    /// 游戏启动
    /// </summary>
    private void onEventGameStart(object eventData)
    {
        Debug.Log("游戏启动");
        CurState = GameState.Init;
        // TODO: 游戏初始化
        
        // 根据游戏数据加载游戏场景Prefab
        var go = AssetManager.LoadPrefab(CurData.GameSceneName);
        go.name = "TestGameScene";
        // 初始化背景
        BackgroundController.Instance.Init();
        EventManager.Instance.SendEvent(EventType.GameRun);
        
    }

    /// <summary>
    /// 游戏运行
    /// </summary>
    private void onEventGameRun(object eventData)
    {
        CurState = GameState.Run;
        EventManager.Instance.SendEvent(EventType.UIGame);
        
    }


    public void CameraShake(float duration = 0.25f)
    {
        MainCamera.DOShakePosition(duration,fadeOut:false,
            strength:0.08f,
            vibrato:30).SetEase(Ease.InQuint);
    }

}

