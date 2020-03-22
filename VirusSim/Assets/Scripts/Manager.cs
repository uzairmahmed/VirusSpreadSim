using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject organismPrefab;

    public List<GameObject> organisms;
    public List<GameObject> healthyOrganisms;
    public List<GameObject> sickOrganisms;
    public List<GameObject> curedOrganisms;
    public List<GameObject> deadOrganisms;

    public GameObject allBuildings;

    public List<GameObject> houses;
    public List<GameObject> offices;
    public List<GameObject> schools;
    public List<GameObject> quarantines;
    public List<GameObject> businesses;
    public List<GameObject> hospitals;

    public int numOrganisms = 64;
    public int houseCap = 4;
    public int officeCap = 2;
    public int schoolCap = 2;
    public int businessCap = 2;
    public int hospitalCap = 2;

    public List<GameObject> tempHouses;
    public List<GameObject> tempDayZones;

    public float WorldTime;
    public float timeVariable;

    // Start is called before the first frame update
    void Start()
    {
        initBuildings();
        spawnOrganisms();
    }

    // Update is called once per frame
    void Update()
    {
        organismCommuteManager();
    }

    void initBuildings()
    {
        foreach (Transform child in allBuildings.transform)
        {
            GameObject building = child.gameObject;
            if (building.tag.Equals("Quarantine")) quarantines.Add(building);
            else if (building.tag.Equals("Business"))
                initBuilding(1, building, businessCap, businesses);
            else if (building.tag.Equals("Hospital"))
                initBuilding(1, building, hospitalCap, hospitals);
            else if (building.tag.Equals("Office"))
                initBuilding(1, building, officeCap, offices);
            else if (building.tag.Equals("School"))
                initBuilding(1, building, schoolCap, schools);
            else if (building.tag.Equals("House"))
                initBuilding(0, building, houseCap, houses);
        }
        int x = tempHouses.Count - tempDayZones.Count;
        for (int i = 0; i < x; i++) 
            tempDayZones.Add(tempHouses[Random.Range(0, tempDayZones.Count)]);
    }

    void initBuilding(int m, GameObject b, int c, List<GameObject> l)
    {
        if (m == 0)
        {
            houses.Add(b);
            for(int i = 0; i < c; i++) tempHouses.Add(b);
        } else if (m == 1)
        {
            l.Add(b);
            for (int i = 0; i < c; i++) tempDayZones.Add(b);
        }
    }
    void spawnOrganisms()
    {
        int randomInfectedOrg = Random.Range(0, numOrganisms-1);

        for (int i = 0; i < numOrganisms; i++)
        {
            int tempHomeI = Random.Range(0, tempHouses.Count-1);
            int tempDayZI = Random.Range(0, tempDayZones.Count-1);

            GameObject tempHome = tempHouses[tempHomeI];
            GameObject tempDayZ = tempDayZones[tempDayZI];

            GameObject organism = Instantiate(organismPrefab, tempHome.transform.position, Quaternion.identity);
            organisms.Add(organism);

            Organism og = organism.GetComponent<Organism>();
            
            if (i == randomInfectedOrg) og.initOrganism(tempHome, tempDayZ, 1);
            else og.initOrganism(tempHome, tempDayZ, 0);

            tempHouses.RemoveAt(tempHomeI);
            tempDayZones.RemoveAt(tempDayZI);
        }
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
