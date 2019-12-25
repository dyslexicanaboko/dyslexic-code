using System;
using System.CodeDom;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.CSharp;
using SimpleClassCreator.DTO;

namespace SimpleClassCreator.Code_Factory
{
    public class DtoGenerator
    {
        private readonly CSharpCodeProvider _compiler;

        public DtoGenerator(string assemblyPath)
        {
            AssemblyPath = assemblyPath;

            BasePath = Path.GetDirectoryName(assemblyPath);

            FileName = Path.GetFileName(assemblyPath);

            AssemblyReference = Assembly.LoadFile(AssemblyPath);

            _compiler = new CSharpCodeProvider();
        }

        public string AssemblyPath { get; set; }
        public string FileName { get; set; }
        public string BasePath { get; }
        public Assembly AssemblyReference { get; }

        public Type PrintClass(string className)
        {
            var t = AssemblyReference.GetType(className, false, false);

            if (t != null)
                Console.WriteLine(t.FullName);
            else
                Console.WriteLine("The Class Named: [" + className + "] could not be found (null returned).");

            return t;
        }

        public AssemblyInfo GetClassProperties(string className)
        {
            var asm = new AssemblyInfo();

            asm.Name = FileName;

            var t = PrintClass(className);

            if (t == null)
                return asm;

            var cInfo = asm.AddClass(className);

            foreach (var pi in t.GetProperties())
            {
                Console.WriteLine(pi.Name);

                cInfo.Properties.Add(new PropertyInfo
                {
                    Name = pi.Name,
                    TypeName = GetTypeAsString(pi.PropertyType),
                    IsSerializable = pi.PropertyType.IsDefined(typeof(SerializableAttribute), false)
                });
            }

            return asm;
        }

        public string MakeDto(string className, ClassParameters parameters)
        {
            var p = parameters;

            var t = PrintClass(className);

            if (t == null)
                return "Type cannot be null";

            var sbC = new StringBuilder(); //Class
            var sbTm = new StringBuilder(); //Translate method
            var sbCm = new StringBuilder(); //Clone method

            var cn = t.Name + "Dto";

            var wcfOk = p.IncludeSerializeablePropertiesOnly && p.IncludeWcfTags;

            //Class declaration
            if (wcfOk) sbC.AppendLine("[DataContract]");

            sbC.Append("public class ").AppendLine(cn);
            sbC.AppendLine("{");

            //Translate method
            sbTm.Append("public ").Append(cn).Append(" Translate(").Append(t.Name).AppendLine(" obj)");
            sbTm.AppendLine("{");
            sbTm.Append("var dto = new ").Append(cn).AppendLine("();");
            sbTm.AppendLine();

            sbCm.Append("public ").Append(cn).AppendLine(" Clone()");
            sbCm.AppendLine("{");
            sbCm.Append("var c = new ").Append(cn).AppendLine("();");
            sbCm.AppendLine();

            foreach (var pi in t.GetProperties())
            {
                Console.WriteLine(pi.Name);

                var tp = pi.PropertyType;

                //Class properties
                if (wcfOk) sbC.AppendLine("[DataMember]");

                sbC.Append("public ").Append(GetTypeAsString(tp));

                sbC.Append(" ").Append(pi.Name).AppendLine(" { get; set; } ")
                    .AppendLine();

                //Translate method
                sbTm.Append("dto.").Append(pi.Name).Append(" = obj.").Append(pi.Name).AppendLine(";");

                //Translate method
                sbCm.Append("c.").Append(pi.Name).Append(" = ").Append(pi.Name).AppendLine(";");
            }

            sbC.AppendLine();

            //Translate method close
            sbTm.AppendLine("return dto;");
            sbTm.AppendLine("}");

            //Clone method close
            sbCm.AppendLine("return c;");
            sbCm.AppendLine("}");

            //Methods to include in the class
            if(p.IncludeTranslateMethod) sbC.Append(sbTm);

            if(p.IncludeCloneMethod) sbC.Append(sbCm);

            sbC.AppendLine("}");

            return sbC.ToString();
        }

        public string GetTypeAsString(Type target)
        {
            if (target.IsValueType && target.Name == "Nullable`1")
            {
                var typeT = target.GetGenericArguments();

                var typeAsString = GetTypeOutput(typeT[0]);

                var strNullableType = $"{typeAsString}?";

                return strNullableType;
            }

            var str = GetTypeOutput(target);

            return str;
        }

        private string GetTypeOutput(Type target)
        {
            var str = _compiler.GetTypeOutput(new CodeTypeReference(target));

            return str;
        }

        public void PrintClasses()
        {
            if (true) ;

            var i = 0;

            //This is a dangerous call for any large assemblies
            foreach (var type in AssemblyReference.GetTypes())
            {
                Console.WriteLine(type.FullName);

                i++;

                if (i >= 10)
                    break;
            }

            //Type type = asm.GetType("TestRunner");
        }
    }
}