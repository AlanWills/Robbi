using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.FX
{
    [AddComponentMenu("Robbi/FX/Emoji Controller")]
    public class EmojiController : MonoBehaviour
    {
        #region Properties and Fields

        [SerializeField]
        private GameObject emoji;

        [SerializeField]
        private SpriteRenderer emojiRenderer;

        [SerializeField]
        private Animation emojiAnimation;

        [SerializeField]
        private Sprite idleEmoji;

        [SerializeField]
        private Sprite levelWinEmoji;

        [SerializeField]
        private Sprite levelLoseEmoji;

        [SerializeField]
        private Sprite waypointAddedEmoji;

        [SerializeField]
        private float emojiLifeTime = 2;

        private Coroutine emojiCoroutine;

        #endregion

        #region Emoji Methods

        public void TryShowIdleEmoji()
        {
            TryShowEmoji(idleEmoji);
        }

        public void TryShowLevelWinEmoji()
        {
            TryShowEmoji(levelWinEmoji);
        }

        public void TryShowLevelLoseEmoji()
        {
            TryShowEmoji(levelLoseEmoji);
        }

        public void TryShowWaypointAddedEmoji()
        {
            TryShowEmoji(waypointAddedEmoji);
        }

        private void TryShowEmoji(Sprite sprite)
        {
            if (emojiCoroutine == null)
            {
                StartCoroutine(ShowEmoji(sprite));
            }
        }

        private IEnumerator ShowEmoji(Sprite sprite)
        {
            emojiRenderer.sprite = sprite;
            emoji.SetActive(true);
            emojiAnimation.Play();

            yield return new WaitForSeconds(emojiLifeTime);

            emoji.SetActive(false);
        }

        #endregion
    }
}
