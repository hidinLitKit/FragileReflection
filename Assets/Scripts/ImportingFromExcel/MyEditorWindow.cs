using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace FragileReflection
{
    public class MyEditorWindow : EditorWindow
    {
        [MenuItem("Window/Excel Helper")]
        public static void ShowExample()
        {
            MyEditorWindow wnd = GetWindow<MyEditorWindow>();
            wnd.titleContent = new GUIContent("Excel Helper");
        }

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            // Create button
            Button button = new Button();
            button.name = "button";
            button.text = "Import Excel Data";
            root.Add(button);

            button.clicked += print;           
        }

        private void print()
        {
            GoogleSheetLoader.DownloadTable();
        }
    }
}
