using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindTester : MonoBehaviour, IMove
{
    [SerializeField] Node target;
    [SerializeField] Node start;
    private Queue<Node> path = new Queue<Node>();

    public float speed;

    FSM<EnemyStates> _fsm;
    ITreeNode _root;
    PathFindState<EnemyStates> _statePathfinding;

    void Start()
    {
            InitializeFSM();
            InitializeTree();

        _statePathfinding.OnStartMoving += TestStart;
        _statePathfinding.OnStartMoving += _statePathfinding.SetStartPoint;
        _statePathfinding.OnDestinationReached += TestEnd;
    }

    private void TestStart()
    {
        print("Start Moving");
    }

    private void TestEnd()
    {
        print("Finish Moving");
    }

    void InitializeFSM()
    {
        _fsm = new FSM<EnemyStates>();

        _statePathfinding = new PathFindState<EnemyStates>(this.transform, this, start, target);

        _fsm.SetInitial(_statePathfinding);
    }
    void InitializeTree()
    {
        var follow = new ActionTree(() => _fsm.Transition(EnemyStates.FindPath));

        var qFollowPoints = new QuestionTree(() => _statePathfinding.DestinationReached, follow, follow);
        _root = qFollowPoints;
    }

    void Update()
    {
        _fsm.OnUpdate();
        _root.Execute();
    }



    public void Look(Vector3 direction)
    {
        transform.LookAt(direction);
    }

    public void Look(Transform target)
    {
        this.transform.LookAt(target);
        print("Look 2");
    }

    public void Move(Vector3 direction)
    {
        direction.y = 0;
        transform.position += Time.deltaTime * transform.forward * speed; ;
        print("Moving");
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    bool CheckForGoal(Node currentNode)
    {
        return currentNode == target;
    }

    List<Node> GetNeighbours(Node node)
    {
        return node.Neighbours;
    }

    float CalculateCost(Node parent, Node child)
    {
        float cost = 0;

        cost += Vector3.Distance(parent.transform.position, child.transform.position);

        if (child.hasObstacle)
        {
            cost += 1000;
        }

        return cost;
    }

    public void MoveSlow(Vector3 direction)
    {
        throw new System.NotImplementedException();
    }


}
