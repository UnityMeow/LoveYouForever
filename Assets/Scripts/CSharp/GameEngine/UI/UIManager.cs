#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	UIManager
** 版  权:	(C)  
** 创建人:  Unity喵
** 日  期:	
** 描  述: 	
**************************** 修改记录 ******************************
** 修改人: 
** 日  期: 
** 描  述: 
*******************************************************************/
#endregion
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LoveYouForever
{
    public class UIManager : InstanceNull<UIManager>
    {
        /// <summary>
        /// UI层级
        /// </summary>
        public enum Layer
        {
            Top,
            Mid,
            Bot,
        }

        /// <summary>
        /// 面板显示总开关
        /// </summary>
        public bool isShow;

        /// <summary>
        /// UI
        /// </summary>
        public Transform UI => ResLoadManager.Instance.LoadPrefab("Prefabs/UI", "UI").transform;

        /// <summary>
        /// Canvas层
        /// </summary>
        public Transform UIRoot => UI.Find("Canvas");

        /// <summary>
        /// UI顶层
        /// </summary>
        public Transform UITop => UIRoot.Find("Top");

        /// <summary>
        /// UI中层
        /// </summary>
        public Transform UIMid => UIRoot.Find("Mid");

        /// <summary>
        /// UI底层
        /// </summary>
        public Transform UIBot => UIRoot.Find("Bot");

        /// <summary>
        /// UI画布
        /// </summary>
        public Canvas rootCanvas => UIRoot.GetComponent<Canvas>();

        /// <summary>
        /// Canvas层的RectTransform
        /// </summary>
        public RectTransform rootRectTransform => UIRoot.GetComponent<RectTransform>();

        /// <summary>
        /// 画布尺寸相关
        /// </summary>
        public CanvasScaler rootScaler => UIRoot.GetComponent<CanvasScaler>();

        /// <summary>
        /// 半屏宽
        /// </summary>
        public float halfScreenW => Screen.width * 0.5f;

        /// <summary>
        /// 半屏高
        /// </summary>
        public float halfScreenH => Screen.height * 0.5f;

        /// <summary>
        /// 真机比率
        /// </summary>
        public float realMachineRatio => (float)Screen.height / Screen.width;

        /// <summary>
        /// 预设比率
        /// </summary>
        public float preaetRatio => rootScaler.referenceResolution.y / rootScaler.referenceResolution.x;

        /// <summary>
        /// UI面板管理表
        /// </summary>
        Dictionary<string, UIBase> _UIPanelList;

        public UIManager()
        {
            _UIPanelList = new Dictionary<string, UIBase>();
            Object.DontDestroyOnLoad(UI.gameObject);
        }

        /// <summary>
        /// 显示面板
        /// (我觉得没啥毛病!)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="panelName"></param>
        /// <param name="prefabName"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public T ShowPanel<T>(string panelName, string prefabName, Layer layer = Layer.Mid)
            where T : UIBase
        {
            UIBase panel;
            if (!IsHavePanel(panelName))
            {
                GameObject obj = ResLoadManager.Instance.LoadPrefab("Prefabs/UI", "prefabName");
                obj.name = panelName;
                //设置层级
                RectTransform tr = obj.transform as RectTransform;
                switch (layer)
                {
                    case Layer.Top: tr.SetParent(UITop); break;
                    case Layer.Mid: tr.SetParent(UIMid); break;
                    case Layer.Bot: tr.SetParent(UIBot); break;
                }
                tr.localPosition = Vector3.zero;
                tr.localScale = Vector3.one;
                tr.offsetMax = Vector2.zero;
                tr.offsetMin = Vector2.zero;
                panel = obj.AddComponent<T>();
                _UIPanelList.Add(panelName, panel);
                panel.Init();
                panel.Show();
            }
            else
            {
                panel = _UIPanelList[panelName];
                panel.transform.SetAsLastSibling();
                panel.Show();
            }
            return panel as T;
        }

        /// <summary>
        /// 隐藏面板
        /// </summary>
        /// <param name="panelName"></param>
        public void HidePanel(string panelName)
        {
            if (IsHavePanel(panelName))
            {
                _UIPanelList[panelName].Hide();
            }
        }

        /// <summary>
        /// 隐藏所有面板
        /// </summary>
        public void HideAllPanel()
        {
            Dictionary<string, UIBase>.Enumerator enumerator
                = _UIPanelList.GetEnumerator();
            while (enumerator.MoveNext())
            {
                _UIPanelList[enumerator.Current.Key].Hide();
            }
        }

        /// <summary>
        /// 移除面板
        /// </summary>
        /// <param name="panelName"></param>
        public void DestroyPanel(string panelName)
        {
            if (IsHavePanel(panelName))
            {
                _UIPanelList[panelName].Destroy();
                _UIPanelList.Remove(panelName);
            }
        }

        /// <summary>
        /// 移除所有面板
        /// </summary>
        public void DestroyAllPanel()
        {
            Dictionary<string, UIBase>.Enumerator enumerator = _UIPanelList.GetEnumerator();
            while (enumerator.MoveNext())
            {
                _UIPanelList[enumerator.Current.Key].Destroy();
            }
            _UIPanelList.Clear();
        }

        /// <summary>
        /// 获取面板
        /// </summary>
        /// <param name="panelName"></param>
        /// <returns></returns>
        public UIBase GetPanel(string panelName)
        {
            if (IsHavePanel(panelName))
                return _UIPanelList[panelName];
            return null;
        }

        /// <summary>
        /// 是否有对应名字的面板
        /// </summary>
        /// <param name="panelName"></param>
        /// <returns></returns>
        bool IsHavePanel(string panelName)
        {
            return _UIPanelList.ContainsKey(panelName);
        }
    }
}
