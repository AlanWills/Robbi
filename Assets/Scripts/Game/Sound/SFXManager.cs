using Robbi.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Sound
{
    [AddComponentMenu("Robbi/Sound/SFX Manager")]
    [RequireComponent(typeof(AudioSource))]
    public class SFXManager : MonoBehaviour
    {
        #region Properties and Fields

        public AudioSource audioSource;

        #endregion

        #region Unity Methods

        private void OnValidate()
        {
            if (audioSource == null)
            {
                audioSource = GetComponent<AudioSource>();
            }
        }

        #endregion

        #region Audio Methods

        public void Play(AudioClip audioClip)
        {
            if (OptionsManager.Instance.SfxEnabled)
            {
                audioSource.PlayOneShot(audioClip);
            }
        }

        public void OnSFXEnabledChanged(bool isEnabled)
        {
            for (int i = 0; i < transform.childCount; ++i)
            {
                transform.GetChild(i).gameObject.SetActive(isEnabled);
            }
        }

        #endregion
    }
}
