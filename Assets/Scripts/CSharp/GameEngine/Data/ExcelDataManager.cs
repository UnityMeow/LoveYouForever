#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	ExcelDataManager
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
	public static class ExcelDataManager
	{
		/// <summary>
		/// 表格存放路径
		/// </summary>
		private static string excelPath = "ExcelAssets/";

		/// <summary>
		/// 表格数据
		/// </summary>
		private static Dictionary<string, ScriptableObject> excelData = new Dictionary<string, ScriptableObject>();
		
		/// <summary>
		/// 获取表格数据
		/// </summary>
		/// <param name="excelName"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T GetData<T>(string excelName)
			where T : ScriptableObject
		{
			if (excelData.ContainsKey(excelName))
			{
				return (T)excelData[excelName];
			}
			
			// 加载表格数据资源
			var data = AssetManager.GetAsset<ScriptableObject>(excelPath + excelName + ".asset");
			excelData.Add(excelName,data);
			return (T)data;
		}
	}
}
