#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	MusicManager
** 版  权:	(C)  
** 创建人:  Unity喵
** 日  期:	2019.11.13
** 描  述: 	音频管理器
**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
*******************************************************************/
#endregion
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoveYouForever
{
	public class MusicManager : Single<MusicManager>
    {
        string bgmPath = "";
        string soundPath = "";
        //string soundBagName = "";
        //string bgmBagName = "";
        AudioSource bgmAS;
        Dictionary<string, AudioSource> soundList = new Dictionary<string, AudioSource>();

        /// <summary>
        /// 改变背景音乐大小
        /// </summary>
        /// <param name="value"></param>
        public void ChangeBGMValue(float value)
        {
            if (bgmAS == null)
                return;
            bgmAS.volume = value;
        }
        
        /// <summary>
        /// 改变音效音乐大小
        /// </summary>
        /// <param name="value"></param>
        public void ChangeSoundValue(float value)
        {
            if (soundList.Count == 0)
                return;
            Dictionary<string, AudioSource>.Enumerator enumerator = soundList.GetEnumerator();
            while (enumerator.MoveNext())
            {
                soundList[enumerator.Current.Key].volume = value;
            }
        }
        
        /// <summary>
        /// 播放背景音乐
        /// </summary>
        /// <param name="name"></param>
        public void PlayBGM(string name)
        {
            if (bgmAS == null)
            {
                GameObject BGM = new GameObject("BGM");
                GameObject.DontDestroyOnLoad(BGM);
                bgmAS = BGM.AddComponent<AudioSource>();
                bgmAS.loop = true;
            }
            if (bgmAS.isPlaying)
                bgmAS.Stop();
            // TODO: 待修改
            bgmAS.clip = AssetManager.GetAsset<AudioClip>(bgmPath + name);
            bgmAS.Play();
        }
        
        /// <summary>
        /// 停止背景音乐
        /// </summary>
        public void StopBGM()
        {
            if (bgmAS != null && bgmAS.isPlaying)
                bgmAS.Stop();
        }
        
        /// <summary>
        /// 暂停背景音乐
        /// </summary>
        public void PauseBGM()
        {
            if (bgmAS != null && bgmAS.isPlaying)
                bgmAS.Pause();
        }
        
        /// <summary>
        /// 播放音效音乐
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isLoop"></param>
        public void PlaySound(string name, bool isLoop = false)
        {
            if (!soundList.ContainsKey(name))
            {
                if (GameObject.Find("Sound") == null)
                {
                    GameObject go = new GameObject("Sound");
                    GameObject.DontDestroyOnLoad(go);
                }
                AudioSource tmp = GameObject.Find("Sound").AddComponent<AudioSource>();
                // TODO：待修改
                tmp.clip = AssetManager.GetAsset<AudioClip>(soundPath + name);
                tmp.name = name;
                soundList.Add(name, tmp);
            }
            soundList[name].loop = isLoop;
            soundList[name].Play();
        }
        
        /// <summary>
        /// 停止音效音乐
        /// </summary>
        /// <param name="name"></param>
        public void StopSound(string name)
        {
            if (!soundList.ContainsKey(name))
                return;
            if (soundList[name].isPlaying)
                soundList[name].Stop();
        }
        
        /// <summary>
        /// 停止所有音效
        /// </summary>
        public void StopAllSound()
        {
            Dictionary<string, AudioSource>.Enumerator enumerator = soundList.GetEnumerator();
            while (enumerator.MoveNext())
            {
                soundList[enumerator.Current.Key].Stop();
            }
        }
    }
}
