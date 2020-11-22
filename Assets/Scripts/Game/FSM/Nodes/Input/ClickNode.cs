using Robbi.Events;
using Robbi.Game.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Robbi.FSM.Nodes.Input
{
    [CreateNodeMenu("Robbi/Input/Click")]
    public class ClickNode : FSMNode
    {
        #region Properties and Fields

        public string gameObjectPath;

        private static char[] delimiter = new char[1] { '.' };

        #endregion

        #region FSM Runtime

        protected override void OnEnter()
        {
            base.OnEnter();

            GameObject gameObject = FindGameObject();
            if (gameObject != null)
            {
                Click(gameObject);
            }
            else
            {
                Debug.LogFormat("Could not find GameObject with path {0}", gameObjectPath);
            }
        }

        #endregion

        #region Utility Methods

        private GameObject FindGameObject()
        {
            string[] splitName = gameObjectPath.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
            GameObject gameObject = GameObject.Find(splitName[0]);

            if (gameObject != null)
            {
                for (int i = 1; i < splitName.Length && gameObject != null; ++i)
                {
                    gameObject = FindGameObject(gameObject.transform, splitName[i]);
                }
            }

            return gameObject;
        }

        private GameObject FindGameObject(Transform transform, string childName)
        {
            for (int i = 0; i < transform.childCount; ++i)
            {
                Transform child = transform.GetChild(i);
                if (child.name == childName)
                {
                    return child.gameObject;
                }
            }

            for (int i = 0; i < transform.childCount; ++i)
            {
                GameObject child = FindGameObject(transform.GetChild(i), childName);
                if (child.name == childName)
                {
                    return child.gameObject;
                }
            }

            return null;
        }

        private void Click(GameObject gameObject)
        {
            Button button = gameObject.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.Invoke();
            }
            else
            {
                Debug.LogFormat("No Button component found on GameObject with path {0}", gameObjectPath);
            }
        }

        #endregion
    }
}
