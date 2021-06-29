using FunctionModule.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FunctionModule
{
    public class UnityClockModule : MonoBehaviour
    {
        public delegate void FrameUpdateEventHandler(UnityClockModule_FrameUpdateEventArgs eventArgs);
        public event FrameUpdateEventHandler FrameUpdateEvent;


        private Queue<UnityClockModule_FrameUpdateEventArgs> _frameUpdateEventArgsQueue = new Queue<UnityClockModule_FrameUpdateEventArgs>();
        
        private long _lastFrameTime = 0;
        private const float _delayRecycleTime = 0.05f;

        private void Update()
        {
            long frameTime = DateTimeModule.GetNowTimeStamp();
            if (_lastFrameTime == 0)
                _lastFrameTime = frameTime;

            UnityClockModule_FrameUpdateEventArgs e = GetFrameUpdateEventArgs();
            e.LastFrameTime = _lastFrameTime;
            e.FrameTime = frameTime;
            FrameUpdateEvent?.Invoke(e);

            StartCoroutine(RecycleUnityClockModule_FrameUpdateEventArgs(() =>
            {
                _frameUpdateEventArgsQueue.Enqueue(e);
            }));
            _lastFrameTime = frameTime;
        }

        private UnityClockModule_FrameUpdateEventArgs GetFrameUpdateEventArgs()
        {
            if (_frameUpdateEventArgsQueue.Count > 0)
                return _frameUpdateEventArgsQueue.Dequeue();
            else
                return new UnityClockModule_FrameUpdateEventArgs();
        }
    
        private IEnumerator RecycleUnityClockModule_FrameUpdateEventArgs(Action action)
        {
            yield return new WaitForSeconds(_delayRecycleTime);
            action?.Invoke();
        }
    }
}

namespace FunctionModule.Events
{
    public class UnityClockModule_FrameUpdateEventArgs
    {
        public long LastFrameTime { get; set; }

        public long FrameTime { get; set; }

        public int PassedTime 
        { 
            get 
            {
                return (int)(FrameTime - LastFrameTime);    
            }
        }
    }
}
