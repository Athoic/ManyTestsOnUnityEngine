using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace Editor 
{
    public class EditorTools
    {
        [MenuItem("EditorTools/CollectTagsAndLayers", false, 1)]
        public static void CollectTagsAndLayers()
        {
            string[] tags= InternalEditorUtility.tags;



            string[] layers=InternalEditorUtility.layers;
        }

        private const string TAGS_DEFINE_PATH = @"Assets\Scripts\Define\TagDefines";
        private static void GenTagsEnum()
        {

        }


    }


    /// <summary>
    /// 帮助窗口
    /// </summary>
    public class HelpWindow : EditorWindow
    {
        HelpWindow()
        {
            this.titleContent = new GUIContent("Help");
        }

        private void OnGUI()
        {
            GUILayout.BeginVertical();

            GUILayout.Space(10);
            GUI.skin.label.fontSize = 12;
            GUI.skin.label.alignment = TextAnchor.UpperLeft;
            GUILayout.Label(
                "F1 帮助\n" +
                "F5 刷新并运行\n" +
                "Alt+A  物体显隐\n" +
                "Alt+D  删除MissingScripts\n" +
                "Alt+T  修改TimeScale(x1,x2,x10)\n"
                );
        }
    }

}

