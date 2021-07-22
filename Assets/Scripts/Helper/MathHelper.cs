using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Project.Helper
{
    public class MathHelper
    {
        public const double Degree2Rad = Math.PI / 180;
        public const double Rad2Degree = 180 / Math.PI;

        public static double Sin(float degree)
        {
            return Math.Sin(degree * Degree2Rad);
        }

        public static double Cos(float degree)
        {
            return Math.Cos(degree * Degree2Rad);
        }

        /// <summary>
        /// 让一个向量旋转给定值
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="degree"></param>
        /// <returns></returns>
        public static Vector2 RotateVector(Vector2 origin, float degree)
        {
            Vector2 logOrigin = origin;
            double x = origin.x * Cos(degree) - origin.y * Sin(degree);
            double y = origin.x * Sin(degree) + origin.y * Cos(degree);
            origin.x = (float)x;
            origin.y = (float)y;
            //Debug.Log($"初始向量为：{logOrigin}\n旋转角度为：{degree}\n旋转结果向量为：{origin}");
            return origin;
        }

        public static double ATan(double tanValue)
        {
            return Math.Atan(tanValue) * Rad2Degree;
        }
    
        /// <summary>
        /// 获取一个由源坐标指向目标坐标的四元数值
        /// </summary>
        /// <param name="target"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Quaternion GetQuaternion(Vector3 target, Vector3 source)
        {
            if (target.x == source.x)
            {
                if (target.y >= source.y)
                {
                    return Quaternion.Euler(0, 0, 90);
                }
                else
                {
                    return Quaternion.Euler(0, 0, -90);
                }
            }
            else
            {
                return Quaternion.Euler(0, 0, (float)ATan((target.y - source.y) / (target.x - source.x)));

                //if (target.x >= source.x)
                //{
                //    return Quaternion.Euler(0, 0, (float)ATan((target.y - source.y) / (target.x - source.x)));
                //}
                //else
                //{
                //    return Quaternion.Euler(0, 0, (float)ATan((target.y - source.y) / (target.x - source.x)));

                //}
            }
            

        }
    
        public static Quaternion GetQuaternion(Vector3 orientation)
        {
            if (orientation == Vector3.right)
                return Quaternion.Euler(0, 0, 0);
            else if (orientation == Vector3.left)
                return Quaternion.Euler(0, 0, 0);
            //else if (orientation == Vector3.left)
            //    return Quaternion.Euler(0, 0, 180);
            else if (orientation == Vector3.up)
                return Quaternion.Euler(0, 0, 90);
            else if (orientation == Vector3.down)
                return Quaternion.Euler(0, 0, -90);
            else
                return Quaternion.Euler(0, 0, 0);

        }

        public static long GetIntervalByCountPerSecond(int countPerSecond)
        {
            return (long)((float)1 / (float)countPerSecond * 1000f);
        }
    }
}
