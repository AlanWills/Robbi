using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Robbi.Boosters
{
    [AddComponentMenu("Robbi/Boosters/Boosters Bar Controller")]
    public class BoostersBarController : MonoBehaviour
    {
        #region Properties and Fields

        public Animator boosterButtonsAnimator;
        public Animator cancelButtonAnimator;
        public Button doorToggleUseButton;
        public Button interactUseButton;

        public string animateInName = "AnimateIn";
        public string animateOutName = "AnimateOut";

        private int animateInHash;
        private int animateOutHash;
        private bool boosterInUse = false;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            animateInHash = Animator.StringToHash(animateInName);
            animateOutHash = Animator.StringToHash(animateOutName);
        }

        #endregion

        #region Callbacks

        public void OnLevelBegun()
        {
            if (boosterInUse)
            {
                cancelButtonAnimator.SetTrigger(animateOutHash);
            }

            boosterInUse = false;
            boosterButtonsAnimator.SetTrigger(animateInHash);
        }

        public void OnLevelLost()
        {
            AnimateOutRelevantUI();
        }

        public void OnLevelWon()
        {
            AnimateOutRelevantUI();
        }

        private void AnimateOutRelevantUI()
        {
            if (!boosterInUse)
            {
                boosterButtonsAnimator.SetTrigger(animateOutHash);
            }
            else
            {
                cancelButtonAnimator.SetTrigger(animateOutHash);
            }

            boosterInUse = false;
        }

        public void OnShowBoostersBar()
        {
            ShowBoostersHideCancel();
        }

        public void OnHideBoostersBar()
        {
            HideBoostersShowCancel();
        }

        #endregion

        #region Animation Methods

        public void ShowBoostersHideCancel()
        {
            boosterButtonsAnimator.SetTrigger(animateInHash);
            cancelButtonAnimator.SetTrigger(animateOutHash);
            boosterInUse = false;
        }

        public void HideBoostersShowCancel()
        {
            boosterButtonsAnimator.SetTrigger(animateOutHash);
            cancelButtonAnimator.SetTrigger(animateInHash);
            boosterInUse = true;
        }

        #endregion
    }
}
