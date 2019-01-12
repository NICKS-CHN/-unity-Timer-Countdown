using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        JSTimer.Instance.SetupTimer("testTimer", () => { Debug.Log("timer"); });
        JSTimer.Instance.SetupCoolDown("testCooldown", 60, (timer) => { Debug.Log("testCooldown" + timer); },null);

    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnDisable()
    {
        //计时器不用时要手动移除，否则会一直执行
        JSTimer.Instance.CancelTimer("testTimer");
    }
}
