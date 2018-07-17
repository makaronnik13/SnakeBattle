using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BoardTemplate))]
public class BoardTemplateInspector : Editor
{
    private BoardTemplate _template;
    private LogicElement.LogicElementType _brush = LogicElement.LogicElementType.None;
    private Vector2 scrollPosition;

    private void OnEnable()
    {
        _template = (BoardTemplate)target;
    }

    public override void OnInspectorGUI()
    {


        int w = 0;
        int h = 0;
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Size: ", GUILayout.Width(45));
        w = Mathf.Clamp(EditorGUILayout.IntField(_template.Cells.Count, GUILayout.Width(45)), 1, 25);
        EditorGUILayout.LabelField("X", GUILayout.Width(15));
        h = Mathf.Clamp(EditorGUILayout.IntField(_template.Cells[0].Count, GUILayout.Width(45)), 1, 25);
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        if (w!= _template.Cells.Count || h!= _template.Cells[0].Count)
        {
            _template.Setize(w, h);
            EditorUtility.SetDirty(_template);
        }

        EditorGUILayout.LabelField(_template.SnakesCount + " players");

        for (int i = 0; i < _template.Cells.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int j = 0; j < _template.Cells[0].Count; j++)
            {
                if(GUILayout.Button(DefaultResources.GetElementByEnum(_template.Cells[i][j]).Img.texture, GUIStyle.none, GUILayout.Width(16), GUILayout.Height(16)))
                {
                    _template.SetCell(_brush, i, j);
                    EditorUtility.SetDirty(_template);
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        scrollPosition =  EditorGUILayout.BeginScrollView(scrollPosition, GUI.skin.horizontalScrollbar, GUIStyle.none , GUILayout.Height(45));
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button(DefaultResources.GetElementByEnum(LogicElement.LogicElementType.None).Img.texture, GUILayout.Width(30), GUILayout.Height(30)))
        {
            _brush = LogicElement.LogicElementType.None;
        }
        if (GUILayout.Button(DefaultResources.GetElementByEnum(LogicElement.LogicElementType.Wall).Img.texture, GUILayout.Width(30), GUILayout.Height(30)))
        {
            _brush = LogicElement.LogicElementType.Wall;
        }
        if (GUILayout.Button(DefaultResources.GetElementByEnum(LogicElement.LogicElementType.MyHead).Img.texture, GUILayout.Width(30), GUILayout.Height(30)))
        {
            _brush = LogicElement.LogicElementType.MyHead;
        }
        if (GUILayout.Button(DefaultResources.GetElementByEnum(LogicElement.LogicElementType.MyBody).Img.texture, GUILayout.Width(30), GUILayout.Height(30)))
        {
            _brush = LogicElement.LogicElementType.MyBody;
        }
   

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndScrollView();

        GUILayout.Label(DefaultResources.GetElementByEnum(_brush).Img.texture, GUILayout.Width(50), GUILayout.Height(50));
        
    }
}
