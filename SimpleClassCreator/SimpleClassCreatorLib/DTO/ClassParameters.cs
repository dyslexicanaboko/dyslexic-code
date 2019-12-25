namespace SimpleClassCreator.DTO
{
    public class ClassParameters
    {
        public string ConnectionString { get; set; }
        public SourceTypeEnum SourceType { get; set; }
        public string ClassSource { get; set; }
        public CodeType LanguageType { get; set; }
        public bool IncludeWcfTags { get; set; }
        public bool IncludeCloneMethod { get; set; }
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
        public bool IncludeSerializeablePropertiesOnly { get; set; }
        public bool ExcludeCollections { get; set; }
        public bool IncludeTranslateMethod { get; set; }
    }
}
