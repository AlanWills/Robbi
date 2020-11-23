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
        Failed
    }

    [AddComponentMenu("Robbi/Testing/Integration Test")]
    public class IntegrationTest : MonoBehaviour
    {
        #region Properties and Fields

        public FloatValue timeout;

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
                FailTest();
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
            if (testName == GetComponent<FSMRuntime>().graph.name)
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
            if (testName == GetComponent<FSMRuntime>().graph.name)
            {
                FailTest();
            }
        }

        public void StopTest()
        {
            GameObject.Destroy(gameObject);
        }

        private void SetTestResult(TestResult testResult)
        {
            StopTest();
        }

        #endregion
    }
}
