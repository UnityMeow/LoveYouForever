﻿#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	UIGuestBook
** 版  权:	(C)  
** 创建人:  Unity喵
** 日  期:	
** 描  述: 	
*******************************************************************/
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace LoveYouForever
{
	public class UIGuestBook : UIBase
	{
		/// <summary>
		/// 返回按钮
		/// </summary>
		private RectTransform backButton;
		
		/// <summary>
		/// 输入按钮
		/// </summary>
		private RectTransform inputButton;

		/// <summary>
		/// 输入框遮罩
		/// </summary>
		private RectTransform inputMask;

		/// <summary>
		/// 输入框
		/// </summary>
		private InputField inputField;

		#region 输入框遮罩相关
		private Vector2 maskOpen = new Vector2(500,70);
		private Vector2 maskClose = new Vector2(0,70);
		private bool maskOn;
		#endregion

		public override void Init()
		{
			base.Init();
			showType = ShowType.Fade;
			
			backButton = (RectTransform)GetGameObject("ButtonBack").transform;
			inputButton = (RectTransform)GetGameObject("ButtonInput").transform;
			inputMask = (RectTransform)GetGameObject("InputMask").transform;

			ButtonAnim("ButtonBack", backButton);
			ButtonAnim("ButtonInput", inputButton);
			
			GetControl<Button>("ButtonBack").onClick.AddListener(buttonBack);
			GetControl<Button>("ButtonInput").onClick.AddListener(buttonInput);
			inputField = GetControl<InputField>("InputField");
			inputField.onEndEdit.AddListener(inputEnd);
			EventManager.Instance.Add(EventType.InputSucceed,this,onEventInputSucceed);
		}

		public override void Show()
		{
			base.Show();
			initText();
		}

		/// <summary>
		/// 初始化文本信息
		/// </summary>
		private void initText()
		{
			UIDataGuestBook.CurIndex = 0;
			UIDataGuestBook.GuestBookOn = true;
			for (int i = 0; i < UIDataGuestBook.Row; i++)
			{
				Text text = creatText(UIDataGuestBook.Instance.Datas[UIDataGuestBook.CurIndex]);
				Vector2 pos = setTextPos(text);
				textMove(text, pos);
				UIDataGuestBook.CurIndex++;
			}
		}
		
		/// <summary>
		/// 创建文本
		/// </summary>
		/// <param name="data"></param>
		private Text creatText(GuestBookData data)
		{
			GameObject go = ChunkAllocator.Instance.GetPrefab("GuestBookText","UIPrefabs/GuestBookText",gameObject.transform);
			Text goText= go.GetComponent<Text>();
			RectTransform rt = go.transform as RectTransform;
			goText.text = data.text;
			// 确定字号
			goText.fontSize = data.count > 1 && goText.preferredWidth < 225 ? 60 : 30;
			return goText;
		}

		/// <summary>
		/// 设置初始位置
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		private Vector2 setTextPos(Text text)
		{
			float x = UIManager.Instance.ScreenUI.x + text.preferredWidth * 0.5f + Random.Range(0, UIDataGuestBook.Row) * 20;
			float y = UIManager.Instance.ScreenUI.y - 80 - UIDataGuestBook.RowSpace * UIDataGuestBook.CurIndex;
			return new Vector2(x, y);
		}
		
		/// <summary>
		/// 设置后续弹幕位置
		/// </summary>
		/// <param name="text"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		private Vector2 setTextPos(Text text,float y)
		{
			float x = UIManager.Instance.ScreenUI.x + UIDataGuestBook.ColumnSpace + text.preferredWidth * 0.5f;
			return new Vector2(x, y);
		}

		/// <summary>
		/// 弹幕移动
		/// </summary>
		private void textMove(Text text,Vector2 pos)
		{
			RectTransform rt = (RectTransform) text.transform;
			rt.localPosition = pos;
			// 第一段移动
			float end_1 = UIManager.Instance.ScreenUI.x - text.preferredWidth * 0.5f;
			float dis_1 = pos.x - end_1;
			
			// 第二段移动
			float end = -UIManager.Instance.ScreenUI.x - text.preferredWidth * 0.5f;
			float dis = end_1 - end;
			
			rt.DOLocalMoveX(end_1,dis_1 / UIDataGuestBook.Speed).
				SetEase(Ease.Linear).OnComplete(() =>
				{
					if (UIDataGuestBook.GuestBookOn)
					{
						Text _text = creatText(UIDataGuestBook.Instance.Datas[UIDataGuestBook.CurIndex]);
						Vector2 _pos = setTextPos(_text, pos.y);
						textMove(_text, _pos);
						UIDataGuestBook.CurIndex++;
						if (UIDataGuestBook.CurIndex >= UIDataGuestBook.Instance.Datas.Count - 1)
							UIDataGuestBook.CurIndex = 0;
						rt.DOLocalMoveX(end, dis / UIDataGuestBook.Speed).SetEase(Ease.Linear).OnComplete(() =>
						{
							ChunkAllocator.Instance.Revert("GuestBookText", rt.gameObject);
							rt.gameObject.SetActive(false);
						});
					}
				});
		}

		/// <summary>
		/// 弹幕输入成功回调
		/// </summary>
		private void onEventInputSucceed()
		{
			UIDataGuestBook.Instance.Datas.Insert(UIDataGuestBook.CurIndex,new GuestBookData{text = inputField.text});
			inputField.text = String.Empty;
		}

		/// <summary>
		/// 输入结束
		/// </summary>
		/// <param name="text"></param>
		private void inputEnd(string text)
		{
			UIDataGuestBook.Instance.AddGuestBookData(text);
		}

		/// <summary>
		/// 返回按钮
		/// </summary>
		private void buttonBack()
		{
			UIDataGuestBook.GuestBookOn = false;
			Hide(() =>
			{
				EventManager.Instance.SendEvent(EventType.UIMain);
			});
		}
		
		/// <summary>
		/// 输入按钮
		/// </summary>
		private void buttonInput()
		{
			if (maskOn)
			{
				inputMask.DOSizeDelta(maskClose, 0.5f);
				maskOn = false;
			}
			else
			{
				inputMask.DOSizeDelta(maskOpen, 0.5f);
				maskOn = true;
			}
		}
	}
}