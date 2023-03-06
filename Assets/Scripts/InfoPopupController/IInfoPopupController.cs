using Character;
using UnityEngine;

namespace InfoPopupController
{
    public interface IInfoPopupController
    {
        public void SetData(UnitModelBase infoPopupData, Vector2 screenPosition);
    }
}