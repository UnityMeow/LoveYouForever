#region --------------------------文件信息--------------------------------------
/******************************************************************
** 文件名:	InstanceNull
** 版  权:	(C)  
** 创建人:  Unity喵
** 日  期:	2019.11.13
** 描  述: 	不继承Mono的单例基类
*******************************************************************/
#endregion

namespace LoveYouForever
{
	public class InstanceNull<T>
        where T : new()
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new T();
                }
                return instance;
            }
        }
        protected InstanceNull()
        { }
    }
}
