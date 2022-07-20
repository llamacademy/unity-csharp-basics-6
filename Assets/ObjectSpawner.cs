using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private float AutoDestroyTime = 10f;
    [SerializeField]
    private bool SpawnInFixedUpdate = false;
    [SerializeField]
    private AutoDestroyText CountDownPrefab;
    [SerializeField]
    private AutoDestroyText StaticTextPrefab;
    private int TotalTextsDied = 0;

    private List<AutoDestroyText> AvailableObjects = new List<AutoDestroyText>();

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            AvailableObjects.Add(Instantiate(
                CountDownPrefab,
                new Vector3(
                    Random.Range(0, Screen.width),
                    Random.Range(0, Screen.height),
                    0
                ),
                Quaternion.identity,
                transform
            ));
            AvailableObjects[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < 10; i++)
        {
            AvailableObjects.Add(Instantiate(
                StaticTextPrefab,
                new Vector3(
                    Random.Range(0, Screen.width),
                    Random.Range(0, Screen.height),
                    0
                ),
                Quaternion.identity,
                transform
            ));
            AvailableObjects[i + 10].gameObject.SetActive(false);
        }

        AvailableObjects.Sort((item1, item2) => Random.Range(-1, 1));

        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        WaitForSeconds wait = new WaitForSeconds(0.5f);
        while(true)
        {
            SpawnObject();
            yield return wait;
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(20, 60, 200, 20), $"Texts Died: {TotalTextsDied}");
    }

    private void SpawnObject()
    {
        if (AvailableObjects.Count == 0)
        {
            AvailableObjects.Add(Instantiate(
                Random.value > 0.5f ? CountDownPrefab : StaticTextPrefab,
                new Vector3(
                    Random.Range(0, Screen.width),
                    Random.Range(0, Screen.height),
                    0
                ),
                Quaternion.identity,
                transform
            ));
        }

        AutoDestroyText text = AvailableObjects[0];

        text.OnDeath += HandleTextDeath;
        text.AutoDestroyTime = AutoDestroyTime;
        
        text.gameObject.SetActive(true);
        AvailableObjects.RemoveAt(0);
    }

    public void HandleTextDeath(AutoDestroyText Instance, Vector2 Position)
    {
        TotalTextsDied++;
        AvailableObjects.Add(Instance);
        Instance.OnDeath -= HandleTextDeath;
    }
}
