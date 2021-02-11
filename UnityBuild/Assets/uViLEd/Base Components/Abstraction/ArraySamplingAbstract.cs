using System.Collections.Generic;
using UnityEngine;
using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        public enum SamplingType
        {
            Sequence,
            Random,
            Shuffle
        }

        public abstract class ArraySamplingAbstract<T> : LogicComponent, IInputPointParse, IOutputPointParse
        {
            [Tooltip("input point for executing the sampling step")]
            public INPUT_POINT DoStep = new INPUT_POINT();
            [Tooltip("input point for resetting the current sampling state to the initial state\n\n"+
                     "Note: this point is switched on if the sampling mode is not random and the loop flag is not set")]
            public INPUT_POINT Reset = new INPUT_POINT();

            [Tooltip("output point that transmits the value of the selected array member")]
            public OUTPUT_POINT<T> ItemValue = new OUTPUT_POINT<T>();
            [Tooltip("output point, called after a full iterating through all array members\n\n"+
                     "Note:this point is switched on if the loop flag is not set)")]
            public OUTPUT_POINT Complete = new OUTPUT_POINT();

            public VARIABLE_LINK<T[]> Variable = new VARIABLE_LINK<T[]>();
            
            [ViewInEditor]
            [Tooltip("setting the method for retrieving array members")]
            public SamplingType Sampling;

            [Tooltip("a flag of loop selection, indicates that after iterating through all the members of the array, the process starts anew at the next step")]
            public bool Loop = false;

            private bool _completed = false;
            private int _currentIndex = 0;
            private int[] _shuffleIndices;

            public override void Constructor()
            {
                DoStep.Handler = DoStepHandler;
                Reset.Handler = ResetHandler;                
            }                        

            private void DoStepHandler()
            {             
                switch(Sampling)
                {
                    case SamplingType.Sequence:
                        {
                            if (_currentIndex < Variable.Value.Length)
                            {
                                var item = Variable.Value[_currentIndex];

                                _currentIndex++;

                                ItemValue.Execute(item);                                
                            }

                            if (_currentIndex == Variable.Value.Length)
                            {                                                                        
                                if (Loop)
                                {                                     
                                    _currentIndex = 0;
                                }
                                else
                                {
                                    if (!_completed)
                                    {
                                        _completed = true;

                                        Complete.Execute();
                                    }
                                }
                            }                               
                        }
                        break;
                    case SamplingType.Random:
                        {
                            var index = UnityEngine.Random.Range(0, Variable.Value.Length);
                            var item = Variable.Value[index];

                            ItemValue.Execute(item);
                        }
                        break;
                    case SamplingType.Shuffle:
                        {
                            if (_shuffleIndices == null)
                            {
                                _shuffleIndices = Randomizer.RandomIndices(Variable.Value.Length);
                            }

                            if (_currentIndex < _shuffleIndices.Length)
                            {
                                var item = Variable.Value[_shuffleIndices[_currentIndex]];

                                _currentIndex++;

                                ItemValue.Execute(item);                                
                            }

                            if (_currentIndex == _shuffleIndices.Length)
                            {
                                if (Loop)
                                {
                                    _shuffleIndices = null;
                                    _currentIndex = 0;
                                }else
                                {
                                    if (!_completed)
                                    {
                                        _completed = true;

                                        Complete.Execute();
                                    }
                                }
                            }                            
                        }
                        break;
                }
            }    
            
            private void ResetHandler()
            {
                _completed = false;
                _currentIndex = 0;
                _shuffleIndices = null;
            }

            public IDictionary<string, object> GetInputPoints()
            {
                var inputPoints = new Dictionary<string, object>();

                inputPoints.Add("DoStep", this.GetType().GetField("DoStep") );                

                if (!Loop && Sampling != SamplingType.Random)
                {
                    inputPoints.Add("Reset", this.GetType().GetField("Reset"));
                }

                return inputPoints;
            }

            public IDictionary<string, object> GetOutputPoints()
            {
                var outputPoints = new Dictionary<string, object>();

                outputPoints.Add("ItemValue", this.GetType().GetField("ItemValue"));

                if (!Loop && Sampling != SamplingType.Random)
                {
                    outputPoints.Add("Complete", this.GetType().GetField("Complete"));
                }

                return outputPoints;
            }
        }
    }
}
