using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Project.Helper
{
    public class GameObjectHelper
    {
        /// <summary>
        /// 获取指定名称的子节点
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<GameObject> FindChildrenWithTag(GameObject parent, string tag)
        {
            List<GameObject> res = new List<GameObject>();

            foreach (Transform t in parent.GetComponentsInChildren<Transform>())
            {
                if (t.tag == tag)
                {
                    res.Add(t.gameObject);
                }
            }
            return res;
        }       
        
        public static GameObject FindChild(GameObject parent, string name)
        {
            GameObject forreturn = null;

            foreach (Transform t in parent.GetComponentsInChildren<Transform>())
            {
                if (t.name == name)
                {
                    //Debug.Log("得到最终子物体的名字是：" + t.name);
                    forreturn = t.gameObject;
                    return t.gameObject;

                }

            }
            return forreturn;
        }

        
    }
}
