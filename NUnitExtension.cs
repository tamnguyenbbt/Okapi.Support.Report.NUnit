using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Okapi.Common.Report;
using Okapi.Report;

namespace Okapi.Support.Report.NUnit
{
    public static class NUnitExtension
    {
        public static OkapiTestContext ToOkapiTestContext(this TestContext testContext)
        {
            if (testContext == null)
            {
                return null;
            }

            return new OkapiTestContext
            {
                TestName = testContext?.Test?.Name,
                FullyQualifiedTestClassName = testContext?.Test?.ClassName,
                MethodName = testContext?.Test?.MethodName,
                ID = testContext?.Test?.ID,
                TestResult = NUnitTestStatusToTestResult(testContext.Result.Outcome.Status),
                StackTrace = testContext?.Result?.StackTrace,
                Message = testContext?.Result?.Message
            };
        }

        private static TestResult NUnitTestStatusToTestResult(TestStatus testOutcome)
        {
            TestResult result = TestResult.Passed;

            switch (testOutcome)
            {
                case TestStatus.Skipped:
                    result = TestResult.Skipped;
                    break;
                case TestStatus.Warning:
                    result = TestResult.Warning;
                    break;
                case TestStatus.Failed:
                    result = TestResult.Failed;
                    break;
                case TestStatus.Inconclusive:
                    result = TestResult.Inconclusive;
                    break;
                case TestStatus.Passed:
                    result = TestResult.Passed;
                    break;
                default:
                    result = TestResult.Unknown;
                    break;
            }

            return result;
        }
    }
}