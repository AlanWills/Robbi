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

        public Animator animator;
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

        private void OnValidate()
        {
            if (animator == null)
            {
                animator = GetComponent<Animator>();
            }
        }

        #endregion

        #region Animation Methods

        public void AnimateIn()
        {
            doorToggleUseButton.interactable = true;
            interactUseButton.interactable = true;
            animator.SetTrigger(animateInHash);
        }

        public void AnimateOut()
        {
            doorToggleUseButton.interactable = true;
            interactUseButton.interactable = true;
            animator.SetTrigger(animateOutHash);
        }

        #endregion
    }
}
