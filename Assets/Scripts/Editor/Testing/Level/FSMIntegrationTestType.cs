using Celeste.FSM;
using Celeste.Testing;
using System;
using System.Collections;
using UnityEngine;

namespace RobbiEditor.Testing
{
    [CreateAssetMenu(fileName = nameof(FSMIntegrationTestType), menuName = "Robbi/Testing/FSM Integration Test Type")]
    public class FSMIntegrationTestType : IntegrationTestType
    {
        protected override Type ArgsType => typeof(FSMIntegrationTestTypeArgs);

        protected override IEnumerator DoTestUpdate(IntegrationTestTypeArgs args)
        {
            FSMIntegrationTestTypeArgs fsmIntegrationTestTypeArgs = args as FSMIntegrationTestTypeArgs;
            LinearRuntime linearRuntime = new LinearRuntime();
            linearRuntime.OnNodeEnter.AddListener((node) =>
            {
                Debug.Log($"Entered node {node.name} in fsm {fsmIntegrationTestTypeArgs.FSM.name}.");
            });
            FSMRuntimeEngine fsmRuntimeEngine = new FSMRuntimeEngine(linearRuntime);
            fsmRuntimeEngine.Start(fsmIntegrationTestTypeArgs.FSM.startNode);

            yield return null;

            while (linearRuntime.CurrentNode != null)
            {
                fsmRuntimeEngine.Update();
                yield return null;
            }
        }

        private class LinearRuntime : ILinearRuntime
        {
            #region Properties and Fields

            public FSMNodeUnityEvent OnNodeEnter { get; } = new FSMNodeUnityEvent();
            public FSMNodeUnityEvent OnNodeUpdate { get; } = new FSMNodeUnityEvent();
            public FSMNodeUnityEvent OnNodeExit { get; } = new FSMNodeUnityEvent();

            public FSMNode StartNode
            {
                get => startNode;
                set => startNode = value;
            }

            public FSMNode CurrentNode
            {
                get => currentNode;
                set => currentNode = value;
            }

            public ILinearRuntimeRecord Record { get; } = new FSMRecord();

            private FSMNode startNode;
            private FSMNode currentNode;

            #endregion

            public void Run()
            {
            }

            public void Stop()
            {
            }
        }
    }
}