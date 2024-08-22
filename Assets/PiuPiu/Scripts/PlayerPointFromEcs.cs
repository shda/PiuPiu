using UnityEngine;

namespace PiuPiu.Scripts
{
    public class PlayerPointFromEcs : MonoBehaviour
    {
        [SerializeField] private float size = 0.1f;
        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(transform.position , size);
        }
    }
}