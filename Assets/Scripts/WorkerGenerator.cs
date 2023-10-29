using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerGenerator : MonoBehaviour
{
    public GameObject workerPrefab;
    public List<Worker> GeneratedWorkers;
    public Transform workerHolder;
    public Transform spawnTransform;
    public Transform FarmTransform;

    public WorkerManager workerManager;

    private void Awake()
    {
        GeneratedWorkers = new List<Worker>();
    }

    private void Start()
    {
        GenerateWorker();        
    }

    public void GenerateWorker()
    {
        Worker worker = Instantiate(workerPrefab, workerHolder).GetComponent<Worker>();
        worker.Initiallize(spawnTransform.position, FarmTransform.position, workerManager);
        workerManager.workers.Add(worker);
    }
}
