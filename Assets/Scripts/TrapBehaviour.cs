using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBehaviour : MonoBehaviour
{
    private ArtisanScript _creator;
    
    #region Monobehaviour Functions

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<RatBehaviour>())
        {
            Debug.Log("A rat has touched a rat trap!");
            if (_creator != null)
            {
                _creator.NotifyTrapDestroyed(gameObject);
            }

            GameObject.Destroy(gameObject);
        }
    }

    public void SetCreator(ArtisanScript artisan)
    {
        _creator = artisan;
    }

    #endregion
}
