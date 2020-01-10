#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	AnimBase
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
    /// <summary>
    /// 有问题！ 需要重构
    /// </summary>
    public class AnimBase : MonoBehaviour
	{
    /// <summary>
    /// 默认动画精灵图组
    /// </summary>
    Sprite[] defaultAnim;
    
    /// <summary>
    /// 默认动画名
    /// </summary>
    string defaultName;
    
    /// <summary>
    /// 当前动画名
    /// </summary>
    string curAnim;
    
    /// <summary>
    /// 当前动画精灵图组
    /// </summary>
    Sprite[] curAnimSprite;
    
    /// <summary>
    /// 当前帧数
    /// </summary>
    int frame;
    
    /// <summary>
    /// FPS
    /// </summary>
    float fps;
    
    /// <summary>
    /// 是否循环
    /// </summary>
    bool isLoop;
    
    /// <summary>
    /// 是否强制播放
    /// </summary>
    bool isForced;
    
    /// <summary>
    /// 是否播放默认动画
    /// </summary>
    bool isDefault;
    
    /// <summary>
    /// 精灵渲染器
    /// </summary>
    /// 
    SpriteRenderer spriteRenderer;
    /// <summary>
    /// 计时器
    /// </summary>
    float clock;
    
    protected virtual void Awake()
    {
        frame = 0;
        fps = 0.1f;
        curAnim = "";
        AnimSpriteManager.Instance.Init("Animations");
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    
    protected virtual void Update()
    {
        clock += Time.deltaTime;
        if(clock > fps)
        {
            clock = 0.0f;
            
            if(curAnimSprite == null)
                return;
            if(frame >= curAnimSprite.Length)
            {
                if(isLoop)
                    frame = 0;
                else if(isDefault)
                {
                    if(defaultAnim == null)
                    {
                        GlobalUtil.Log("CAnimBase: 默认动画为空");
                        return;
                    }
                    curAnim = defaultName;
                    curAnimSprite = defaultAnim;
                    isLoop = true;
                    frame = 0;
                }
                else
                    frame = -1;
                isForced = false;
            }
            if(frame != -1)
                spriteRenderer.sprite = curAnimSprite[frame++];
        }
    }
    
    protected void SetFPS(int fps)
    {
        this.fps = 1f / fps;
    }
    
    /// <summary>
    /// 默认动画设置
    /// </summary>
    /// <param name="animName"></param>
    protected void SetDefaultAnim(string animName)
    {
        defaultName = animName;
        defaultAnim = AnimSpriteManager.Instance.LoadAnim(animName);
    }
    
    /// <summary>
    /// 取消默认动画
    /// </summary>
    protected void OffDefaultAnim()
    {
       defaultAnim = null; 
    }
    
    /// <summary>
    /// 播放默认动画
    /// </summary>
    protected void PlayDefaultAnim()
    {
        if(defaultAnim == null)
        {
            GlobalUtil.Log("CAnimBase: 默认动画为空");
            return;
        }
        curAnim = defaultName;
        curAnimSprite = defaultAnim;
        isLoop = true;
        frame = 0;
    }
    
    /// <summary>
    /// 切换动画(动画名，是否循环播放，是否播放完毕播放默认动画，是否重置动画)
    /// </summary>
    /// <param name="animName"></param>
    /// <param name="isLoop"></param>
    /// <param name="isDefault"></param>
    /// <param name="isForced"></param>
    /// <param name="reset"></param>
    protected void ChangeAnim(string animName, bool isLoop = false, bool isDefault = false, bool isForced = false, bool reset = false)
    {
        this.isDefault = isDefault;
        this.isLoop = isLoop;
        if(animName == curAnim)
        {
            if(reset)
                frame = 0;
            return;
        }
        if(!this.isForced)
        {
            curAnimSprite = AnimSpriteManager.Instance.LoadAnim(animName);
            curAnim = animName;
            frame = 0;
            this.isForced = isForced;
        }
    }
    
    
    protected bool IsPlayAnim(string name)
    {
        if(curAnim == name && frame != -1)
            return true;
        else
            return false;
    }
}
}
