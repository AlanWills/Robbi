using Robbi.Levels.Elements;
using Robbi.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobbiEditor.Validation.Doors.Conditions
{
    public class DoorHasClosedTiles : IValidationCondition<Door>
    {
        public string DisplayName { get { return "Has Closed Tiles"; } }

        public bool Validate(Door obj, StringBuilder output)
        {
            if (obj.closedTile == null)
            {
                output.AppendLineFormat("Door {0} has no closed tile set", obj.name);
                return false;
            }

            return true;
        }
    }
}
