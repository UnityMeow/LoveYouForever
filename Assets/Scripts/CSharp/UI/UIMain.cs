#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	UIMain
** 版  权:	(C)  
** 创建人:  Unity喵
** 日  期:	
** 描  述: 	
*******************************************************************/
#endregion
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LoveYouForever
{
	public class UIMain : UIBase
    {
        /// <summary>
        /// 开始游戏文本
        /// </summary>
        private RectTransform startText;

        /// <summary>
        /// 设置按钮
        /// </summary>
        private RectTransform settingButton;
        
        /// <summary>
        /// 标题图片
        /// </summary>
        private Image titleImage;

        /// <summary>
        /// 左边的星星
        /// </summary>
        private RectTransform starLeft;
        
        /// <summary>
        /// 右边的星星
        /// </summary>
        private RectTransform starRight;

        /// <summary>
        /// 记忆碎片
        /// </summary>
        private RectTransform memento;
        
        /// <summary>
        /// 留言板
        /// </summary>
        private RectTransform guestBook;

        public override void Init()
        {
            base.Init();
            startText = (RectTransform)GetGameObject("startText").transform;
            settingButton = (RectTransform)GetGameObject("setting").transform;
            titleImage = GetControl<Image>("title");
            starLeft = (RectTransform)GetGameObject("star_01").transform;
            starRight = (RectTransform)GetGameObject("star_02").transform;
            memento = (RectTransform)GetGameObject("memento").transform;
            guestBook = (RectTransform)GetGameObject("guestBook").transform;
        }

        public override void Show()
        {
            base.Show();
            startText.DOScale(1.1f, 1.8f).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.InOutQuad);
            var pos = settingButton.localPosition.y + 5;
            settingButton.DOLocalMoveY(pos,1.5f).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.InOutQuad);
            titleImage.DOFade(0.45f, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutBounce);
            starLeft.DORotate(new Vector3(0, 0, 379.2f), 20f,RotateMode.FastBeyond360).SetLoops(-1,LoopType.Restart).SetEase(Ease.InSine);
            starRight.DORotate(new Vector3(0, 0, -79.2f), 20f,RotateMode.FastBeyond360).SetLoops(-1,LoopType.Restart).SetEase(Ease.OutQuad);
            ButtonAnim("setting", settingButton);
            ButtonAnim("memento", memento);
            ButtonAnim("guestBook", guestBook);
            GetControl<Button>("setting").onClick.AddListener(buttonSetting);
            GetControl<Button>("memento").onClick.AddListener(buttonMemento);
            GetControl<Button>("guestBook").onClick.AddListener(buttonGuestBook);
            AddEventTrigger("startButton",EventTriggerType.PointerClick,buttonStart);
        }

        /// <summary>
        /// 开始按钮
        /// </summary>
        private void buttonStart(BaseEventData data)
        {
            Hide(() =>
            {
                EventManager.Instance.SendEvent(EventType.GameStart);
            });
        }

        /// <summary>
        /// 设置按钮
        /// </summary>
        private void buttonSetting()
        {
            // Hide();
            Debug.Log("设置界面");
            // UIManager.Instance.ShowPanel<UIMain>("SettingUI", "SettingUI");
        }

        /// <summary>
        /// 记忆碎片按钮
        /// </summary>
        private void buttonMemento()
        {
            // Hide();
            Debug.Log("记忆碎片");
            // UIManager.Instance.ShowPanel<UIMain>("MementoUI", "MementoUI");
        }

        /// <summary>
        /// 留言板按钮
        /// </summary>
        private void buttonGuestBook()
        {
            // Hide();
            Debug.Log("留言板");
            // UIManager.Instance.ShowPanel<UIMain>("GuestUI", "GuestUI");
        }

    }
}
