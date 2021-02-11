using System.Collections.Generic;
using System.Text;
using System;
using UnityEngine;

using uViLEd.Core;

namespace uViLEd
{
    namespace Components
    {
        [ComponentDefinition(Name = "DebugConsole", 
                           Path = "uViLEd Components/Base/Debug", 
                           Tooltip = "the component to display the console inside the application", 
                           Color = VLEColor.DarkOrange)]
        public class DebugConsoleComponent : LogicComponent
        {         
            [ViewInEditor]
            [Tooltip("flag for enabling or disabling the console")]
            public bool State = true;
            [ViewInEditor]
            [Tooltip("flag for show stack trace data")]
            public bool ShowStackTrace = false;
            [ViewInEditor]
            [Tooltip("flag to show the last record first in the list")]
            public bool LastAsFirst = true;
            [ViewInEditor]
            [Tooltip("maximum number of records in the console")]
            public int MaxLogCount = 20;
            [Range(0f, 1f)]
            [Tooltip("console size relative to the screen")]
            public float PartOfHeightScreen = 0.3f;

            [Tooltip("the key for displaying the console (PC). For a mobile projects, the console is opened by tapping the screen with 2 fingers")]
            public KeyCode KeyForShowConsole;

            private bool _showConsole;
            private List<string> _logStrList = new List<string>();
            private Vector2 _scrollPosition = Vector2.zero;

            private GUIStyle _consoleLogStrStyle;

            private const string _baseLogStr = "<size=14>[<color={0}><b>{1}</b></color>]: {2}</size>";
            private const string _traceStackStr = "[<color=white><b>Stack</b></color>]: <color=grey>{0}</color>";

            public override void Constructor()
            {
                if (State)
                {
                    Application.logMessageReceivedThreaded += LogMessageReceivedHandler;
                }
            }

            private void LogMessageReceivedHandler(string condition, string stackTrace, LogType type)
            {
                lock(_logStrList)
                {
                    void pushToList (string str)
                    {                        
                        if (LastAsFirst)
                        {
                            if (_logStrList.Count == MaxLogCount)
                            {
                                _logStrList.RemoveAt(_logStrList.Count - 1);
                            }

                            _logStrList.Insert(0, str);
                        }
                        else
                        {
                            if (_logStrList.Count == MaxLogCount)
                            {
                                _logStrList.RemoveAt(0);
                            }

                            _logStrList.Add(str);
                        }
                    };

                    var stringBuilder = new StringBuilder();

                    switch(type)
                    {
                        case LogType.Assert:
                            stringBuilder.Append(_baseLogStr.Fmt("brown", type, condition));
                            break;
                        case LogType.Error:
                            stringBuilder.Append(_baseLogStr.Fmt("red", type, condition));
                            break;
                        case LogType.Exception:
                            stringBuilder.Append(_baseLogStr.Fmt("red", type, condition));
                            break;
                        case LogType.Warning:
                            stringBuilder.Append(_baseLogStr.Fmt("yellow", type, condition));
                            break;
                        case LogType.Log:
                            stringBuilder.Append(_baseLogStr.Fmt("white", type, condition));
                            break;
                    }
                                        
                    if (ShowStackTrace)
                    {
                        stringBuilder.Append("\n");
                        stringBuilder.Append(_traceStackStr.Fmt(stackTrace));

                        pushToList(stringBuilder.ToString());
                    } else
                    {                        
                        pushToList(stringBuilder.ToString());
                    }                                              
                }
            }

            private void OnGUI()
            {                
                if (State && _showConsole)
                {
                    PrepareGUIStyle();

                    lock (_logStrList)
                    {
                        var rectPosition = new Rect(0f, Screen.height * (1f - PartOfHeightScreen), Screen.width, Screen.height * PartOfHeightScreen);
                   
                        GUILayout.BeginArea(rectPosition, GUI.skin.box);
                        _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);

                        foreach(var logData in _logStrList)
                        {
                            GUILayout.TextArea(logData, _consoleLogStrStyle);
                        }                       

                        GUILayout.EndScrollView();
                        GUILayout.EndArea();
                    }                
                }
            }

            private void Update()
            {
                if(State)
                {
#if UNITY_IOS || UNITY_ANDROID
                    if(Input.touchCount == 3)
                    {
                        _showConsole = !_showConsole;
                    }
#else                    
                    if(Input.GetKeyDown(KeyForShowConsole))
                    {
                        _showConsole = !_showConsole;
                    }
#endif
                }
            }

            private void PrepareGUIStyle()
            {
                if(_consoleLogStrStyle == null)
                {
                    _consoleLogStrStyle = new GUIStyle(GUI.skin.textArea);
                    _consoleLogStrStyle.richText = true;
                }
            }
        }
    }
}