using System;

namespace SimpleAccess
{
    /// <summary>
    /// Mark a Entity property as Identity columns
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKeyAttribute : Attribute
    {
        public string DbSequence { get; }
        public bool IsIdentity { get; set; }
        public UniqueIdGeneration UniqueIdGeneration { get; set; }


        public PrimaryKeyAttribute() { }

        public PrimaryKeyAttribute(string dbSequence)
        {
            DbSequence = dbSequence;
        }

        public PrimaryKeyAttribute(UniqueIdGeneration uniqueIdGeneration)
        {
            UniqueIdGeneration = uniqueIdGeneration;
        }
    }
    public enum UniqueIdGeneration
    { 
        None, Client, Database
    }
}