using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;                                            //  Cuan lejos puedo ver
    [Range(0, 360)]                                                     //  Rango para el siguiente campo
    public float viewAngle;                                             //  Cuánto puedo ver

    public LayerMask targetMask;                                        //  Qué estoy buscando
    public LayerMask obstacleMask;                                      //  Qué evitará que lo vea

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();      //  Targets a la vista. 
    public float delayTime;                                             //  Framerate de checkeo

    private float meshResolution = 1;                                        //
    public MeshFilter viewMeshFilter;                                   //  Sistema que grafica los datos acerca de los triángulos que le entregamos. 
    Mesh viewMesh;                                                      //  Mesh vacía. La instanciaremos más adelante. 

    public int edgeResolveIterations;



    // Use this for initialization
    void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;
        StartCoroutine(FindTargetsWithDelay(delayTime));
    }

    // Update is called once per frame
    void LateUpdate()
    {
        DrawFieldOfView();
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();                                                 //Cualquier Target que había visto es eliminado de mi memoria.                                       
        Collider2D[] targetsInView = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInView.Length; i++)
        {
            Transform target = targetsInView[i].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            //Debug.Log(Vector3.Angle(transform.up, directionToTarget));
            if (Vector3.Angle(transform.up, directionToTarget) < (viewAngle / 2))
            {
                float disToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics2D.Raycast(transform.position, directionToTarget, disToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }
    }

    private void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);                       //Resolución de la vista. Cada cuántos ángulos enviará un rayo. 
        float stepAngleSize = viewAngle / stepCount;                                        //Cantidad de grados que cubre cada nuevo rayo. 
        List<Vector3> viewPoints = new List<Vector3>();                                     //Listado de Puntos en el espacio donde Chocan o Terminan;

        ViewCastInfo oldViewCastoldVCastInfo = new ViewCastInfo();

        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);                                     //Voy a Cosntruir un Struct con información del rayo.

            if (i > 0)
            {
                if (oldViewCastoldVCastInfo.hit != newViewCast.hit)
                {
                    EdgeInfo edge = FindEdge(oldViewCastoldVCastInfo, newViewCast);
                    if (edge.pointA != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointA);
                    }
                    if (edge.pointB != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointB);
                    }
                }
            }
            oldViewCastoldVCastInfo = newViewCast;
            viewPoints.Add(newViewCast.point);                                              //Agrego a una lista la información de cada uno de los rayos. 
        }

        int vertexCount = viewPoints.Count + 1;                                             //Tamaño de la lista que crearon los rayos, incluyendo el punto 0. 
        Vector3[] vertices = new Vector3[vertexCount];                                      //Listando de vértices que hay. 
        int[] triangles = new int[(vertexCount - 2) * 3];                                   //Listado de triángulos que hay. 
        vertices[0] = Vector3.zero;                                                         //Seteando el vértice de origen. 

        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);               //Saltamos el vértice 0. Vértice actual es el viewPoint equivalente al index.
            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
    {
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;
        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for (int i = 0; i < edgeResolveIterations; i++)
        {
            float angle = (minAngle + maxAngle / 2);
            ViewCastInfo newViewCast = ViewCast(angle);
            if (newViewCast.hit == minViewCast.hit)
            {
                minAngle = angle;
                minPoint = newViewCast.point;
            }
            else
            {
                maxAngle = angle;
                maxPoint = newViewCast.point;
            }
        }

        return new EdgeInfo(minPoint, maxPoint);
    }



    ViewCastInfo ViewCast(float globalAngle)                            //Función que retorna la información detectada por el rayo. 
    {
        Vector3 direction = DirectionFromAngle(globalAngle, false);     //Dirección en la que va dirigido el rayo.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, viewRadius, obstacleMask);      //  Toma el dato de cada Hit. 
        if (Physics2D.Raycast(transform.position, direction, viewRadius, obstacleMask))                     //Si es que hay un hit devuelve información del golpe. 
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else                                                            //De lo contrario regresa los valores máximos según el radio. 
        {
            return new ViewCastInfo(false, transform.position + direction * viewRadius, viewRadius, globalAngle);
        }
    }

    public struct ViewCastInfo                                                      //Struct: Estructura de datos que se puede crear como un paquete de información. 
    {
        public bool hit;                // Golpeó o no golpeó?
        public Vector3 point;           // En qué punto terminó el rayo. 
        public float distance;          // Distancia desde el punto 0 al punto de término. 
        public float angle;             // Ángulo en que estoy mirando. 

        public ViewCastInfo(bool _hit, Vector3 _point, float _dist, float _angle)
        {
            hit = _hit;
            point = _point;
            distance = _dist;
            angle = _angle;
        }
    }
    public Vector3 DirectionFromAngle(float angleIndegrees, bool angleIsGlobal)         // A partir del ángulo entregado calcula la dirección. 
    {
        if (!angleIsGlobal)                                                             //Si el ángulo no es global lo relaciona al objeto. 
        {
            angleIndegrees -= transform.eulerAngles.z;
        }
        return new Vector3(Mathf.Sin(angleIndegrees * Mathf.Deg2Rad), Mathf.Cos(angleIndegrees * Mathf.Deg2Rad), 0);
    }


    public struct EdgeInfo
    {
        public Vector3 pointA;                             // Lugar mas cercano a la orilla (edge) dentro del obstáculo
        public Vector3 pointB;                             // Lugar más cercano a la orilla fuera del obstáculo. 

        public EdgeInfo(Vector3 _pointA, Vector3 _pointB)
        {
            pointA = _pointA;
            pointB = _pointB;
        }
    }
}
