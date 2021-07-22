using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor.CodeGenerator
{
    public class EnumCodeGenerator
    {
        private const string ENUM_GEN_PATH= "Assets\\Scripts\\Define\\{0}.cs";

        public static void GenEnumCode(string enumName, string[] enums)
        {
            List<string> list = new List<string>();
            for (int i = 0, count = enums.Length; i < count; i++)
                list.Add(enums[i]);
            GenEnumCode(enumName, list);
        }

        public static void GenEnumCode(string enumName, List<string> enums)
        {
            StringBuilder enumCodestringBuilder = new StringBuilder();
            enumCodestringBuilder.Append("namespace Define.Enum\n{\n");
            enumCodestringBuilder.Append($"    public enum {enumName}\n");
            enumCodestringBuilder.Append("    {\n");
            for (int i = 0, count = enums.Count; i < count; i++)
            {
                enumCodestringBuilder.Append($"        {enums[i].Replace(" ","")},\n\n");
            }
            enumCodestringBuilder.Append("    }\n}");

            string path = string.Format(ENUM_GEN_PATH, enumName);
            using (FileStream fileStream = new FileStream(path,FileMode.OpenOrCreate))
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
