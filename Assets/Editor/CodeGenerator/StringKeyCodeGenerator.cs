using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.CodeGenerator
{
    public class StringKeyCodeGenerator
    {
        private const string STRING_KEY_GEN_PATH = "Assets\\Scripts\\Define\\StringKey\\{0}.cs";

        public static void GenEnumCode(string className, string[] keys)
        {
            List<string> list = new List<string>();
            for (int i = 0, count = keys.Length; i < count; i++)
                list.Add(keys[i]);
            GenEnumCode(className, list);
        }

        public static void GenEnumCode(string className, List<string> keys)
        {
            StringBuilder enumCodestringBuilder = new StringBuilder();
            enumCodestringBuilder.Append("namespace Define.StringKey\n{\n");
            enumCodestringBuilder.Append($"    public class {className}\n");
            enumCodestringBuilder.Append("    {\n");
            for (int i = 0, count = keys.Count; i < count; i++)
            {
                enumCodestringBuilder.Append($"        public const string {keys[i].Replace(" ", "")} = \"{keys[i]}\";\n\n");
            }
            enumCodestringBuilder.Append("    }\n}");

            string path = string.Format(STRING_KEY_GEN_PATH, className);
            using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                StreamWriter streamWriter = new StreamWriter(fileStream);
                streamWriter.Write(enumCodestringBuilder.ToString());
                streamWriter.Flush();
                streamWriter.Close();
            }

            enumCodestringBuilder.Clear();
        }

    }
}
