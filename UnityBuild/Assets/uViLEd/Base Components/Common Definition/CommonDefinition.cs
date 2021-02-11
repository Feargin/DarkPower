#pragma warning disable 649

using System;
using System.Collections.Generic;
using System.Text;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace uViLEd
{
    [Serializable]
    public class AnimationEventDefinition
    {
        public int ClipIndex;

        [Serializable]
        public class EventDescription
        {
            public string Name;
            public int Frame;
        }

        public List<EventDescription> FrameEvents;
    }

    [Serializable]
    public class LogicEventDefinition : IWrapperData
    {
        public string ToId;
        public string FromId;

        [SerializeField]
        private string _name;

        public (string Name, string Value) GetInfo()
        {
            return ("Logic", _name);
        }
    }

    [Serializable]
    public class LogicMessageDefinition : IWrapperData
    {
        public int Id;
        [SerializeField]
        private string _name;

        public (string Name, string Value) GetInfo()
        {
            return ("Message", _name);
        }
    }

    [Serializable]
    public class ComponentNameDefinition : IWrapperData
    {
        public string Name
        {
            get
            {
                return _componentShortName;
            }
        }
        [SerializeField]
        private string _assembly;
        [SerializeField]
        private string _componentName;
        [SerializeField]
        private string _componentShortName;

        private Type _cachedType;

        public Type GetComponentType()
        {
            if (_cachedType != null) return _cachedType;

            var assemblys = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var assembly in assemblys)
            {
                if (string.Compare(assembly.FullName, _assembly, StringComparison.Ordinal) == 0)
                {
                    _cachedType = assembly.GetType(_componentName);

                    return _cachedType;
                }
            }

            return null;
        }

        public (string Name, string Value) GetInfo()
        {
            return ("Component", _componentShortName);
        }     
    }

    [Serializable]
    public class InputAxisDefinition : IWrapperData
    {
        public string Name;

        public (string Name, string Value) GetInfo()
        {
            return ("Axis", Name);
        }        
    }

    public enum InputEventType
    {
        Down = 0,
        Up = 1,
        Hold = 2
    }

    [Serializable]
    public class InputEventsDefinition : IWrapperData
    {
        [SerializeField]
        private int _mask = -1;

        private List<InputEventType> _listEvents = new List<InputEventType>();

        public List<InputEventType> GetEvents()
        {
#if UNITY_EDITOR
            if (!EditorApplication.isPlaying)
            {
                _listEvents.Clear();
            }
#endif
            if (_listEvents.Count == 0)
            {
                if (_mask < 0)
                {
                    _listEvents.Add(InputEventType.Down);
                    _listEvents.Add(InputEventType.Up);
                    _listEvents.Add(InputEventType.Hold);
                }
                else if (_mask > 0)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        int layer = 1 << i;

                        if ((_mask & layer) != 0)
                        {
                            _listEvents.Add((InputEventType)i);
                        }
                    }
                }
            }

            return _listEvents;
        }

        public (string Name, string Value) GetInfo()
        {
            var events = GetEvents();
            var stringBuilder = new StringBuilder();

            foreach (var ev in events)
            {
                stringBuilder.Append(ev.ToString());
                stringBuilder.Append(", ");
            }

            if (stringBuilder.Length > 0)
            {
                stringBuilder.Remove(stringBuilder.Length - 2, 2);
            }

            return ("Events", stringBuilder.ToString());
        }
    }

    [Serializable]
    public class MecanimParameterDefinition : IWrapperData
    {
        public string Name;
        public int DataType;

        public MecanimParameterDefinition(AnimatorControllerParameterType dataType)
        {
            DataType = (int)dataType;
        }

        public (string Name, string Value) GetInfo()
        {
            return ("ParameterName", Name);
        }
    }
    
    [Serializable]
    public class SceneDefinition : IWrapperData
    {
        public int SceneIndex
        {
            get
            {
                return _sceneIndex;
            }
        }

        public string SceneName
        {
            get
            {
                return _sceneName;
            }
        }

        [SerializeField]
        private string _sceneName;

        [SerializeField]
        private int _sceneIndex;


        public (string Name, string Value) GetInfo()
        {
            return ("Scene", _sceneName);
        }       
    }

    [Serializable]
    public class AssetBundleDefinition : IWrapperData
    {
        public string Name
        {
            get
            {
                return _assetBundleName;
            }
        }

        [SerializeField]
        private string _assetBundleName;

        public (string Name, string Value) GetInfo()
        {
            return ("Bundle", _assetBundleName);
        }
    }
}