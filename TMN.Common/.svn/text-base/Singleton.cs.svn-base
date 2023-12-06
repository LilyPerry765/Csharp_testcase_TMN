using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Collections;
using System.Diagnostics;

namespace TMN
{
    [DebuggerStepThrough()]
    [DebuggerNonUserCode()]
    public static class Singleton<T> where T : class, new()
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                    instance = new T();

                return instance;
            }
        }
    }


}
