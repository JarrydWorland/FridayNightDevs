using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManger : Singleton<LevelManger>
{
    public PlayerController player;
    public GameObject ForceField;
    public Transform HomeBase;
    public Vector3 StartPoint;
    public List<GameObject> AsteroidContainers;
    public List<GameObject> individualAsts;
    public List<GameObject> collectables;
    public GameObject collectablePrefab;
    public int numOfCollectables;
    public Text collectionCount;
    public CompleteCameraController camera;

    private void Update()
    {
        //if (HomeBase.gameObject.GetComponent<HomeBase>().collected.Count == 0)
        //{

        //collectionCount.text = "Shards Collected: 0/10";
        // }
        // else
        //{
        collectionCount.text = "Shards Collected: " + HomeBase.gameObject.GetComponent<HomeBase>().collected.Count + "/" + numOfCollectables;
        // }
    }
    void Awake()
    {
        StartPoint = player.transform.position;
        individualAsts = new List<GameObject>();
        foreach (GameObject g in AsteroidContainers)
        {
            foreach (Transform t in g.transform)
            {
                individualAsts.Add(t.gameObject);
            }
        }
        CreateCollectable(numOfCollectables);
    }

    public void CreateCollectable(int numberToGenerate)
    {
        if(numberToGenerate>individualAsts.Count)
        {
            numberToGenerate = individualAsts.Count;
        }
        System.Random rnd = new System.Random();
        Vector3 pos;
        List<int> asteroidIndex = new List<int>();
        collectables = new List<GameObject>();
        for (int count = 0; count < numberToGenerate; count++)
        {
            int r = rnd.Next(individualAsts.Count);
            while (asteroidIndex.Contains(r))
            {
                r = rnd.Next(individualAsts.Count);
            }
            asteroidIndex.Add(r);
            Vector3 collectablePos = (Vector3)new Vector2(Random.value, Random.value).normalized;
            collectablePos = collectablePos * collectablePrefab.GetComponent<Collectable>().setDistance;
            pos = individualAsts[r].transform.position + collectablePos;

            collectables.Add(Instantiate(collectablePrefab, pos, transform.rotation));
            collectables[count].GetComponent<Collectable>().attatchedTo = individualAsts[r];
        }
    }

    public void ResetLevel()
    {
        Debug.Log("Level Reset");
        Application.LoadLevel(Application.loadedLevel);
    }
}
