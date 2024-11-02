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

    //private Node currentDestination;
    //bool moving = false;

    // Start is called before the first frame update
    void Start()
    {
        //var temp = BFS_Dijkstra.Run<Node>(start, CheckForGoal, GetNeighbours, CalculateCost);
        //foreach (var node in temp)
        //{
        //    path.Enqueue(node);
        //}
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
        //var idle = new CrashStateIdle<EnemyStates>(_anim);
        _statePathfinding = new PathFindState<EnemyStates>(this.transform, this, start, target);

        //idle.AddTransition(EnemyStates.Waypoints, _statePathfinding);
        //_statePathfinding.AddTransition(EnemyStates.Idle, idle);
        _fsm.SetInitial(_statePathfinding);
    }
    void InitializeTree()
    {
        //var idle = new ActionTree(() => _fsm.Transition(EnemyStates.Idle));
        var follow = new ActionTree(() => _fsm.Transition(EnemyStates.FindPath));

        var qFollowPoints = new QuestionTree(() => _statePathfinding.DestinationReached, follow, follow);
        _root = qFollowPoints;
    }

    // Update is called once per frame
    void Update()
    {
        _fsm.OnUpdate();
        _root.Execute();

        //if (path.Count > 0 && !moving)
        //{
        //    currentDestination = path.Dequeue();            
        //    moving = true;
        //}       

        //if (moving)
        //{
        //    //transform.Translate(currentDestination.transform.position);
        //}

        //if (transform.position == currentDestination.transform.position)
        //{
        //    moving = false;
        //}

    }

    public void RePathDijkstra()
    {
        _statePathfinding._start = start;
        _statePathfinding._goal = target;
        _statePathfinding.SetPathDijkstra();
    }

    public void Look(Vector3 direction)
    {
        //if (Vector3.Angle(transform.forward, direction) > (Mathf.PI * Mathf.Rad2Deg) / 2)
        //{
        //    transform.forward = direction;
        //}
        //else
        //{
        //    transform.forward = Vector3.Lerp(transform.forward, direction, 150 * Time.deltaTime);
        //}

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

        if (child.hasBonus)
        {
            cost -= 500;
        }

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
