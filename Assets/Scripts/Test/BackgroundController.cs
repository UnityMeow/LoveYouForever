using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LoveYouForever
{
    public class BackgroundController: InstanceNull<BackgroundController>
    {
        /// <summary>
        /// 移动速度
        /// </summary>
        private float speed = 10;

        /// <summary>
        /// 移动方向
        /// </summary>
        private Vector2 direction;

        /// <summary>
        /// 重置位置
        /// </summary>
        private Vector2 resetPos;

        /// <summary>
        /// 图片宽
        /// </summary>
        private float width;

        /// <summary>
        /// 当前图片名
        /// </summary>
        private string curImageName;
        
        /// <summary>
        /// 背景
        /// </summary>
        private Transform[] background = new Transform[2];

        public GameObject GO;

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            GO = GameObject.Find("TestGameScene/Background");
            for (int i = 0; i < background.Length; i++)
            {
                background[i] = GO.transform.GetChild(i);
            }
            
            direction = Vector2.left;
            
            width = background[0].GetComponent<BoxCollider2D>().bounds.size.x;
            
            resetPos = new Vector2(width,0);
            
            // 初始化位置
            for (int i = 0; i < background.Length; i++)
            {
                background[i].localPosition = new Vector3(i * width,0,0);
            }

            // TODO: 待修改 后期直接在按键触发
            GlobalMonoManager.Instance.AddFixedUpdateListener(Move);
        }

        /// <summary>
        /// 背景移动
        /// </summary>
        public void Move()
        {
            for (int i = 0; i < 2; i++)
            {
                background[i].Translate(speed * Time.deltaTime * direction);
                if (background[i].localPosition.x <= -width)
                {
                    background[i].localPosition = resetPos + (speed * Time.deltaTime * direction);
                }
            }
        }

        /// <summary>
        /// 背景切换
        /// </summary>
        public void Change(string name)
        {

        }

    }
}
