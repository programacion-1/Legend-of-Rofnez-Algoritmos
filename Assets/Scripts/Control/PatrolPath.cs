using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    //Clase para difinir recorridos para enemigos
    public class PatrolPath : MonoBehaviour
    {
        const float waypointGizmoRadius = 0.3f;
        
        //Dibujará los gizmos necesarios para corroborar el patrol path con sus nodos y líneas correspondientes
        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                int j = GetNextIndex(i);
                Gizmos.color = Color.white;
                Gizmos.DrawSphere(GetWaypoint(i), waypointGizmoRadius);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }
        }

        public int GetNextIndex(int i)
        {
            if(i+1 == transform.childCount) return 0;
            return i+1;
        }

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}
