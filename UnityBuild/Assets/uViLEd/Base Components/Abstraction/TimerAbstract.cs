using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        public abstract class TimerAbstract : LogicComponent, IDisposable
        {
            [Tooltip("input point for starting the timer")]
            public INPUT_POINT Start = new INPUT_POINT();
            [Tooltip("input point for forced stopping the timer")]
            public INPUT_POINT Break = new INPUT_POINT();
            [Tooltip("output point that is called when the component work is finished")]
            public OUTPUT_POINT Complete = new OUTPUT_POINT();

            private Coroutine _timerCoroutine;            

            protected bool breakTimer = false;

            protected abstract IEnumerator TimerEnumerator();            

            public override void Constructor()
            {
                Start.Handler = StartHandler;
                Break.Handler = BreakHandler;
            }

            protected virtual void StartHandler()
            {                
                if (_timerCoroutine == null)
                {
                    breakTimer = false;

                    _timerCoroutine = CoroutineHost.StartCoroutine(TimerEnumerator());
                }                                           
            }

            protected void ResetEnumerators()
            {                
                _timerCoroutine = null;
            }

            protected virtual void BreakHandler()
            {
                BreakTimer();
            }          

            private void BreakTimer()
            {
                breakTimer = true;

                if (_timerCoroutine != null)
                {                    
                    CoroutineHost.StopCoroutine(_timerCoroutine);
                    _timerCoroutine = null;
                }           
            }

            public virtual void Dispose()
            {
                BreakTimer();
            }            
        }
    }
}
