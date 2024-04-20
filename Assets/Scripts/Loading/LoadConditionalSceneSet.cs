using Celeste.Loading;
using Celeste.Logic;
using System;
using System.Collections;
using UnityEngine;

namespace Robbi.Loading
{
    [CreateAssetMenu(fileName = nameof(LoadConditionalSceneSet), menuName = "Robbi/Loading/Load Conditional Scene Set")]
    public class LoadConditionalSceneSet : LoadJob
    {
        [SerializeField] private Condition isFirstLevel;
        [SerializeField] private LoadSceneSetLoadJob firstLevelSceneSetLoadJob;
        [SerializeField] private LoadSceneSetLoadJob notFirstLevelSceneSetLoadJob;

        public override IEnumerator Execute(Action<float> setProgress, Action<string> setOutput)
        {
            if (isFirstLevel.IsMet)
            {
                yield return firstLevelSceneSetLoadJob.Execute(setProgress, setOutput);
            }
            else
            {
                yield return notFirstLevelSceneSetLoadJob.Execute(setProgress, setOutput);
            }
        }
    }
}