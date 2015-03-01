using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfUtils.Patterns;

namespace WpfUtilsTest.Patterns
{
    public class SingletonExample : Singleton<SingletonExample>
    {
        private SingletonExample()
        {

        }
    }

    [TestClass]
    public class SingletonTests
    {
        [TestMethod]
        public void Instance_InvokesPrivateConstructor()
        {
            var test = SingletonExample.Instance;
        }
    }
}
