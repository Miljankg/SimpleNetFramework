using System;
using SimpleNetFramework;
using SimpleNetFrameworkExample.Logic.TTwo;

namespace SimpleNetFrameworkExample.Logic.TestLogic
{
    class TestLogic : ITestLogic
    {
        private ITTwoLogic _l;

        public TestLogic(ITTwoLogic l)
        {
            _l = l;
        }

        public string Do()
        {
            return "TestLogic Says (from TTwoLogic) => " + _l.GetSome();
        }
    }
}
