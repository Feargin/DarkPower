using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        public abstract class VectorMultiplyOperationAbstract : LogicComponent, IInputPointParse
        {
            [Tooltip("input point for setting the value of the first vector2")]
            public INPUT_POINT<Vector2> Vector2First = new INPUT_POINT<Vector2>();
            [Tooltip("input point for setting the value of the second vector2")]
            public INPUT_POINT<Vector2> Vector2Second = new INPUT_POINT<Vector2>();

            [Tooltip("input point for setting the value of the first vector3")]
            public INPUT_POINT<Vector3> Vector3First = new INPUT_POINT<Vector3>();
            [Tooltip("input point for setting the value of the second vector3")]
            public INPUT_POINT<Vector3> Vector3Second = new INPUT_POINT<Vector3>();

            [Tooltip("input point for setting the value of the first vector4")]
            public INPUT_POINT<Vector4> Vector4First = new INPUT_POINT<Vector4>();
            [Tooltip("input point for setting the value of the second vector4")]
            public INPUT_POINT<Vector4> Vector4Second = new INPUT_POINT<Vector4>();            

            [Tooltip("type of vector. This value configures the input and output points for the specified type")]
            public VectorType Vector = VectorType._Vector3;

            private Vector2 _firstV2, _secondV2;
            private Vector3 _firstV3, _secondV3;
            private Vector4 _firstV4, _secondV4;

            private bool _firstSet = false;
            private bool _secondSet = false;

            public override void Constructor()
            {
                Vector2First.Handler = Vector2FirstHandler;
                Vector2Second.Handler = Vector2SecondHandler;

                Vector3First.Handler = Vector3FirstHandler;
                Vector3Second.Handler = Vector3SecondHandler;

                Vector4First.Handler = Vector4FirstHandler;
                Vector4Second.Handler = Vector4SecondHandler;
            }

            protected abstract object Vector2Operation(Vector2 first, Vector2 second);
            protected abstract object Vector3Operation(Vector3 first, Vector3 second);
            protected abstract object Vector4Operation(Vector4 first, Vector4 second);
            protected abstract void HandlerResult(object result);
            
            private void Vector2FirstHandler(Vector2 value)
            {                               
                if(_secondSet)
                {
                    _firstSet = false;
                    _secondSet = false;
                    
                    HandlerResult(Vector2Operation(value, _secondV2));                    
                }else
                {
                    _firstV2 = value;
                    _firstSet = true;
                }
            }

            private void Vector2SecondHandler(Vector2 value)
            {                
                if (_firstSet)
                {
                    _firstSet = false;
                    _secondSet = false;

                    HandlerResult(Vector2Operation(_firstV2, value));
                }
                else
                {
                    _secondV2 = value;
                    _secondSet = true;
                }
            }

            private void Vector3FirstHandler(Vector3 value)
            {
                if (_secondSet)
                {
                    _firstSet = false;
                    _secondSet = false;

                    HandlerResult(Vector3Operation(value, _secondV3));
                }
                else
                {
                    _firstV3 = value;
                    _firstSet = true;
                }
            }

            private void Vector3SecondHandler(Vector3 value)
            {
                if (_firstSet)
                {
                    _firstSet = false;
                    _secondSet = false;

                    HandlerResult(Vector3Operation(_firstV3, value));
                }
                else
                {
                    _secondV3 = value;
                    _secondSet = true;
                }
            }

            private void Vector4FirstHandler(Vector4 value)
            {
                if (_secondSet)
                {
                    _firstSet = false;
                    _secondSet = false;

                    HandlerResult(Vector4Operation(value, _secondV4));
                }
                else
                {
                    _firstV4 = value;
                    _firstSet = true;
                }
            }

            private void Vector4SecondHandler(Vector4 value)
            {
                if (_firstSet)
                {
                    _firstSet = false;
                    _secondSet = false;

                    HandlerResult(Vector2Operation(_firstV4, value));
                }
                else
                {
                    _secondV4 = value;
                    _secondSet = true;
                }
            }

            public IDictionary<string, object> GetInputPoints()
            {
                var inputPonts = new Dictionary<string, object>();

                switch (Vector)
                {
                    case VectorType._Vector2:
                        inputPonts.Add("Vector2First", this.GetType().GetField("Vector2First"));
                        inputPonts.Add("Vector2Second", this.GetType().GetField("Vector2Second"));
                        break;
                    case VectorType._Vector3:
                        inputPonts.Add("Vector3First", this.GetType().GetField("Vector3First"));
                        inputPonts.Add("Vector3Second", this.GetType().GetField("Vector3Second"));
                        break;
                    case VectorType._Vector4:
                        inputPonts.Add("Vector4First", this.GetType().GetField("Vector4First"));
                        inputPonts.Add("Vector4Second", this.GetType().GetField("Vector4Second"));
                        break;
                }

                return inputPonts;
            }
        }
    }
}
