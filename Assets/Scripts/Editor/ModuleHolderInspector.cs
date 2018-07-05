using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ModuleHolder))]
public class ModuleHolderInspector : Editor
{

    private ModuleHolder _holder;

    private void OnEnable()
    {
        _holder = (ModuleHolder)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();
        _holder.ModuleHolderName = EditorGUILayout.TextField("Name", _holder.ModuleHolderName);
        _holder.moduleType = (ModuleHolder.ModuleType)EditorGUILayout.EnumPopup("Type", _holder.moduleType);
        _holder.Cost = EditorGUILayout.IntField("Cost", _holder.Cost);
        _holder.Size = EditorGUILayout.IntField("Size", _holder.Size);
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical();
        _holder.Img = (Sprite)EditorGUILayout.ObjectField(_holder.Img, typeof(Sprite), false, GUILayout.Width(EditorGUIUtility.singleLineHeight*4), GUILayout.Height(EditorGUIUtility.singleLineHeight * 4));
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();

        if (_holder.moduleType == ModuleHolder.ModuleType.Simple)
        {
           _holder.Operator = (ModuleHolder.BaseModuleOperator)EditorGUILayout.EnumPopup("Operator", _holder.Operator);
        }
        else
        {
           _holder.LogicString = EditorGUILayout.TextField("logic", _holder.LogicString);
        }

    }
}
