using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FunctionTimer 
{
    public static FunctionTimer Create(Action action, float timer)
	{
        GameObject gameObject = new GameObject("FunctionTimer", typeof(MonoBehaviourHook));
        FunctionTimer functionTimer = new FunctionTimer(action, timer, gameObject);

        
        gameObject.GetComponent<MonoBehaviourHook>().onUpdate = functionTimer.Update;

        return functionTimer;
	}

    public class MonoBehaviourHook : MonoBehaviour
    {
        public Action onUpdate;

        private void Update()
        {
            if (onUpdate != null)
                onUpdate();
        }
    }



    private GameObject gameObject;
    private Action action;
    private float timer;
    private bool isDestroyed;

    // Start is called before the first frame update
   private FunctionTimer(Action action, float timer, GameObject gameObject)
	{
        this.action = action;
        this.timer = timer;
        isDestroyed = false;
        this.gameObject = gameObject;
	}

    // Update is called once per frame
    public void Update()
    {
        if (!isDestroyed)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                action();
                DestroySelf();
            }
        }
    }

    public void DestroySelf()
	{
        isDestroyed = true;
       UnityEngine.Object.Destroy(gameObject);
	}

    public float GetTimeLeft()
	{
        return timer;
	}
    public bool IsDestroyed()
	{
        return isDestroyed;
	}
}

