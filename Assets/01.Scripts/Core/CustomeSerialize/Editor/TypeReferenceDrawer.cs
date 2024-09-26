#if UNITY_EDITOR
using UnityEditor;
using System;
using System.Linq;
using UnityEngine;

[CustomPropertyDrawer(typeof(TypeReference))]
public class TypeReferenceDrawer : PropertyDrawer
{
    private Type GetBaseType(SerializedProperty property)
    {
        var fieldInfo = property.serializedObject.targetObject.GetType().GetField(property.propertyPath.Split('.')[0]);
        if (fieldInfo != null)
        {
            var attribute = fieldInfo.GetCustomAttributes(typeof(TypeFilterAttribute), false).FirstOrDefault() as TypeFilterAttribute;
            if (attribute != null)
            {
                return attribute.BaseType;
            }
        }
        return typeof(object); // 기본값으로 object 반환
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        var typeNameProperty = property.FindPropertyRelative("typeName");
        var baseType = GetBaseType(property);

        var type = !string.IsNullOrEmpty(typeNameProperty.stringValue)
            ? Type.GetType(typeNameProperty.stringValue)
            : null;

        var rect = EditorGUI.PrefixLabel(position, label);
        if (EditorGUI.DropdownButton(rect, new GUIContent(type != null ? type.Name : "None"), FocusType.Keyboard))
        {
            var menu = new GenericMenu();
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.BaseType == baseType && !t.IsAbstract && !t.IsInterface); // 직계 자식만 필터링

            foreach (var t in types)
            {
                var t1 = t;
                menu.AddItem(new GUIContent(t.FullName), t == type, () =>
                {
                    typeNameProperty.stringValue = t1.AssemblyQualifiedName;
                    property.serializedObject.ApplyModifiedProperties();
                });
            }

            menu.ShowAsContext();
        }

        EditorGUI.EndProperty();
    }
}
#endif
