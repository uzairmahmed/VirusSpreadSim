using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

    public int capacity;
    public List <GameObject> organisms;

    // Start is called before the first frame update
    void Start()
    {
        organisms = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
