using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BombController : MonoBehaviour
{
    [SerializeField] GameObject bombPosition;


    GameObject[] allBarriers;
    BarrierControl barrierShutdown;
    
    // Start is called before the first frame update
    void Start()
    {
        
        allBarriers = GameObject.FindGameObjectsWithTag("Barrier");
        if (allBarriers == null)
        {
            Destroy(gameObject);
        }
        else {
            GameObject targetBarrier = allBarriers[0];


            float nearestBarrier = Vector3.Distance(bombPosition.transform.position, targetBarrier.transform.position);
            for (int i = 1; i < allBarriers.Length; i++)
            {
                float currentBarrier = Vector3.Distance(bombPosition.transform.position, allBarriers[i].transform.position);
                if (currentBarrier < nearestBarrier)
                {
                    targetBarrier = allBarriers[i];
                    nearestBarrier = currentBarrier;
                }

            }


            barrierShutdown = targetBarrier.GetComponent<BarrierControl>();
            StartCoroutine(BarrierDeactivation(20));
        }
        
    }

    // Update is called once per frame
/*    void Update()
    {
        
    }*/


    IEnumerator BarrierDeactivation(float deactivationTime) {
        
        yield return new WaitForSeconds(20f);
        barrierShutdown.active = false;
        Destroy(gameObject);
    }


}
