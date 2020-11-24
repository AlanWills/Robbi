using Robbi.Debugging.Logging;
using Robbi.Events;
using Robbi.FSM;
using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Testing
{
    public enum TestResult
    { 
        Passed,
        Failed,
        TimedOut
    }

    [AddComponentMenu("Robbi/Testing/Integration Test")]
    public class IntegrationTest : MonoBehaviour
    {
        #region Properties and Fields

        public FloatValue timeout;
        public StringEvent integrationTestTimedOut;

        private float currentRuntime = 0;

        #endregion

        #region Unity Methods

        private void Start()
        {
            currentRuntime = 0;
        }

        private void Update()
        {
            currentRuntime += Time.deltaTime;

            if (currentRuntime > timeout.value)
            {
                TimeoutTest();
            }
        }

        #endregion

        #region Test Methods

        public void PassTest()
        {
            SetTestResult(TestResult.Passed);
        }

        public void TryPassTest(string testName)
        {
            if (testName == gameObject.name)
            {
                PassTest();
            }
        }

        public void FailTest()
        {
            SetTestResult(TestResult.Failed);
        }

        public void TryFailTest(string testName)
        {
            if (testName == gameObject.name)
            {
                FailTest();
            }
        }

        public void TimeoutTest()
        {
            integrationTestTimedOut.Raise(gameObject.name);
            SetTestResult(TestResult.TimedOut);
        }

        public void StopTest()
        {
            GameObject.Destroy(gameObject);
        }

        private void SetTestResult(TestResult testResult)
        {
            HudLogger.LogInfoFormat("Integration Test {0} {1}", gameObject.name, testResult);

            StopTest();
        }

        #endregion
    }
}
