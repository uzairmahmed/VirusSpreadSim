using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> organisms;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        organismCommuteManager();
    }

    void organismCommuteManager()
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("Commuting");
            foreach (GameObject org in organisms)
            {
                Organism og = org.GetComponent<Organism>();
                og.changeCommute();
            }
        }
    }
}
