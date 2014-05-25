using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleClassCreator.DTO
{
    public class ClassParameters
    {
        public string ConnectionString { get; set; }
        public SourceTypeEnum SourceType { get; set; }
        public string ClassSource { get; set; }
        public CodeType LanguageType { get; set; }
        public bool IncludeWCFTags { get; set; }
        public bool BuildOutClassProperties { get; set; }
        public bool IncludeMemberPrefix { get; set; }
        public string MemberPrefix { get; set; }
        public bool IncludeNamespace { get; set; }
        public string Namespace { get; set; }
        public string ClassName { get; set; }
        public bool SaveAsFile { get; set; }
        public bool ReplaceExisting { get; set; }
        public string Filepath { get; set; }
        public string Filename { get; set; }
    }
}
