using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using Microsoft.CSharp;

namespace SimpleClassCreator.Code_Factory
{
    public class DtoGenerator
    {
        public string AssemblyPath { get; set; }
        public string FileName { get; set; }
        public string BasePath { get; private set; }
        public Assembly AssemblyReference { get; private set; }
        private CSharpCodeProvider _compiler;

        public DtoGenerator(string assemblyPath)
        {
            AssemblyPath = assemblyPath;
            BasePath = Path.GetDirectoryName(assemblyPath);
            FileName = Path.GetFileName(assemblyPath);
            AssemblyReference = Assembly.LoadFile(AssemblyPath);
            _compiler = new CSharpCodeProvider();
        }

        public Type PrintClass(string className)
        {
            Type t = AssemblyReference.GetType(className, false, false);

            if (t != null)
                Console.WriteLine(t.FullName);
            else
                Console.WriteLine("The Class Named: [" + className + "] could not be found (null returned).");

            return t;
        }

        public AssemblyInfo GetClassProperties(string className)
        {
            AssemblyInfo asm = new AssemblyInfo();

            asm.Name = FileName;

            Type t = PrintClass(className);

            if (t == null)
                return asm;

            ClassInfo cInfo = asm.AddClass(className);

            foreach (System.Reflection.PropertyInfo pi in t.GetProperties())
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

        public void MakeDTO(string className)
        {
            Type t = PrintClass(className);

            if (t == null)
                return;

            StringBuilder sbC = new StringBuilder();
            StringBuilder sbT = new StringBuilder();

            string strClassName = t.Name + "DTO";

            sbC.AppendLine("[DataContract]");
            sbC.Append("public class ").AppendLine(strClassName);
            sbC.AppendLine("{");

            sbT.Append("public ").Append(strClassName).Append(" Translate(").Append(t.Name).AppendLine(" obj)");
            sbT.AppendLine("{");
            sbT.Append(strClassName).Append(" dto = new ").Append(strClassName).AppendLine("();");
            sbT.AppendLine();

            Type tp;

            foreach (System.Reflection.PropertyInfo pi in t.GetProperties())
            {
                Console.WriteLine(pi.Name);

                tp = pi.PropertyType;

                sbC.AppendLine("[DataMember]");
                sbC.Append("public ").Append(GetTypeAsString(tp));

                //TODO: This is totally optional. Convert "System.Nullable<T>" to "T?" Where T is a generic type

                sbC.Append(" ").Append(pi.Name).AppendLine(" { get; set; } ");

                sbT.Append("dto.").Append(pi.Name).Append(" = obj.").Append(pi.Name).AppendLine(";");
            }

            sbC.AppendLine();

            sbT.AppendLine("return dto;");
            sbT.AppendLine("}");

            sbC.Append(sbT);
            sbC.AppendLine("}");

            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(Path.Combine(BasePath, t.Name + ".cs"), false))
            {
                sw.Write(sbC.ToString());
            }
        }

        public string GetTypeAsString(Type target)
        {
            return _compiler.GetTypeOutput(new CodeTypeReference(target));
        }

        public void PrintClasses()
        {
            //D:\Dev\fsrtfs01.fmp.local\Connect\Source Code\Dev\Connect.Entities\UnitsResidentsBase.generated.cs


            if (true) ;

            int i = 0;

            //This is a dangerous call for any large assemblies
            foreach (Type type in AssemblyReference.GetTypes())
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