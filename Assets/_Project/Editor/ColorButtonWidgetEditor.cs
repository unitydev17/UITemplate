using UITemplate.View;
using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(ColorButtonWidget))]
public class ColorButtonWidgetEditor : ButtonEditor
{
    private SerializedProperty _idleColor;
    private SerializedProperty _activeColor;

    protected override void OnEnable()
    {
        base.OnEnable();
        _idleColor = serializedObject.FindProperty("_idleColor");
        _activeColor = serializedObject.FindProperty("_activeColor");
    }

    public override void OnInspectorGUI()
    {
        // Begin drawing the inspector
        serializedObject.Update();

        // Draw the custom property field
        EditorGUILayout.PropertyField(_idleColor);
        EditorGUILayout.PropertyField(_activeColor);

        // Apply changes to the serializedProperty
        serializedObject.ApplyModifiedProperties();

        // Call the base class implementation
        base.OnInspectorGUI();
    }
}