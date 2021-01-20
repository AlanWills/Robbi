using Celeste.Parameters;
using Robbi.Options;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Sound
{
    [AddComponentMenu("Robbi/Sound/Music Manager")]
    [RequireComponent(typeof(AudioSource))]
    public class MusicManager : MonoBehaviour
    {
        #region Properties and Fields

        public bool shuffle = true;
        [SerializeField] private List<AudioClip> music = new List<AudioClip>();
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private BoolValue musicEnabled;

        private int currentTrackIndex = -1;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            if (audioSource.playOnAwake)
            {
                NextTrack();
            }
        }

        private void OnValidate()
        {
            if (audioSource == null)
            {
                audioSource = GetComponent<AudioSource>();
            }
        }

        #endregion

        #region Utility

        public void NextTrack()
        {
            if (music.Count > 0 && musicEnabled.Value)
            {
                currentTrackIndex = shuffle ? Random.Range(0, music.Count) : (++currentTrackIndex % music.Count);
                Debug.AssertFormat(0 <= currentTrackIndex & currentTrackIndex < music.Count, "Invalid track index {0}", currentTrackIndex);
                PlayOneShot(music[currentTrackIndex]);
            }
        }

        public void OnMusicEnabledChanged(bool isEnabled)
        {
            for (int i = 0; i < transform.childCount; ++i)
            {
                transform.GetChild(i).gameObject.SetActive(isEnabled);
            }
        }

        public void Play(AudioClip audioClip)
        {
            if (musicEnabled.Value && !audioSource.isPlaying)
            {
                audioSource.clip = audioClip;
                audioSource.Play();
            }
        }

        public void PlayOneShot(AudioClip audioClip)
        {
            if (musicEnabled.Value)
            {
                audioSource.clip = audioClip;
                audioSource.Play();
            }
        }

        #endregion
    }
}
