using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System;
using System.Linq;

[CustomEditor(typeof(BoardTemplate))]
public class BoardTemplateInspector : Editor
{
    private BoardTemplate _template;
    private ElementPair _brush;
    private Vector2 scrollPosition;
    private bool showTiles;
    private ReorderableList tilesList;

    private void OnEnable()
    {
        _template = (BoardTemplate)target;
        List<ElementPair> pairs = _template.Tiles;
        tilesList = CreateList(serializedObject, serializedObject.FindProperty("_tiles"), "tiles", "element", 5);
    }

    public override void OnInspectorGUI()
    {
        int w = 0;
        int h = 0;
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Size: ", GUILayout.Width(45));
        w = Mathf.Clamp(EditorGUILayout.IntField(_template.Cells.Count, GUILayout.Width(45)), 1, 25);
        EditorGUILayout.LabelField("X", GUILayout.Width(15));
        h = Mathf.Clamp(EditorGUILayout.IntField(_template.Cells[0].raw.Count, GUILayout.Width(45)), 1, 25);
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        if (w!= _template.Cells.Count || h!= _template.Cells[0].raw.Count)
        {
            _template.Setize(w, h);
            EditorUtility.SetDirty(_template);
        }

        EditorGUILayout.LabelField(_template.SnakesCount + " players");

        for (int i = 0; i < _template.Cells.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int j = 0; j < _template.Cells[0].raw.Count; j++)
            {
                if(GUILayout.Button(_template.Cells[i].raw[j].image.texture, GUIStyle.none, GUILayout.Width(16), GUILayout.Height(16)))
                {
                    _template.SetCell(_brush, i, j);
                    EditorUtility.SetDirty(_template);
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        scrollPosition =  EditorGUILayout.BeginScrollView(scrollPosition, GUI.skin.horizontalScrollbar, GUIStyle.none , GUILayout.Height(45));
        EditorGUILayout.BeginHorizontal();

        foreach(ElementPair ep in _template.Tiles)
        {
            if (DefaultResources.GetElementByEnum(ep.element)!=null)
            {
                if (GUILayout.Button(ep.image.texture, GUILayout.Width(30), GUILayout.Height(30)))
                {
                    _brush = ep;
                }
            }       
        }


   

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndScrollView();

        if (_brush!=null)
        {
            GUILayout.Label(_brush.image.texture, GUILayout.Width(50), GUILayout.Height(50));
        }

        showTiles = EditorGUILayout.Foldout(showTiles, "Tiles");
        if(showTiles)
        {
            tilesList.DoLayoutList();
        }
        
    }

    ReorderableList CreateList(
        SerializedObject obj,
        SerializedProperty prop,
        string label,
        string elementLabelPropertyName,
        int expandedLines
    )
    {
        ReorderableList list = new ReorderableList(obj, prop, true, true, true, true);

        list.drawHeaderCallback = rect => {
            EditorGUI.LabelField(rect, label);
        };

        // Initialize a temporary list of element heights to be used later on in the draw function
        List<float> heights = new List<float>(prop.arraySize);

        // Main draw callback for the reorderable list
        list.drawElementCallback = (rect, index, active, focused) => {
            SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);
            //            Sprite s = (element.objectReferenceValue as GameObject);

            float height = EditorGUIUtility.singleLineHeight * 1.25f; // multiply by 1.25 to give each property a little breathing room
           
            // Manage heights of each element
            /// TODO: heights should really based on the GetPropertyHeight of property type, rather
            /// than some random function parameter that we input, but I can't get GetPropertyHeight
            /// to be properly here... at least for custom property drawers.
            try
            {
                heights[index] = height;
            }
            catch (ArgumentOutOfRangeException e)
            {
                Debug.LogWarning(e.Message);
            }
            finally
            {
                float[] floats = heights.ToArray();
                Array.Resize(ref floats, prop.arraySize);
                heights = floats.ToList();
            }

            // Add a bit of padding to the top of each element
            rect.y += 2;

            Rect rect2 = new Rect(rect.min.x+ rect.width / 2, rect.min.y, rect.width / 2, rect.height);
            rect = new Rect(rect.min.x, rect.min.y, rect.width/2, rect.height);

                EditorGUI.PropertyField(rect, element.FindPropertyRelative("element"), GUIContent.none );
            EditorGUI.PropertyField(rect2, element.FindPropertyRelative("image"),GUIContent.none);

        };

        // Adjust heights based on whether or not an element is selected.
        list.elementHeightCallback = (index) => {
            Repaint();
            float height = 0;

            try
            {
                height = heights[index];
            }
            catch (ArgumentOutOfRangeException e)
            {
                Debug.LogWarning(e.Message);
            }
            finally
            {
                float[] floats = heights.ToArray();
                Array.Resize(ref floats, prop.arraySize);
                heights = floats.ToList();
            }

            return height;
        };

        // Set the color of the selected list item
        list.drawElementBackgroundCallback = (rect, index, active, focused) => {
            rect.height = heights[index];
            Texture2D tex = new Texture2D(1, 1);
            tex.SetPixel(0, 0, new Color(0.1f, 0.33f, 1f, 0.33f));
            tex.Apply();
            if (active)
                GUI.DrawTexture(rect, tex as Texture);
        };

        /* 
        /// Uncomment this section if you want a little dropdown list for the 
        /// "add element to list" button.
                list.onAddDropdownCallback = ( rect, li ) => {
                    var menu = new GenericMenu();
                    menu.AddItem(new GUIContent("Add Element"), false, () => {
                        serializedObject.Update();
                        li.serializedProperty.arraySize++;
                        serializedObject.ApplyModifiedProperties();
                    });
                    menu.ShowAsContext();
                    float[] floats = heights.ToArray();
                    Array.Resize(ref floats, prop.arraySize);
                    heights = floats.ToList();
                };
        */
        return list;
    }
}
