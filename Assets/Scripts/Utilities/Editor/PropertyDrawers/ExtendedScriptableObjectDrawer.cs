// Developed by Tom Kail at Inkle
// Released under the MIT Licence as held at https://opensource.org/licenses/MIT

// Must be placed within a folder named "Editor"

using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Utilities.Editor.PropertyDrawers
{
    /// <summary>
    ///     Extends how ScriptableObject object references are displayed in the inspector
    ///     Shows you all values under the object reference
    ///     Also provides a button to create a new ScriptableObject if property is null.
    /// </summary>
    [CustomPropertyDrawer(typeof(ScriptableObject), true)]
    public class ExtendedScriptableObjectDrawer : PropertyDrawer
    {
        private const int ButtonWidth = 66;

        private static readonly List<string> IgnoreClassFullNames = new() {"TMPro.TMP_FontAsset"};

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float totalHeight = EditorGUIUtility.singleLineHeight;
            if (property.objectReferenceValue == null || !AreAnySubPropertiesVisible(property))
                return totalHeight;
            if (!property.isExpanded)
                return totalHeight;

            var data = property.objectReferenceValue as ScriptableObject;
            if (data == null)
                return EditorGUIUtility.singleLineHeight;

            var serializedObject = new SerializedObject(data);
            SerializedProperty prop = serializedObject.GetIterator();
            if (prop.NextVisible(true))
                do
                {
                    if (prop.name == "m_Script") continue;
                    SerializedProperty subProp = serializedObject.FindProperty(prop.name);
                    float height = EditorGUI.GetPropertyHeight(subProp, null, true) +
                                   EditorGUIUtility.standardVerticalSpacing;
                    totalHeight += height;
                } while (prop.NextVisible(false));

            // Add a tiny bit of height if open for the background
            totalHeight += EditorGUIUtility.standardVerticalSpacing;

            return totalHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            Type type = GetFieldType();

            if (type == null || IgnoreClassFullNames.Contains(type.FullName))
            {
                EditorGUI.PropertyField(position, property, label);
                EditorGUI.EndProperty();
                return;
            }

            ScriptableObject propertySo = null;
            if (!property.hasMultipleDifferentValues && property.serializedObject.targetObject != null &&
                property.serializedObject.targetObject is ScriptableObject targetObject)
                propertySo = targetObject;

            var guiContent = new GUIContent(property.displayName);
            var foldoutRect = new Rect(position.x, position.y, EditorGUIUtility.labelWidth,
                EditorGUIUtility.singleLineHeight);
            if (property.objectReferenceValue != null && AreAnySubPropertiesVisible(property))
            {
                property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, guiContent, true);
            }
            else
            {
                // So yeah having a foldout look like a label is a weird hack 
                // but both code paths seem to need to be a foldout or 
                // the object field control goes weird when the code path changes.
                // I guess because foldout is an interactable control of its own and throws off the controlID?
                foldoutRect.x += 12;
                EditorGUI.Foldout(foldoutRect, property.isExpanded, guiContent, true, EditorStyles.label);
            }

            Rect indentedPosition = EditorGUI.IndentedRect(position);
            float indentOffset = indentedPosition.x - position.x;
            var propertyRect = new Rect(position.x + (EditorGUIUtility.labelWidth - indentOffset), position.y,
                position.width - (EditorGUIUtility.labelWidth - indentOffset), EditorGUIUtility.singleLineHeight);

            if (propertySo != null || property.objectReferenceValue == null) propertyRect.width -= ButtonWidth;

            EditorGUI.ObjectField(propertyRect, property, type, GUIContent.none);
            if (GUI.changed) property.serializedObject.ApplyModifiedProperties();

            var buttonRect = new Rect(position.x + position.width - ButtonWidth, position.y, ButtonWidth,
                EditorGUIUtility.singleLineHeight);

            if (property.propertyType == SerializedPropertyType.ObjectReference &&
                property.objectReferenceValue != null)
            {
                var data = (ScriptableObject) property.objectReferenceValue;

                if (property.isExpanded)
                {
                    // Draw a background that shows us clearly which fields are part of the ScriptableObject
                    GUI.Box(
                        new Rect(0,
                            position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing -
                            1,
                            Screen.width,
                            position.height - EditorGUIUtility.singleLineHeight -
                            EditorGUIUtility.standardVerticalSpacing),
                        "");

                    EditorGUI.indentLevel++;
                    var serializedObject = new SerializedObject(data);

                    // Iterate over all the values and draw them
                    SerializedProperty prop = serializedObject.GetIterator();
                    float y = position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                    if (prop.NextVisible(true))
                        do
                        {
                            // Don't bother drawing the class file
                            if (prop.name == "m_Script") continue;
                            float height = EditorGUI.GetPropertyHeight(prop, new GUIContent(prop.displayName), true);
                            EditorGUI.PropertyField(new Rect(position.x, y, position.width - ButtonWidth, height), prop,
                                true);
                            y += height + EditorGUIUtility.standardVerticalSpacing;
                        } while (prop.NextVisible(false));

                    if (GUI.changed)
                        serializedObject.ApplyModifiedProperties();

                    EditorGUI.indentLevel--;
                }
            }
            else
            {
                if (GUI.Button(buttonRect, "Create"))
                {
                    var selectedAssetPath = "Assets";
                    if (property.serializedObject.targetObject is MonoBehaviour behaviour)
                    {
                        MonoScript ms = MonoScript.FromMonoBehaviour(behaviour);
                        selectedAssetPath = Path.GetDirectoryName(AssetDatabase.GetAssetPath(ms));
                    }

                    property.objectReferenceValue = CreateAssetWithSavePrompt(type, selectedAssetPath);
                }
            }

            property.serializedObject.ApplyModifiedProperties();
            EditorGUI.EndProperty();
        }

        // Creates a new ScriptableObject via the default Save File panel
        private static ScriptableObject CreateAssetWithSavePrompt(Type type, string path)
        {
            path = EditorUtility.SaveFilePanelInProject("Save ScriptableObject", type.Name + ".asset", "asset",
                "Enter a file name for the ScriptableObject.", path);
            if (path == "") return null;
            var asset = ScriptableObject.CreateInstance(type);
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
            EditorGUIUtility.PingObject(asset);
            return asset;
        }

        private Type GetFieldType()
        {
            Type type = fieldInfo.FieldType;
            if (type.IsArray) type = type.GetElementType();
            else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
                type = type.GetGenericArguments()[0];
            return type;
        }

        private static bool AreAnySubPropertiesVisible(SerializedProperty property)
        {
            var data = (ScriptableObject) property.objectReferenceValue;
            var serializedObject = new SerializedObject(data);
            SerializedProperty prop = serializedObject.GetIterator();
            while (prop.NextVisible(true))
            {
                if (prop.name == "m_Script") continue;
                return true; //if theres any visible property other than m_script
            }

            return false;
        }
    }
}