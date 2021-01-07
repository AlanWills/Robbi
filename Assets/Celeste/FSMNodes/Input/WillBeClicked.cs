using Celeste.FSM;
using Celeste.Hierarchy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Celeste.FSMNodes.Input
{
    [CreateNodeMenu("Celeste/Input/Will Be Clicked")]
    public class WillBeClicked : FSMNode
    {
        #region Properties and Fields
        
        public GameObjectPath gameObjectPath = new GameObjectPath();
        public float attemptWindow = 1;

        private float currentTime = 0;

        private const string WILL_BE_CLICKED_OUTPUT_PORT = "WillBeClicked";
        private const string WONT_BE_CLICKED_OUTPUT_PORT = "WontBeClicked";

        #endregion

        public WillBeClicked()
        {
            RemoveDynamicPort(DEFAULT_OUTPUT_PORT_NAME);

            AddOutputPort(WILL_BE_CLICKED_OUTPUT_PORT);
            AddOutputPort(WONT_BE_CLICKED_OUTPUT_PORT);
        }

        #region Add/Remove/Copy

        protected override void OnCopyInGraph(FSMNode original)
        {
            base.OnCopyInGraph(original);

            WillBeClicked willBeClicked = original as WillBeClicked;

            attemptWindow = willBeClicked.attemptWindow;
            gameObjectPath = new GameObjectPath();
            gameObjectPath.Path = willBeClicked.gameObjectPath.Path;
        }

        #endregion

        #region Node Overrides

        protected override void OnEnter()
        {
            base.OnEnter();

            currentTime = 0;
            gameObjectPath.Reset();
        }

        protected override FSMNode OnUpdate()
        {
            if (currentTime <= attemptWindow)
            {
                currentTime += Time.deltaTime;

                GameObject gameObject = gameObjectPath.GameObject;
                if (gameObject != null)
                {
                    PointerEventData eventData = new PointerEventData(EventSystem.current);
                    eventData.position = gameObject.transform.position;

                    List<RaycastResult> results = new List<RaycastResult>();
                    EventSystem.current.RaycastAll(eventData, results);

                    if (results.Count > 0 && results[0].gameObject == ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject))
                    {
                        Debug.LogFormat("GameObject {0} will be clicked", gameObjectPath.Path);
                        return GetConnectedNode(WILL_BE_CLICKED_OUTPUT_PORT);
                    }
                }

                // We are still within the attempt window so stay within this node
                return this;
            }

            Debug.LogFormat("GameObject {0} will not be clicked", gameObjectPath);
            return GetConnectedNode(WONT_BE_CLICKED_OUTPUT_PORT);
        }

        #endregion
    }
}
