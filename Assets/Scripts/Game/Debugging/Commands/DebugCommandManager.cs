using Robbi.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Robbi.Debugging.Commands
{
    [AddComponentMenu("Robbi/Debugging/Debug Command Manager")]
    public class DebugCommandManager : MonoBehaviour
    {
        #region Properties and Fields

        public GameObject debugUI;
        public Text outputText;

        private Dictionary<string, IDebugCommand> registeredCommands = new Dictionary<string, IDebugCommand>();

        #endregion

        #region Command Methods

        private void RegisterCommands()
        {
            RegisterCommand<SetCurrentLevelIndexDebugCommand>("scl");
            RegisterCommand<GetCurrentLevelIndexDebugCommand>("gcl");
        }

        private void RegisterCommand<T>(string name) where T : IDebugCommand, new()
        {
            registeredCommands.Add(name, new T());
        }

        public void ExecuteCommand(string commandText)
        {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
            string[] parameters = commandText.Split(' ');
            if (parameters.Length > 0)
            {
                if (registeredCommands.TryGetValue(parameters[0], out IDebugCommand debugCommand))
                {
                    List<string> commandParams = new List<string>(parameters.Length - 1);
                    for (int i = 1; i < parameters.Length; ++i)
                    {
                        commandParams.Add(parameters[i]);
                    }

                    StringBuilder output = new StringBuilder();
                    debugCommand.Execute(commandParams, output);
                    outputText.text = output.ToString();
                }
            }
#endif
        }

        #endregion

        #region Unity Methods

        private void Start()
        {
            debugUI.SetActive(false);

#if DEVELOPMENT_BUILD || UNITY_EDITOR
            DontDestroyOnLoad(gameObject);
            RegisterCommands();
#else
            GameObject.Destroy(gameObject);
#endif
        }

        private void Update()
        {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
#if UNITY_ANDROID || UNITY_IOS
            if (Input.touchCount == 3 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                debugUI.SetActive(!debugUI.activeSelf);
            }
#else
            // Middle mouse button down
            if (Input.GetMouseButtonDown(2))
            {
                debugUI.SetActive(!debugUI.activeSelf);
            }
#endif
#endif
        }

        #endregion
    }
}
