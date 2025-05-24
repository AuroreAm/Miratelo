using System;
using System.Linq;
using System.Reflection;

namespace Pixify
{
    public class NodeFormelyNamedAttribute : Attribute
    {
        public string name;

        public NodeFormelyNamedAttribute ( string name )
        {
            this.name = name;
        }
    }

    public static class ClassMigration
    {
        public static bool TypeWithFormer(string FormerName, out Type type)
        {
            FormerName = FormerName.Substring(0, FormerName.IndexOf(","));
            var NodeTypeList = typeof(action).Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(action)));
            foreach (var a in NodeTypeList)
            {
                if (a.GetCustomAttribute<NodeFormelyNamedAttribute>() != null && a.GetCustomAttribute<NodeFormelyNamedAttribute>().name == FormerName)
                {
                    type = a;
                    return true;
                }
            }
            type = null;
            return false;
        }
    }
}
