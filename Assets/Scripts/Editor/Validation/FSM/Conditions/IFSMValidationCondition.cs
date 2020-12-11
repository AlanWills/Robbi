using Robbi.FSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobbiEditor.Validation.FSM.Conditions
{
    public interface IFSMValidationCondition
    {
        string DisplayName { get; }

        bool Validate(FSMGraph fsmGraph, StringBuilder output);
    }
}
