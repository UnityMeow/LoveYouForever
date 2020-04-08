#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	AnimSpriteManager
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

namespace LoveYouForever
{
    public class AnimSpriteManager : Single<AnimSpriteManager>
    {
        /// <summary>
        /// 图片数据集合
        /// </summary>
        struct PicData
        {
            public int x;
            public int y;
            public int w;
            public int h;
            public int offx;
            public int offy;
        }

        /// <summary>
        /// 数据文件主路径
        /// </summary>
        string dataMainPath;

        /// <summary>
        /// 图片文件主路径
        /// </summary>
        string textureMainPath;

        /// <summary>
        /// 2D资源表
        /// </summary>
        Dictionary<string, Sprite[]> spriteList;

        public AnimSpriteManager()
        {
            spriteList = new Dictionary<string, Sprite[]>();
        }

        /// <summary>
        /// 初始化2D资源相关主路径
        /// </summary>
        /// <param name="mainPath"></param>
        public void Init(string mainPath)
        {
            dataMainPath = mainPath + "/data/";
            textureMainPath = mainPath + "/pic/";
        }

        /* 
            加载动画图集 
            规则：
                bytes放主路径下的data文件夹
                png放主路径下的pic文件夹
        */
        public Sprite[] LoadAnim(string name)
        {
            spriteList.Add(name,
                    LoadSprite(textureMainPath + name, dataMainPath + name));
            return spriteList[name];
        }

        public void RemoveAnim(string name)
        {
            if (!spriteList.ContainsKey(name))
                return;
            spriteList.Remove(name);
        }

        public void ClearAnim()
        {
            spriteList.Clear();
        }
        
        /// <summary>
        /// 精灵资源加载（资源key，数据key）
        /// </summary>
        /// <param name="textureName"></param>
        /// <param name="dataName"></param>
        /// <returns></returns>
        Sprite[] LoadSprite(string textureName,string dataName)
        {
            // 加载图片
            Texture2D texture = AssetManager.GetAsset<Texture2D>(textureName + ".png");
            // 加载数据
            TextAsset data = AssetManager.GetAsset<TextAsset>(dataName + ".bytes");
            Debug.Log(dataName + ".bytes");
            int index = 0;
            int len = BitConverter.ToInt32(data.bytes, index);
            index += 4;
            // 数据集合
            PicData[] pic_data = new PicData[len];
            // 图片集合
            Sprite[] pic = new Sprite[len];
            // 取数据
            for (int i = 0; i < len; i++)
            {
                pic_data[i].x = BitConverter.ToInt32(data.bytes, index);
                index += 4;
                pic_data[i].y = BitConverter.ToInt32(data.bytes, index);
                index += 4;
                pic_data[i].w = BitConverter.ToInt32(data.bytes, index);
                index += 4;
                pic_data[i].h = BitConverter.ToInt32(data.bytes, index);
                index += 4;
                pic_data[i].offx = BitConverter.ToInt32(data.bytes, index);
                index += 4;
                pic_data[i].offy = BitConverter.ToInt32(data.bytes, index);
                index += 4;
                pic_data[i].y = texture.height - pic_data[i].y - pic_data[i].h;
            }
 
            // 裁图片
            for (int i = 0; i < len; i++)
            {
                pic[i] = Sprite.Create(texture, new Rect(pic_data[i].x, pic_data[i].y, pic_data[i].w, pic_data[i].h),
                    new Vector2(pic_data[i].offx / pic_data[i].w, pic_data[i].offy / pic_data[i].h));
            }

            // 返回裁剪好的精灵图组
            return pic;
        }
    }
}
