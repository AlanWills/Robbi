using Robbi.FSM;
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
        #region Test Methods

        public void PassTest(string testName)
        {
            SetTestResult(testName, TestResult.Passed);
        }

        public void FailTest(string testName)
        {
            SetTestResult(testName, TestResult.Failed);
        }

        private void SetTestResult(string testName, TestResult testResult)
        {
            if (testName == GetComponent<FSMRuntime>().graph.name)
            {
                GameObject.Destroy(gameObject);
            }
        }

        #endregion
    }
}
