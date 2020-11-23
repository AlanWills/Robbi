using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Robbi.Utils
{
    public static class GameObjectUtils
    {
        public static GameObject FindGameObject(string[] splitName)
        {
            GameObject gameObject = GameObject.Find(splitName[0]);

            if (gameObject != null)
            {
                for (int i = 1; i < splitName.Length && gameObject != null; ++i)
                {
                    gameObject = FindGameObjectRecursive(gameObject.transform, splitName[i]);
                }
            }

            return gameObject;
        }

        public static GameObject FindGameObjectRecursive(Transform transform, string childName)
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
                GameObject child = FindGameObjectRecursive(transform.GetChild(i), childName);
                if (child != null)
                {
                    return child;
                }
            }

            return null;
        }

        public static void Click(this GameObject gameObject)
        {
            Button button = gameObject.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.Invoke();
            }
            else
            {
                Debug.LogFormat("No Button component found on GameObject {0}", gameObject.name);
            }
        }
    }
}
