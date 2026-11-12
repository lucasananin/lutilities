using UnityEditor;
using UnityEngine;
using lutilities.TagCollections;

[CustomPropertyDrawer(typeof(TagCollection.TagEntry))]
public class TagEntryDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var valueProperty = property.FindPropertyRelative("_value");

        valueProperty.stringValue = EditorGUI.TagField(
            position,
            label,
            valueProperty.stringValue
        );
    }
}
