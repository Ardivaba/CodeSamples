using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityLoader;


namespace UnityLoader
{
    public static class Loader
    {
        public static void Load()
        {
            var plugins = GetTypesWithHelpAttribute(Assembly.GetExecutingAssembly());
            foreach (var plugin in plugins)
            {
                try
                {
                    SceneMgr.Get().gameObject.AddComponent(plugin);
                }
                catch
                {

                }
            }
        }

        public static void NewAppDomain()
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(AssemblyResolve);
        }

        private static Assembly AssemblyResolve(Object sender, ResolveEventArgs args)
        {
            String assemblyPath = @"C:\Users\Ardivaba\Documents\Visual Studio 2015\Projects\UnityLoader\UnityLoader\bin\Debug\UnityLoader.dll";
            if (!File.Exists(assemblyPath))
            {
                return null;
            }
            else
            {
                return Assembly.LoadFrom(assemblyPath);
            }
        }

        public static List<Type> GetTypesWithHelpAttribute(Assembly assembly)
        {
            List<Type> types = new List<Type>();
            foreach (Type type in assembly.GetTypes())
            {
                if (type.GetCustomAttributes(typeof(PluginAttribute), true).Length > 0)
                {
                    types.Add(type);
                }
            }

            return types;
        }
    }
}