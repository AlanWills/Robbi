using Celeste.Debug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Debugging.Buttons
{
    [AddComponentMenu("Robbi/Debugging/Level Debug Button")]
    public class LevelDebugButton : DebugOnlyMonoBehaviour
    {
        #region Properties and Fields

        public Animator debugButtonsAnimator;
        public string animateInName = "AnimateIn";
        public string animateOutName = "AnimateOut";

        private int animateInHash;
        private int animateOutHash;
        private bool areButtonsShown = false;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            animateInHash = Animator.StringToHash(animateInName);
            animateOutHash = Animator.StringToHash(animateOutName);
        }

        private void OnEnable()
        {
            debugButtonsAnimator.SetTrigger(animateOutHash);
            areButtonsShown = false;
        }

        #endregion

        #region Show/Hide

        public void ToggleDebugButtonsVisibility()
        {
            debugButtonsAnimator.SetTrigger(areButtonsShown ? animateOutHash : animateInHash);
            areButtonsShown = !areButtonsShown;
        }

        #endregion
    }
}
