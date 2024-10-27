using UnityEngine;

namespace Basics
{
    public class InstantiateOnDestroy : MonoBehaviour
    {
        public GameObject[] onDestroyPrefabs;
        [Header("Target position may be left empty to spawn on current transform's position")]
        public Transform targetPosition;

        private void OnDestroy()
        {
            if(onDestroyPrefabs != null)
            {
                foreach(GameObject prefab in onDestroyPrefabs)
                {
                    var instance = Instantiate(prefab, null);
                    if(targetPosition)
                    {
                        instance.transform.position = targetPosition.position;
                    }
                    else
                    {
                        instance.transform.position = transform.position;
                    }
                }
            }
        }
    }
}
