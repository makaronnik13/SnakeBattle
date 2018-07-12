using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SnakeSkin))]
public class SnakeSkinEditor : Editor
{

    private SnakeSkin _skin;

    private void OnEnable()
    {
        _skin = (SnakeSkin)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        EditorGUILayout.BeginVertical();
        _skin.SkinName = EditorGUILayout.TextField("Name", _skin.SkinName);
        _skin.Base = EditorGUILayout.Toggle("BaseSkin", _skin.Base);
        if (!_skin.Base)
        {
            _skin.SkinCost = EditorGUILayout.IntField("Cost", _skin.SkinCost);
        }

        EditorGUILayout.EndVertical();
        GUILayout.FlexibleSpace();
        EditorGUILayout.BeginVertical();
        _skin.Tail = (Sprite)EditorGUILayout.ObjectField(_skin.Tail, typeof(Sprite), false, GUILayout.Width(EditorGUIUtility.singleLineHeight * 4), GUILayout.Height(EditorGUIUtility.singleLineHeight * 4));
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        _skin.Head = (Sprite)EditorGUILayout.ObjectField(_skin.Head, typeof(Sprite), false, GUILayout.Width(EditorGUIUtility.singleLineHeight * 4), GUILayout.Height(EditorGUIUtility.singleLineHeight * 4));
        _skin.Body = (Sprite)EditorGUILayout.ObjectField(_skin.Body, typeof(Sprite), false, GUILayout.Width(EditorGUIUtility.singleLineHeight * 4), GUILayout.Height(EditorGUIUtility.singleLineHeight * 4));
        _skin.Angle = (Sprite)EditorGUILayout.ObjectField(_skin.Angle, typeof(Sprite), false, GUILayout.Width(EditorGUIUtility.singleLineHeight * 4), GUILayout.Height(EditorGUIUtility.singleLineHeight * 4));
        EditorGUILayout.EndHorizontal();

        EditorUtility.SetDirty(_skin);
    }
}
