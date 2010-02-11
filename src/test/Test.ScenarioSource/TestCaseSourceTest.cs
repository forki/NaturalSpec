using System.Collections.Generic;
using NUnit.Framework;

namespace Test.ScenarioSource
{
    [TestFixture]
    public class TestCaseSourceTest
    {
        public IEnumerable<TestCaseData> MyTestCases()
        {
            return
                new List<TestCaseData>
                    {
                        new TestCaseData(1),
                        new TestCaseData(2),
                        new TestCaseData(3)
                    };
        }

        [TestCaseSource("MyTestCases")]
        public void CanRunTestCaseSource(int n)
        {
            Assert.IsTrue(n < 10);
        }
    }
}