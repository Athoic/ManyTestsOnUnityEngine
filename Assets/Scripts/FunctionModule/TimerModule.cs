
using FunctionModule.Events;
using System;
using System.Collections;
using System.Threading;
using UnityEngine;

namespace FunctionModule
{
    public class Timer
    {
        private UnityClockModule _unityClockModule;


        public static IEnumerator SetDelayFunc(Action action, float delaySeconds)
        {
            yield return new WaitForSeconds(delaySeconds);
            action?.Invoke();
        }

        private Action _loopFunc;
        private long _delayTime;
        private long _interval;
        /// <summary>
        /// 总计要循环的次数
        /// </summary>
        private int _loopCount;


        private long _lastFrameTime = 0;
        private long _passedTime = 0;
        /// <summary>
        /// 已循环次数
        /// </summary>
        private int _count;

        public ETimerState TimerState { get; private set; } = ETimerState.Undefine;

        /// <summary>
        /// 获取一个循环定时器
        /// </summary>
        /// <param name="action">循环执行的方法</param>
        /// <param name="interval">间隔时间。单位：毫秒</param>
        /// <param name="loopCount">循环次数。若此值不为自然数，则没有循环次数限制</param>
        /// <param name="delayTime">开始计时前的延迟时间。</param>
        public Timer(Action action, long interval, int loopCount=-1,long delayTime=0)
        {
            _loopFunc = action;
            _interval = interval;
            _loopCount = loopCount;
            _delayTime = delayTime;

            _count = 0;
            TimerState = ETimerState.Ready;

            _unityClockModule=GameObject.Find("AppSystem").GetComponent<UnityClockModule>();
        }

        public bool StartLoop()
        {
            if (TimerState != ETimerState.Ready)
                return false;

            _unityClockModule.FrameUpdateEvent += UnityClockModule_FrameUpdateEvent;

            return true;
        }

        private void UnityClockModule_FrameUpdateEvent(UnityClockModule_FrameUpdateEventArgs eventArgs)
        {
            _passedTime += eventArgs.PassedTime;

            //if(PreLoop())
            //    Loop();
            PreLoop();
            Loop();
        }

        private bool PreLoop()
        {
            if (_delayTime <= 0) 
            {
                if(TimerState==ETimerState.Ready)
                    TimerState = ETimerState.Started;
                return true;
            }

            if (_passedTime < _delayTime)
                return false;
            
            TimerState = ETimerState.Started;
            _loopFunc?.Invoke();
            _count++;
            _delayTime = 0;
            _passedTime = 0;
            return true;
        }

        private void Loop()
        {
            if (TimerState != ETimerState.Started)
                return;

            if (_count == _loopCount)
            {
                EndLoop();
                return;
            }

            if (_passedTime < _interval)
                return;

            _loopFunc?.Invoke();
            _count++;
            _passedTime = 0;
        }
    
        public void EndLoop()
        {
            TimerState = ETimerState.Ended;
            _loopFunc = null;
            _unityClockModule.FrameUpdateEvent -= UnityClockModule_FrameUpdateEvent;
        }
    }

    public enum ETimerState
    {
        Undefine,

        Ready,

        Started,

        Pause,

        Ended,
    }

}
