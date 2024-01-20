using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Enums
{
    public enum InteractionFailureType
    {
        //order of these InteractionTypes MATTERS, when instantiating interactions by checkouts, they are converted to ints, that corresponds to an index in a list of Interactions. Mixing up the order would mean a different interaction instantiation when converting item's InteractionType to int.
        None,
        IncorrectQuantity,
        IncorrectWeight
    }
}
