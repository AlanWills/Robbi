using Celeste.Events;
using Celeste.Parameters;

namespace Robbi.Parameters
{
    public class StringValueEventRaiser : ParameterisedEventRaiser<string, StringEvent>
    {
        public void Raise(StringValue stringValue)
        {
            Raise(stringValue.Value);
        }
    }
}