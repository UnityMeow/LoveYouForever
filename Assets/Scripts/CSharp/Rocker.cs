#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	Rocker
** 版  权:	(C)  
** 创建人:  Unity喵
** 日  期:	
** 描  述: 	
*******************************************************************/
#endregion
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LoveYouForever
{
	public class Rocker : MonoBehaviour
	{
		/// <summary>
		/// 触摸球
		/// </summary>
		Transform ball;
		/// <summary>
		/// 重置位置
		/// </summary>
		Vector3 zeroPos;
		/// <summary>
		/// 半径
		/// </summary>
		float radius;
		
		void Start()
		{
			radius = 150;
			ball = transform.Find("ball");
			zeroPos = transform.position;
		}
		
		/// <summary>
		/// 拖拽
		/// </summary>
		/// <param name="data"></param>
		public void RDrag(BaseEventData data)
		{
			PointerEventData eventData = data as PointerEventData;
			// 鼠标位置
			Vector2 MousePosition;
			// 转换鼠标位置
			RectTransformUtility.ScreenPointToLocalPointInRectangle
			(
				// 底图的RectTransform
				transform as RectTransform,
				// 点击位置
				eventData.position,
				// 触发事件的相机
				eventData.pressEventCamera,
				// 返回转换后的Vector2
				out MousePosition
			);
			// 更改触摸球位置
			ball.localPosition = MousePosition;
			// Vector2转换转Vector3
			Vector3 pos = MousePosition;
			// 模长大于半径
			if(pos.magnitude > radius)
			{
				// 改变底图位置 = 底图位置+(鼠标位置-鼠标位置单位向量*半径)
				transform.position += pos - pos.normalized * radius;
			}
			// 根据鼠标位置向量进行英雄移动  事件传递
			// pos 为位置向量
		}
		
		/// <summary>
		/// 抬起
		/// </summary>
		/// <param name="data"></param>
		public void RUp(BaseEventData data)
		{
			// 摇杆位置重置
			ball.localPosition = Vector3.zero;
			transform.position = zeroPos;
			// 停止移动 事件传递
			
		}
	}
}
