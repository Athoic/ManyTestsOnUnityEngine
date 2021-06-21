
using System;
using System.Collections;
using System.Threading;
using UnityEngine;

namespace FunctionModule
{
    public class TimerModule : MonoBehaviour
    {
        public static IEnumerator SetDelayFunc(Action action, float delaySeconds)
        {
            yield return new WaitForSeconds(delaySeconds);
            action?.Invoke();
        }

        //public static void SetDelayFunc(Action action, int delayTime)
        //{
        //    Thread thread = new Thread(new ThreadStart(() =>
        //      {
        //          Thread.Sleep(delayTime);
        //          action.Invoke();
        //      }));
        //    thread.IsBackground = true;
        //    thread.Start();
        //}

    }

}
