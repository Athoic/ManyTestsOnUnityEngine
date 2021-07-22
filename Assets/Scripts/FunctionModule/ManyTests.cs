using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FunctionModule;

public class ManyTests : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region 定时器测试
    /// <summary>
    /// 定时器个数
    /// </summary>
    private const int _timerCount = 10;
    private List<UnityTimer> timers = new List<UnityTimer>();
    private bool _isListClean=true;
    private int _timerIndex = 0;
    public void TestTimer()
    { 
        if(!_isListClean)
        {
            for(int i = 0, count = timers.Count; i < count; i++)
            {
                timers[i].EndLoop();
            }
            _isListClean = true;
            return;
        }

        timers.Clear();
        for (int i=0; i < _timerCount; i++)
        {
            TimerTest timerTest =new TimerTest(_timerIndex++);
            UnityTimer timer = new UnityTimer(() =>
              {
                  timerTest.ShowTimer();
              },
            1000);

            timerTest.TimerComponent = timer;
            timers.Add(timer);
            
            timer.StartLoop();
        }
        _isListClean = false;
    }

    class TimerTest
    {
        private int _index;

        public UnityTimer TimerComponent;

        private int _execCount = 0;

        public TimerTest(int index)
        {
            _index = index;
            _execCount = 0;
        }

        public void ShowTimer()
        {
            _execCount++;
            Debug.Log($"\n第{_index}定时器运行中，\n状态为{TimerComponent.TimerState}");
        }
    }

    #endregion
}
