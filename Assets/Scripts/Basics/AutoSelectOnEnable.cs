using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Basics
{
    [RequireComponent(typeof(Selectable))]
    public class AutoSelectOnEnable : MonoBehaviour
    {
        private Selectable _selectable;
        private Selectable _lastSelectedObject;

        void Awake()
        {
            _selectable = GetComponent<Selectable>();
        }

        private void Update()
        {
            if(!EventSystem.current.currentSelectedGameObject || !EventSystem.current.currentSelectedGameObject.activeInHierarchy)
            {
                if(_lastSelectedObject && _lastSelectedObject.gameObject.activeInHierarchy)
                {
                    _lastSelectedObject.Select();
                }
                else
                {
                    _selectable.Select();
                }
            }

            if(EventSystem.current.currentSelectedGameObject)
            {
                _lastSelectedObject = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>();
            }
            else
            {
                _lastSelectedObject = null;
            }
        }

        private void OnEnable()
        {
            _selectable.Select();
        }
    }
}
