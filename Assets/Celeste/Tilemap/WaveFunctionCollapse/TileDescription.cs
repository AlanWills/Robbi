using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Celeste.Tilemaps.WaveFunctionCollapse
{
    [CreateAssetMenu(fileName = "TileDescription", menuName = "Celeste/Tilemaps/Wave Function Collapse/Tile Description")]
    public class TileDescription : ScriptableObject
    {
        #region Serialized Fields

        public IEnumerable<Rule> Rules
        {
            get { return rules; }
        }

        public float weight = 1;
        public TileBase tile;
        [SerializeField, HideInInspector] private List<Rule> rules = new List<Rule>();

        #endregion

        #region Rules Functions

        public Rule AddRule()
        {
            Rule rule = ScriptableObject.CreateInstance<Rule>();
            rule.hideFlags = HideFlags.HideInHierarchy;
            rules.Add(rule);

#if UNITY_EDITOR
            UnityEditor.AssetDatabase.AddObjectToAsset(rule, this);
            UnityEditor.EditorUtility.SetDirty(this);
#endif

            return rule;
        }

        public void RemoveRule(int ruleIndex)
        {
            if (0 <= ruleIndex && ruleIndex < rules.Count)
            {
#if UNITY_EDITOR
                UnityEditor.AssetDatabase.RemoveObjectFromAsset(rules[ruleIndex]);
                UnityEditor.EditorUtility.SetDirty(this);
#endif
                rules.RemoveAt(ruleIndex);
            }
        }

        public void ClearRules()
        {
#if UNITY_EDITOR
            foreach (Rule rule in rules)
            {
                UnityEditor.AssetDatabase.RemoveObjectFromAsset(rule);
            }
#endif

            rules.Clear();
        }

        public Rule FindRule(Predicate<Rule> predicate)
        {
            return rules.Find(predicate);
        }

        public bool SupportsTile(TileDescription tile, Direction direction)
        {
            foreach (Rule rule in rules)
            {
                if (rule.otherTile == tile && rule.direction == direction)
                {
                    return true;
                }
            }

            return false;
        }

#endregion
    }
}
