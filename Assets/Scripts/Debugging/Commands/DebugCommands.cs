using Celeste.Application;
using Celeste.Events;
using Robbi.Levels;
using Robbi.Movement;
using Robbi.Options;
using Robbi.Testing;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Celeste.Debugging.Commands;
using Celeste.Log;
using Robbi.Boosters;

namespace Robbi.Debugging.Commands
{
    [AddComponentMenu("Robbi/Debugging/Debug Commands")]
    public class DebugCommands : MonoBehaviour
    {
        #region Properties and Fields

        public GameObject debugUI;
        public Text outputText;

        private Dictionary<string, IDebugCommand> registeredCommands = new Dictionary<string, IDebugCommand>();

        #endregion

        #region Command Methods

        private void RegisterCommands()
        {
            RegisterCommand<ConsoleLevelManager>("lm");
            RegisterCommand<ConsoleMovementManager>("mm");
            RegisterCommand<ConsoleBoostersManager>("bm");
            RegisterCommand<ConsoleHudLog>("hlog");
            RegisterCommand<ConsoleApplication>("app");
            RegisterCommand<ConsoleIntegrationTest>("it");
            RegisterCommand<ConsoleCurrentLevelIntegrationTest>("clit");
            RegisterCommand<ConsoleFuel>("fuel");
            RegisterCommand<ConsoleOptionsManager>("opts");
        }

        private void RegisterCommand<T>(string name) where T : IDebugCommand, new()
        {
            registeredCommands.Add(name, new T());
        }

        public void ExecuteCommand(string commandText)
        {
            string[] parameters = commandText.Split(' ');
            if (parameters.Length > 0)
            {
                StringBuilder output = new StringBuilder();

                if (registeredCommands.TryGetValue(parameters[0], out IDebugCommand debugCommand))
                {
                    List<string> commandParams = new List<string>(parameters.Length - 1);
                    for (int i = 1; i < parameters.Length; ++i)
                    {
                        commandParams.Add(parameters[i]);
                    }

                    debugCommand.Execute(commandParams, output);
                }
                else
                {
                    output.AppendFormat("Could not find command {0}", parameters[0]);
                }

                outputText.text = output.ToString();
            }
        }

        #endregion

        #region Unity Methods

        private void Start()
        {
            debugUI.SetActive(false);
            RegisterCommands();
        }

        #endregion

        #region Activation

        public void TryToggle(MultiTouchEventArgs multiTouchEventArgs)
        {
#if UNITY_ANDROID || UNITY_IOS
            for (int i = 0; i < multiTouchEventArgs.touchCount; ++i)
            {
                if (multiTouchEventArgs.touches[i].phase == TouchPhase.Ended)
                {
                    Toggle();
                    return;
                }
            }
#endif
        }

        public void Toggle()
        {
            debugUI.SetActive(!debugUI.activeSelf);
        }

        #endregion
    }
}
