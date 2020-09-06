using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Robbi.Debugging.Logging
{
    [AddComponentMenu("Robbi/Debugging/Hud Logger")]
    public class HudLogger : MonoBehaviour
    {
        private class HudMessage
        {
            public Text message;
            public float timeAlive;
        }

        #region Properties and Fields

        public Transform textParent;
        public GameObject hudMessagePrefab;
        public Color infoColour = Color.white;
        public Color warningColour = Color.yellow;
        public Color errorColour = Color.red;
        public float messageLifetime = 3;

        [SerializeField]
        private int maxMessages = 20;

        private List<HudMessage> hudMessages = new List<HudMessage>();
        private Stack<Text> cachedMessageInstances = new Stack<Text>();

        private static HudLogger instance;

        #endregion

        #region Logging Methods

        public static void LogInfo(string message)
        {
            instance.Log(message, instance.infoColour);
        }

        public static void LogWarning(string message)
        {
            instance.Log(message, instance.warningColour);
        }

        public static void LogError(string message)
        {
            instance.Log(message, instance.errorColour);
        }

        private void Log(string message, Color colour)
        {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
            if (cachedMessageInstances.Count > 0)
            {
                Text messageText = cachedMessageInstances.Pop();
                messageText.text = message;
                messageText.color = colour;
                messageText.gameObject.SetActive(true);

                hudMessages.Add(new HudMessage() { message = messageText, timeAlive = 0 });
            }
            else
            {
                Debug.LogWarningFormat("Hud Message limit reached.  Message: {0}", message);
            }
#endif
        }

        #endregion

        #region Unity Methods

        private void Awake()
        {
            instance = this;

#if DEVELOPMENT_BUILD || UNITY_EDITOR
            for (int i = 0; i < maxMessages; ++i)
            {
                GameObject gameObject = GameObject.Instantiate(hudMessagePrefab, textParent);
                Text messageText = gameObject.GetComponent<Text>();
                gameObject.SetActive(false);
                
                cachedMessageInstances.Push(messageText);
            }
            
            DontDestroyOnLoad(gameObject);
#else
            GameObject.Destroy(gameObject);
#endif
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;

            for (int i = hudMessages.Count; i > 0; --i)
            {
                HudMessage hudMessage = hudMessages[i - 1];
                hudMessage.timeAlive += deltaTime;

                if (hudMessage.timeAlive > messageLifetime)
                {
                    Text messageText = hudMessage.message;
                    messageText.text = "";
                    messageText.gameObject.SetActive(false);

                    hudMessages.RemoveAt(i - 1);
                    cachedMessageInstances.Push(messageText);
                }
            }
        }

        #endregion
    }
}
