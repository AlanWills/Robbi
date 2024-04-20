using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Robbi.Boosters
{
    [AddComponentMenu("Robbi/Boosters/Boosters Bar Controller")]
    public class BoostersBarController : MonoBehaviour
    {
        #region Properties and Fields

        [SerializeField] private List<GameObject> boosterBarUI = new List<GameObject>();
        [SerializeField] private Animator boosterBarAnimator;
        [SerializeField] private Button doorToggleUseButton;
        [SerializeField] private Button interactUseButton;

        [SerializeField] private string animateInName = "AnimateIn";
        [SerializeField] private string animateOutName = "AnimateOut";

        [NonSerialized] private int animateInHash;
        [NonSerialized] private int animateOutHash;

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
            foreach (GameObject gameObject in boosterBarUI)
            {
                gameObject.SetActive(true);
            }

            ShowBoostersHideCancel();
        }

        public void OnLevelLost()
        {
            HideBoosterBarUI();
        }

        public void OnLevelWon()
        {
            HideBoosterBarUI();
        }

        private void HideBoosterBarUI()
        {
            foreach (GameObject gameObject in boosterBarUI)
            {
                gameObject.SetActive(false);
            }
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
            boosterBarAnimator.SetTrigger(animateInHash);
        }

        public void HideBoostersShowCancel()
        {
            boosterBarAnimator.SetTrigger(animateOutHash);
        }

        #endregion
    }
}
