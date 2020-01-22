using System;

namespace SimpleAccess
{
    /// <summary>
    /// Mark a Entity property as Identity columns
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class KeyAttribute : Attribute
    {
        public string DbSequence { get; }
        public bool IsIdentity { get; set; }

        public KeyAttribute() { }

        public KeyAttribute(string dbSequence)
        {
            DbSequence = dbSequence;
        }
    }
}