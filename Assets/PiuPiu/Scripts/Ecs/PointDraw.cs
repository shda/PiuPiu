using UnityEngine;

namespace PiuPiu.Scripts
{
    public class PointDraw : MonoBehaviour
    {
        public float size = 0.1f;
        
        public static PointDraw Instance;

        private void Awake()
        {
            Instance = this;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(transform.position , size);
        }
    }
}