using Character.Base;
using Events.Interfaces;
using UnityEngine;

namespace Events
{
    public class ShowInfoPopupEvent : IEvent
    {
        public UnitModelBase Data { get; }
        public Vector2 ScreenPosition { get; }

        public ShowInfoPopupEvent(UnitModelBase data, Vector2 screenPosition)
        {
            Data = data;
            ScreenPosition = screenPosition;
        }
    }
}
