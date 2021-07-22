using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Text;
using Editor.CodeGenerator;

namespace Editor 
{
    public class EditorTools
    {
        private const string TAG_ENUM_FILE = "TagDefines";
        private const string LAYER_ENUM_FILE = "LayerDefines";

        [MenuItem("EditorTools/CollectTagsAndLayers", false, 1)]
        public static void CollectTagsAndLayers()
        {
            StringKeyCodeGenerator.GenEnumCode(TAG_ENUM_FILE, InternalEditorUtility.tags);
            StringKeyCodeGenerator.GenEnumCode(LAYER_ENUM_FILE, InternalEditorUtility.layers);
            Debug.Log("Tag和Layer的string key生成完成");
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

