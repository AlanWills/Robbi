using Robbi.Options;
using Celeste.FSM;
using Robbi.Boosters;

namespace Robbi.FSM.Nodes.Game
{
    [CreateNodeMenu("Robbi/Saving/Save Boosters Manager")]
    [NodeTint(0.2f, 0.2f, 0.6f)]
    public class SaveBoostersManagerNode : FSMNode
    {
        #region FSM Runtime Overrides

        protected override void OnEnter()
        {
            base.OnEnter();

            BoostersManager.Instance.Save();
        }

        #endregion
    }
}
