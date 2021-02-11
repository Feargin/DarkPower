using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        public enum VectorType
        {
            _Vector2,
            _Vector3,                
            _Vector4
        }

        public abstract class VectorSingleOperationAbstract : LogicComponent, IInputPointParse
        {
            [Tooltip("input point for setting the value of the vector2")]
            public INPUT_POINT<Vector2> Vector2In = new INPUT_POINT<Vector2>();
            [Tooltip("input point for setting the value of the vector3")]
            public INPUT_POINT<Vector3> Vector3In = new INPUT_POINT<Vector3>();
            [Tooltip("input point for setting the value of the vector4")]
            public INPUT_POINT<Vector4> Vector4In = new INPUT_POINT<Vector4>();

            [Tooltip("type of vector. This value configures the input and output points for the specified type")]
            public VectorType Vector;
            
            public IDictionary<string, object> GetInputPoints()
            {
                var inputPonts = new Dictionary<string, object>();

                switch(Vector)
                {
                    case VectorType._Vector2:
                        inputPonts.Add("Vector2In", this.GetType().GetField("Vector2In"));
                        break;
                    case VectorType._Vector3:
                        inputPonts.Add("Vector3In", this.GetType().GetField("Vector3In"));
                        break;
                    case VectorType._Vector4:
                        inputPonts.Add("Vector4In", this.GetType().GetField("Vector4In"));
                        break;
                }
                
                return inputPonts;
            }            
        }
    }
}