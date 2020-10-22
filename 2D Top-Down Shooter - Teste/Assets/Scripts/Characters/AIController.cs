using System.Linq;
using UnityEngine;

[RequireComponent(typeof(StateMachine))]
public class AIController : Character, IGivePoints
{
    public StateMachine StateMachine { get; private set; }
    public Transform Target { get; private set; }
    public Vector3 Destination { get; private set; }

    [SerializeField] int scorePointsToGive;
    public int ScorePointsToGive => scorePointsToGive;

    [Space(5f)]
    [Header("State Machine Properties")]

    [SerializeField] float awarenessRadius = 10f;
    public float AwarenessRadius => awarenessRadius;

    // Distância mínima para concluir a chegada ao destino.
    [SerializeField] float stoppingThreshold = 1f; 
    public float StoppingThreshold => stoppingThreshold;

    [SerializeField] float minDestinationDistance = 3f;
    public float MinDestionationRange => minDestinationDistance;

    [SerializeField] float maxDestinationDistance = 10f;
    public float MaxDestionationRange => maxDestinationDistance;

    [SerializeField] float sightAngle = 45f;
    [SerializeField] float shootingRange = 5f;
    [SerializeField] LayerMask targetableLayers;
    [SerializeField] bool showDebugGizmos;

    protected override void Awake()
    {
        base.Awake();
        StateMachine = GetComponent<StateMachine>();
    }

    private void Start()
    {
        StateMachine.CreateStates(this);
    }

    public void SetDestination(Vector2 newDestination) => Destination = newDestination;
    
    public bool HasTargetAround()
    {
        if (Target != null ) // Tem um target
        {
            if (!Target.gameObject.activeInHierarchy) // O target morreu
            {
                Target = null;
                return false;
            }

            if(GetVectorToTarget().magnitude >= awarenessRadius +1) // O target saiu do campo de detecção
            {
                Target = null;
                return false;
            }
        }

        Collider2D[] targetsAround = Physics2D.OverlapCircleAll(transform.position, 
                                                                AwarenessRadius, 
                                                                targetableLayers);

        for (int i = 0; i < targetsAround.Length; i++)
        {
            if (targetsAround[i].transform == this.transform)
                continue;

            return true;
        }

        return false;
    }

    public void GetClosestTarget()
    {
        Collider2D[] targetsAround = Physics2D.OverlapCircleAll(transform.position, 
                                                                AwarenessRadius, 
                                                                targetableLayers);

        var targetsInDistanceOrder = targetsAround
                                    .OrderBy(target => 
                                     Vector2.Distance(transform.position, 
                                                      target.transform.position));

        Target = targetsInDistanceOrder.FirstOrDefault().transform;
    }

    public bool IsTargetInSight()
    {
        if (Target == null) 
            return false;

        // Estou acrescentando o tamanho do personagem(1f) para que possa pegar o centro dele para perseguir,
        // pois a detecção do personagem(overlapping) pode acontecer na borda do personagem.
        if (GetVectorToTarget().magnitude <= awarenessRadius + 1f)
        {
            float angleToTarget = Vector2.Angle(transform.up, GetDirectionToTarget());

            if (angleToTarget <= sightAngle)
                return true;
        }
        return false;
    }

    public bool IsTargetInShootingDistance()
    {
        if(Target)
            return GetVectorToTarget().magnitude < shootingRange;

        return false;
    }

    public Vector2 GetDirectionToTarget()
    {
        if(Target)
            return GetVectorToTarget().normalized;

        return Vector2.zero;
    }

    public Vector2 GetVectorToTarget()
    {
        if(Target)
            return (Target.transform.position - this.transform.position);

        return Vector2.zero;
    }

    #region Gizmos
    void OnDrawGizmos()
    {
        if (showDebugGizmos)
        {
            Vector3 angleA = DirectionFromAngle(-sightAngle, false);
            Vector3 angleB = DirectionFromAngle(sightAngle, false);

            Gizmos.color = Color.black;
            Gizmos.DrawLine(this.transform.position, this.transform.position + angleA * awarenessRadius);
            Gizmos.DrawLine(this.transform.position, this.transform.position + angleB * awarenessRadius);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(this.transform.position, this.transform.position + transform.up * shootingRange);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(this.transform.position,awarenessRadius);

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(Destination, 0.3f);
        }
    }

    Vector2 DirectionFromAngle(float angleInDregrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDregrees += transform.eulerAngles.z;
        }

        return new Vector2(Mathf.Sin(angleInDregrees * Mathf.Deg2Rad) * -1, Mathf.Cos(angleInDregrees * Mathf.Deg2Rad));
    }
    #endregion
}