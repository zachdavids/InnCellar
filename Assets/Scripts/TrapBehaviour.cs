using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBehaviour : MonoBehaviour
{
    private ArtisanScript creator;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.GetComponent<RatBehaviour>() != null)
        {
            Debug.Log("A rat has touched a rat trap!");
            if (creator != null) { creator.NotifyTrapDestroyed(this.gameObject); }
            GameObject.Destroy(this.gameObject);
        }
    }

    public void SetCreator(ArtisanScript artisan)
    {
        creator = artisan;
    }
}
