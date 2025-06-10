using NUnit.Framework;
using UnityEngine;

namespace Runtime.DeckLogic
{
    internal enum PlayerColor
    {
        Red,
        Blue
    }
    public class PlayerDeck : MonoBehaviour
    {
        private float[] cardXCoo = { -8, -4, 0, 4, 8, 12 };
        internal PlayerColor PlayerColor { get; private set; }
        
        internal void Init(PlayerColor color)
        {
            PlayerColor = color;
            // можно добавить Setup: UI, камера и т.д.
        }
    }
}
