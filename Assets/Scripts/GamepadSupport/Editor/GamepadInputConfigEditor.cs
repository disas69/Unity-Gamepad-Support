using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GamepadSupport.Editor
{
    [CustomEditor(typeof(GamepadInputConfig))]
    public class GamepadInputConfigEditor : UnityEditor.Editor
    {
        private const string JoystickButton0 = "joystick button 0";
        private const string JoystickButton1 = "joystick button 1";
        private const string JoystickButton2 = "joystick button 2";
        private const string JoystickButton3 = "joystick button 3";
        private const string JoystickButton4 = "joystick button 4";
        private const string JoystickButton5 = "joystick button 5";
        private const string JoystickButton6 = "joystick button 6";
        private const string JoystickButton7 = "joystick button 7";
        private const string JoystickButton8 = "joystick button 8";
        private const string JoystickButton9 = "joystick button 9";

        private readonly Dictionary<GamepadButton, string> _buttonImagesDictionary = new Dictionary<GamepadButton, string>
        {
            { GamepadButton.Button1, "Assets/Scripts/GamepadSupport/Editor/gamepad_button1.png" },
            { GamepadButton.Button2, "Assets/Scripts/GamepadSupport/Editor/gamepad_button2.png" },
            { GamepadButton.Button3, "Assets/Scripts/GamepadSupport/Editor/gamepad_button3.png" },
            { GamepadButton.Button4, "Assets/Scripts/GamepadSupport/Editor/gamepad_button4.png" },
            { GamepadButton.LeftBumper, "Assets/Scripts/GamepadSupport/Editor/gamepad_button_lb.png" },
            { GamepadButton.RightBumper, "Assets/Scripts/GamepadSupport/Editor/gamepad_button_rb.png" },
            { GamepadButton.LeftTrigger, "Assets/Scripts/GamepadSupport/Editor/gamepad_button_lt.png" },
            { GamepadButton.RightTrigger, "Assets/Scripts/GamepadSupport/Editor/gamepad_button_rt.png" },
            { GamepadButton.BackButton, "Assets/Scripts/GamepadSupport/Editor/gamepad_button_back.png" },
            { GamepadButton.StartButton, "Assets/Scripts/GamepadSupport/Editor/gamepad_button_start.png" },
        };

        private GamepadInputConfig _target;
        private GUIStyle _headerStyle;
        private GUIStyle _labelStyle;

        protected virtual void OnEnable()
        {
            _target = target as GamepadInputConfig;
            _headerStyle = new GUIStyle
            {
                normal = {textColor = Color.gray},
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter
            };
            _labelStyle = new GUIStyle
            {
                normal = {textColor = Color.gray},
                fontStyle = FontStyle.Italic,
                alignment = TextAnchor.MiddleCenter
            };
        }
        
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            DrawInspector();

            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(_target);
            }
        }

        private void DrawInspector()
        {
            if (GUILayout.Button("Setup Input Manager"))
            {
                SetupInputManager();
            }

            EditorGUILayout.LabelField("Input Events Configuration", _headerStyle);
            var inputEvents = serializedObject.FindProperty("InputEvents");
            var count = inputEvents.arraySize;

            if (count > 0)
            {
                EditorGUILayout.BeginVertical(UnityEngine.GUI.skin.box);
                {
                    for (var i = 0; i < count; i++)
                    {
                        EditorGUILayout.BeginHorizontal(UnityEngine.GUI.skin.box);
                        {
                            var config = inputEvents.GetArrayElementAtIndex(i);
                            var button = config.FindPropertyRelative("Button");
                            var action = config.FindPropertyRelative("Action");
                            var label = string.Format("{0} : {1}", (GamepadButton) button.intValue, (GameActionType) action.intValue);

                            EditorGUILayout.BeginHorizontal();
                            {
                                UnityEngine.GUI.DrawTexture(GUILayoutUtility.GetRect(100f, 100f), AssetDatabase.LoadAssetAtPath<Texture>(GetImagePath((GamepadButton) button.intValue)), ScaleMode.ScaleToFit);

                                EditorGUILayout.BeginVertical();
                                {
                                    EditorGUILayout.LabelField(label, _labelStyle);
                                    EditorGUILayout.PropertyField(action);
                                }
                                EditorGUILayout.EndVertical();
                            }
                            EditorGUILayout.EndHorizontal();
                        }
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.Space();
                    }
                }
                EditorGUILayout.EndVertical();
            }
        }

        private string GetImagePath(GamepadButton button)
        {
            string path;
            if (_buttonImagesDictionary.TryGetValue(button, out path))
            {
                return path;
            }

            return string.Empty;
        }

        private static void SetupInputManager()
        {
            var serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);

            AddAxis(serializedObject, new InputAxis { Name = GamepadInputConfig.XboxAButton, PositiveButton = JoystickButton0, Sensitivity = 1f, Type = AxisType.KeyOrMouseButton, Axis = 1, JoyNum = 1 });
            AddAxis(serializedObject, new InputAxis { Name = GamepadInputConfig.XboxBButton, PositiveButton = JoystickButton1, Sensitivity = 1f, Type = AxisType.KeyOrMouseButton, Axis = 1, JoyNum = 1 });
            AddAxis(serializedObject, new InputAxis { Name = GamepadInputConfig.XboxXButton, PositiveButton = JoystickButton2, Sensitivity = 1f, Type = AxisType.KeyOrMouseButton, Axis = 1, JoyNum = 1 });
            AddAxis(serializedObject, new InputAxis { Name = GamepadInputConfig.XboxYButton, PositiveButton = JoystickButton3, Sensitivity = 1f, Type = AxisType.KeyOrMouseButton, Axis = 1, JoyNum = 1 });
            AddAxis(serializedObject, new InputAxis { Name = GamepadInputConfig.XboxLeftBumper, PositiveButton = JoystickButton4, Sensitivity = 1f, Type = AxisType.KeyOrMouseButton, Axis = 1, JoyNum = 1 });
            AddAxis(serializedObject, new InputAxis { Name = GamepadInputConfig.XboxRightBumper, PositiveButton = JoystickButton5, Sensitivity = 1f, Type = AxisType.KeyOrMouseButton, Axis = 1, JoyNum = 1 });
            AddAxis(serializedObject, new InputAxis { Name = GamepadInputConfig.XboxBackButton, PositiveButton = JoystickButton6, Sensitivity = 1f, Type = AxisType.KeyOrMouseButton, Axis = 1, JoyNum = 1 });
            AddAxis(serializedObject, new InputAxis { Name = GamepadInputConfig.XboxStartButton, PositiveButton = JoystickButton7, Sensitivity = 1f, Type = AxisType.KeyOrMouseButton, Axis = 1, JoyNum = 1 });
            AddAxis(serializedObject, new InputAxis { Name = GamepadInputConfig.XboxLeftStickXAxis, Sensitivity = 1f, Type = AxisType.JoystickAxis, Axis = 1, JoyNum = 1 });
            AddAxis(serializedObject, new InputAxis { Name = GamepadInputConfig.XboxLeftStickYAxis, Sensitivity = 1f, Type = AxisType.JoystickAxis, Axis = 2, JoyNum = 1 });
            AddAxis(serializedObject, new InputAxis { Name = GamepadInputConfig.XboxRightStickXAxis, Sensitivity = 1f, Type = AxisType.JoystickAxis, Axis = 4, JoyNum = 1 });
            AddAxis(serializedObject, new InputAxis { Name = GamepadInputConfig.XboxRightStickYAxis, Sensitivity = 1f, Type = AxisType.JoystickAxis, Axis = 5, JoyNum = 1 });
            AddAxis(serializedObject, new InputAxis { Name = GamepadInputConfig.XboxDPadXAxis, Sensitivity = 1f, Type = AxisType.JoystickAxis, Axis = 6, JoyNum = 1 });
            AddAxis(serializedObject, new InputAxis { Name = GamepadInputConfig.XboxDPadYAxis, Sensitivity = 1f, Type = AxisType.JoystickAxis, Axis = 7, JoyNum = 1 });
            AddAxis(serializedObject, new InputAxis { Name = GamepadInputConfig.XboxLeftTrigger, Sensitivity = 1f, Type = AxisType.JoystickAxis, Axis = 9, JoyNum = 1 });
            AddAxis(serializedObject, new InputAxis { Name = GamepadInputConfig.XboxRightTrigger, Sensitivity = 1f, Type = AxisType.JoystickAxis, Axis = 10, JoyNum = 1 });

            AddAxis(serializedObject, new InputAxis { Name = GamepadInputConfig.Ps4XButton, PositiveButton = JoystickButton1, Sensitivity = 1f, Type = AxisType.KeyOrMouseButton, Axis = 1, JoyNum = 1 });
            AddAxis(serializedObject, new InputAxis { Name = GamepadInputConfig.Ps4CircleButton, PositiveButton = JoystickButton2, Sensitivity = 1f, Type = AxisType.KeyOrMouseButton, Axis = 1, JoyNum = 1 });
            AddAxis(serializedObject, new InputAxis { Name = GamepadInputConfig.Ps4SquareButton, PositiveButton = JoystickButton0, Sensitivity = 1f, Type = AxisType.KeyOrMouseButton, Axis = 1, JoyNum = 1 });
            AddAxis(serializedObject, new InputAxis { Name = GamepadInputConfig.Ps4TriangleButton, PositiveButton = JoystickButton3, Sensitivity = 1f, Type = AxisType.KeyOrMouseButton, Axis = 1, JoyNum = 1 });
            AddAxis(serializedObject, new InputAxis { Name = GamepadInputConfig.Ps4LeftBumper, PositiveButton = JoystickButton4, Sensitivity = 1f, Type = AxisType.KeyOrMouseButton, Axis = 1, JoyNum = 1 });
            AddAxis(serializedObject, new InputAxis { Name = GamepadInputConfig.Ps4RightBumper, PositiveButton = JoystickButton5, Sensitivity = 1f, Type = AxisType.KeyOrMouseButton, Axis = 1, JoyNum = 1 });
            AddAxis(serializedObject, new InputAxis { Name = GamepadInputConfig.Ps4ShareButton, PositiveButton = JoystickButton8, Sensitivity = 1f, Type = AxisType.KeyOrMouseButton, Axis = 1, JoyNum = 1 });
            AddAxis(serializedObject, new InputAxis { Name = GamepadInputConfig.Ps4OptionsButton, PositiveButton = JoystickButton9, Sensitivity = 1f, Type = AxisType.KeyOrMouseButton, Axis = 1, JoyNum = 1 });
            AddAxis(serializedObject, new InputAxis { Name = GamepadInputConfig.Ps4LeftStickXAxis, Sensitivity = 1f, Type = AxisType.JoystickAxis, Axis = 1, JoyNum = 1 });
            AddAxis(serializedObject, new InputAxis { Name = GamepadInputConfig.Ps4LeftStickYAxis, Sensitivity = 1f, Type = AxisType.JoystickAxis, Axis = 2, JoyNum = 1 });
            AddAxis(serializedObject, new InputAxis { Name = GamepadInputConfig.Ps4RightStickXAxis, Sensitivity = 1f, Type = AxisType.JoystickAxis, Axis = 3, JoyNum = 1 });
            AddAxis(serializedObject, new InputAxis { Name = GamepadInputConfig.Ps4RightStickYAxis, Sensitivity = 1f, Type = AxisType.JoystickAxis, Axis = 6, JoyNum = 1 });
            AddAxis(serializedObject, new InputAxis { Name = GamepadInputConfig.Ps4DPadXAxis, Sensitivity = 1f, Type = AxisType.JoystickAxis, Axis = 7, JoyNum = 1 });
            AddAxis(serializedObject, new InputAxis { Name = GamepadInputConfig.Ps4DPadYAxis, Sensitivity = 1f, Type = AxisType.JoystickAxis, Axis = 8, JoyNum = 1 });
            AddAxis(serializedObject, new InputAxis { Name = GamepadInputConfig.Ps4LeftTrigger, Sensitivity = 1f, Type = AxisType.JoystickAxis, Axis = 4, JoyNum = 1 });
            AddAxis(serializedObject, new InputAxis { Name = GamepadInputConfig.Ps4RightTrigger, Sensitivity = 1f, Type = AxisType.JoystickAxis, Axis = 5, JoyNum = 1 });

            EditorUtility.DisplayDialog("Gamepad Input", "Gamepad Input has been successfully set up!", "Ok");
        }

        private static void AddAxis(SerializedObject serializedObject, InputAxis axis)
        {
            if (AxisDefined(serializedObject, axis.Name))
            {
                return;
            }

            var axesProperty = serializedObject.FindProperty("m_Axes");
            axesProperty.arraySize++;
            serializedObject.ApplyModifiedProperties();

            var axisProperty = axesProperty.GetArrayElementAtIndex(axesProperty.arraySize - 1);
            GetChildProperty(axisProperty, "m_Name").stringValue = axis.Name;
            GetChildProperty(axisProperty, "descriptiveName").stringValue = axis.DescriptiveName;
            GetChildProperty(axisProperty, "descriptiveNegativeName").stringValue = axis.DescriptiveNegativeName;
            GetChildProperty(axisProperty, "negativeButton").stringValue = axis.NegativeButton;
            GetChildProperty(axisProperty, "positiveButton").stringValue = axis.PositiveButton;
            GetChildProperty(axisProperty, "altNegativeButton").stringValue = axis.AltNegativeButton;
            GetChildProperty(axisProperty, "altPositiveButton").stringValue = axis.AltPositiveButton;
            GetChildProperty(axisProperty, "gravity").floatValue = axis.Gravity;
            GetChildProperty(axisProperty, "dead").floatValue = axis.Dead;
            GetChildProperty(axisProperty, "sensitivity").floatValue = axis.Sensitivity;
            GetChildProperty(axisProperty, "snap").boolValue = axis.Snap;
            GetChildProperty(axisProperty, "invert").boolValue = axis.Invert;
            GetChildProperty(axisProperty, "type").intValue = (int) axis.Type;
            GetChildProperty(axisProperty, "axis").intValue = axis.Axis - 1;
            GetChildProperty(axisProperty, "joyNum").intValue = axis.JoyNum;

            serializedObject.ApplyModifiedProperties();
        }

        private static bool AxisDefined(SerializedObject serializedObject, string axisName)
        {
            var axesProperty = serializedObject.FindProperty("m_Axes");
            axesProperty.Next(true);
            axesProperty.Next(true);
            while (axesProperty.Next(false))
            {
                var axis = axesProperty.Copy();
                axis.Next(true);
                if (axis.stringValue == axisName)
                {
                    return true;
                }
            }

            return false;
        }

        private static SerializedProperty GetChildProperty(SerializedProperty parent, string name)
        {
            var child = parent.Copy();
            child.Next(true);
            do
            {
                if (child.name == name)
                {
                    return child;
                }
            } while (child.Next(false));

            return null;
        }
    }

    public enum AxisType
    {
        KeyOrMouseButton = 0,
        MouseMovement = 1,
        JoystickAxis = 2
    }

    [Serializable]
    public class InputAxis
    {
        public string Name;
        public string DescriptiveName;
        public string DescriptiveNegativeName;
        public string NegativeButton;
        public string PositiveButton;
        public string AltNegativeButton;
        public string AltPositiveButton;
        public float Gravity;
        public float Dead;
        public float Sensitivity;
        public bool Snap;
        public bool Invert;
        public AxisType Type;
        public int Axis;
        public int JoyNum;
    }
}