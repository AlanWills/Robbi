using Robbi.Options;
using Celeste.FSM;

namespace Robbi.FSM.Nodes.Game
{
    [CreateNodeMenu("Robbi/Saving/Save Options Manager")]
    [NodeTint(0.2f, 0.2f, 0.6f)]
    public class SaveOptionsManagerNode : FSMNode
    {
        #region FSM Runtime Overrides

        protected override void OnEnter()
        {
            base.OnEnter();

            OptionsManager.Instance.Save();
        }

        #endregion
    }
}
