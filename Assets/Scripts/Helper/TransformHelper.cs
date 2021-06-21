using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Project.Helper
{
    public class TransformHelper
    {
        public static Transform GetTransform(Transform check, string name)
        {
            Transform forreturn = null;

            foreach (Transform t in check.GetComponentsInChildren<Transform>())
            {
                if (t.name == name)
                {
                    Debug.Log("得到最终子物体的名字是：" + t.name);
                    forreturn = t;
                    return t;

                }

            }
            return forreturn;
        }
    }
}
