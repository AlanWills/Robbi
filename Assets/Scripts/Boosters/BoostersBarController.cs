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
            boosterButtonsAnimator.SetTrigger(animateInHash);
        }

        public void OnShowBoostersBar()
        {
            AnimateIn();
        }

        public void OnHideBoostersBar()
        {
            AnimateOut();
        }

        #endregion

        #region Animation Methods

        public void AnimateIn()
        {
            doorToggleUseButton.interactable = true;
            interactUseButton.interactable = true;
            boosterButtonsAnimator.SetTrigger(animateInHash);
            cancelButtonAnimator.SetTrigger(animateOutHash);
        }

        public void AnimateOut()
        {
            doorToggleUseButton.interactable = false;
            interactUseButton.interactable = false;
            boosterButtonsAnimator.SetTrigger(animateOutHash);
            cancelButtonAnimator.SetTrigger(animateInHash);
        }

        #endregion
    }
}
