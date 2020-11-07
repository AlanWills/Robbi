using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.DataSystem.Nodes.Forwarders
{
    [Serializable]
    [CreateNodeMenu("Robbi/Forwarders/Sprite Forwarder")]
    public class SpriteForwarderNode : ValueForwarderNode<Sprite>
    {
    }
}
