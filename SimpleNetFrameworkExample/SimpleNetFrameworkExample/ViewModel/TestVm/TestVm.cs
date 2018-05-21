using System;
using SimpleNetFrameworkExample.Logic.TestLogic;

namespace SimpleNetFrameworkExample.ViewModel.TestVm
{
    class TestVm : ITestVm
    {
        private readonly ITestLogic _logic;

        public TestVm(ITestLogic logic)
        {
            _logic = logic;
        }
        
        public string SomeText => "ViewModelSays =>" +_logic.Do();
    }
}
