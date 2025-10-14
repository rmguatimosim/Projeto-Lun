using System.Collections.Generic;

using UnityEngine;

public class FragmentLauncher : MonoBehaviour
{
    public GameObject dataFragment;
    public List<int> dataValors;
    private MeleeCreatureController mc;
    public GameObject spawnPoint;
    public GameObject target;
    public Vector2 force;
    public Vector2 arcDegrees;
    public float rangeInDegrees;
    private float cooldown;

    void Awake()
    {
        mc = gameObject.GetComponent<MeleeCreatureController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        cooldown = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //ignore if game is over
        if (GameManager.Instance.isGameOver) return;
        cooldown -= Time.deltaTime;

        if (mc.hitWall && cooldown <= 0)
        {
            Fire();
            Fire();
            Fire();
            mc.hitWall = false;
        }
    }

    private void Fire()
    {
        //Get prefab
        GameObject fragmentPrefab = dataFragment;
        fragmentPrefab.GetComponent<Data>().content = dataValors[Random.Range(0, dataValors.Count)];
        //Create bomb
        GameObject fragment = Instantiate(fragmentPrefab, spawnPoint.transform.position, fragmentPrefab.transform.rotation);

        //Apply force
        Rigidbody fragmentRigidBody = fragment.GetComponent<Rigidbody>();
        Vector3 impulseVector = CalculateImpulse();
        // Vector3 impulseVector = target.transform.position - spawnPoint.transform.position;
        // impulseVector.Scale(new(1, 0, 1));
        // impulseVector.Normalize();
        // impulseVector += new Vector3(0, Random.Range(arcDegrees.x, arcDegrees.y) / 45f, 0);
        // impulseVector.Normalize();
        // impulseVector = Quaternion.AngleAxis(rangeInDegrees * Random.Range(-1f, 1f), Vector3.up) * impulseVector;
        // impulseVector *= Random.Range(force.x, force.y);
        fragmentRigidBody.AddForce(impulseVector, ForceMode.Impulse);
    }

    private Vector3 CalculateImpulse()
    {
        Vector3 impulseVector = target.transform.position - spawnPoint.transform.position;
        impulseVector.Scale(new(1, 0, 1));
        impulseVector.Normalize();
        impulseVector += new Vector3(0, Random.Range(arcDegrees.x, arcDegrees.y) / 45f, 0);
        impulseVector.Normalize();
        impulseVector = Quaternion.AngleAxis(rangeInDegrees * Random.Range(-1f, 1f), Vector3.up) * impulseVector;
        impulseVector *= Random.Range(force.x, force.y);
        return impulseVector;
    }

}
