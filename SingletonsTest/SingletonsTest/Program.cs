using System;
using System.Reflection;


namespace SingletonsTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //MySingleton instance = MySingleton.Instance;
            //instance.DoAction();
        }
    }
    #region Etalon
    public class Singleton
    {
        private static Singleton instance;
        private Singleton()
        { }
        public static Singleton GetInstance ()
        {
            if(instance==null)
            {
                instance = new Singleton();
            }
            return instance;
        }
    }
    #endregion

    #region Version2
    //solution based ECMA335 : CLI, Paragraff 8.9.5 дает потокобезопасность
    public sealed class Singleton2
    {
        private Singleton2()
        { }
        public static Singleton2 Instance
        {
            get { return InstanceHolder._instance; }
        }
        protected class InstanceHolder
        {
            static InstanceHolder()
            { }
            internal static readonly Singleton2 _instance = new Singleton2();
        }
    }
    #endregion

    #region LazyClass - standart
    public sealed class Singleton3
    {
        private static readonly Lazy<Singleton3> _instance = new Lazy<Singleton3>(()=> new Singleton3());
        private Singleton3()
        { }
        public static Singleton3 Instance
        {
            get { return _instance.Value; }
        }
    }
    #endregion

    #region LazyClass - standart
    public class Singleton<T> where T : class
    {
        public static readonly Lazy<T> _instance = new Lazy<T>(() => (T)typeof(T).GetConstructor(
            BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[0], null).Invoke(null)
        );
        public static T Instance
        {
            get { return _instance.Value; }
        }
        public sealed class MySingleton : Singleton<MySingleton>
        {
            private MySingleton()
            { }
            public void DoAction()
            {
                Console.WriteLine("My Singleton Test");
            }
        }
        //MySingleton instance = MySingleton.Instance;
        //instance.DoAction();
    }
    #endregion
}
