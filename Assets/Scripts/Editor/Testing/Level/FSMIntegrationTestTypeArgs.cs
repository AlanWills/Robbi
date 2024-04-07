using Celeste.FSM;
using Celeste.Testing;
using UnityEngine;

namespace RobbiEditor.Testing
{
    public class FSMIntegrationTestTypeArgs : IntegrationTestTypeArgs
    {
        public FSMGraph FSM => fsm;

        [SerializeField] private FSMGraph fsm;
    }
}