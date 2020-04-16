using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoveYouForever
{
    
    
    public class BackgroundController: Single<BackgroundController>
    {
        private struct BGData
        {
            /// <summary>
            /// 速度
            /// </summary>
            public float Speed;

            /// <summary>
            /// 图片名
            /// </summary>
            public string ImageName;

            /// <summary>
            /// 图片宽
            /// </summary>
            public float Width;
            
            /// <summary>
            /// 路径
            /// </summary>
            public string Path;
            
            /// <summary>
            /// 重置位置
            /// </summary>
            public Vector2 ResetPos;

            /// <summary>
            /// 背景
            /// </summary>
            public Transform[] Background;
        }
        
        public GameObject GO;

        private BGData[] backgroundData =
        {
            new BGData{Speed = 2.8f,ImageName = "Road", Path = "TestGameScene/Background/Third/", Background = new Transform[3]}, 
            new BGData{Speed = 0.8f,ImageName = "Mountain", Path = "TestGameScene/Background/First/", Background = new Transform[3]}, 
            new BGData{Speed = 1.2f,ImageName = "Cloud", Path = "TestGameScene/Background/First/", Background = new Transform[3]}, 
            new BGData{Speed = 1.7f,ImageName = "Tree", Path = "TestGameScene/Background/Second/", Background = new Transform[3]}, 
            new BGData{Speed = 1.7f,ImageName = "Cake", Path = "TestGameScene/Background/Second/", Background = new Transform[3]}, 
        };

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            for (int i = 0; i < backgroundData.Length; i++)
            {
                GO = GameObject.Find(backgroundData[i].Path + backgroundData[i].ImageName);
                for (int j = 0; j < backgroundData[i].Background.Length; j++)
                {
                    backgroundData[i].Background[j] = GO.transform.GetChild(j);
                }
                backgroundData[i].Width = backgroundData[i].Background[0].GetComponent<BoxCollider2D>().bounds.size.x;
                backgroundData[i].ResetPos = 
                    new Vector2(backgroundData[i].Width * (backgroundData[i].Background.Length - 1),0);
                for (int j = 0; j < backgroundData[i].Background.Length; j++)
                {
                    backgroundData[i].Background[j].localPosition = new Vector3(j * backgroundData[i].Width,0,0);
                    Debug.Log(backgroundData[i].Background[j].localPosition);
                }
            }

            EventManager.Instance.Add(EventType.Season,this,onEventChange);
            // TODO: 待修改 由英雄移动脚本来控制
            EventManager.Instance.Add(EventType.HeroMove,this,Move);
        }

        /// <summary>
        /// 背景移动
        /// </summary>
        public void Move(object data)
        {
            for (int j = 0; j < backgroundData.Length; j++)
            {
                for (int i = 0; i < backgroundData[j].Background.Length; i++)
                {
                    backgroundData[j].Background[i].Translate(backgroundData[j].Speed * Time.deltaTime * Vector2.left);
                    if (backgroundData[j].Background[i].localPosition.x <= -backgroundData[j].Width)
                    {
                        backgroundData[j].Background[i].localPosition = backgroundData[j].ResetPos + (backgroundData[j].Speed * Time.deltaTime * Vector2.left);
                    }
                }
            }
        }

        /// <summary>
        /// 背景切换
        /// </summary>
        public void onEventChange(object eventData)
        {
            
        }

    }
}
