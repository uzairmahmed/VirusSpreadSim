using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Organism : MonoBehaviour
{
    public GameObject home;
    public GameObject location;

    private NavMeshAgent nav;
    protected GameObject dest;
    protected int target;

    public int state; //0: healthy, 1: sick, 2: cured, -1: dead
    public int strength;
    public int health;
    public int infectivity;

    private MeshRenderer mesh;
    public Material healthyMat;
    public Material sickMat;
    public Material curedMat;
    public Material deadMat;

    // Start is called before the first frame update
    void Start()
    {
        target = 0;
        dest = home;

        nav = GetComponent<NavMeshAgent>();
        mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        commuteManager();
        nav.SetDestination(dest.transform.position);

        stateManager();
    }

    public void initOrganism(GameObject h, GameObject l, int s)
    {
        home = h;
        location = l;
        state = s;
    }

    public void changeState(int s)
    {
        state = s;
    }

    public void changeCommute()
    {
        if (target == 0) target = 1;
        else if (target == 1) target = 0;
    }

    void stateManager()
    {
        if (state == 0)
        {
            mesh.material = healthyMat;
        }
        else if (state == 1)
        {
            mesh.material = sickMat;
        }
        else if (state == 2)
        {
            mesh.material = curedMat;
        }
        else if (state == -1)
        {
            mesh.material = deadMat;
        }
    }

    void commuteManager()
    {
        if (target == 0) dest = home;
        else if (target == 1) dest = location;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Organism")
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                Debug.DrawRay(contact.point, contact.normal, Color.black, 1);
                Debug.Log("Yes");
            }

            GameObject collidedOrg = collision.gameObject;
            Organism collidedScript = collidedOrg.GetComponent<Organism>();
            if (collidedScript.state == 1) state = 1;
        }
    }
}
