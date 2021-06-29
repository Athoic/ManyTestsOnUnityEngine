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

    private List<Timer> timers = new List<Timer>();
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
        for (int i=0; i < 5; i++)
        {
            TimerTest timerTest =new TimerTest(_timerIndex++);
            Timer timer = new Timer(() =>
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

        public Timer TimerComponent;

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

}
