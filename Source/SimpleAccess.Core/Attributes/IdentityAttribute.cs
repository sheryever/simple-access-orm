using System;

namespace SimpleAccess
{
    /// <summary>
    /// Mark a Entity property as Identity columns
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class IdentityAttribute : KeyAttribute
    {
        public string DbSequence { get; }

        public IdentityAttribute() { }

        public IdentityAttribute(string dbSequence)
        {
            DbSequence = dbSequence;
        }
    }
}