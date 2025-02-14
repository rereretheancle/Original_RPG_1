using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    public string areaToLoad;

    public string thisAreaTransitionName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerController.instance.StopAllCoroutines();
            
            PlayerController.instance.moveDirection = Vector2.zero;

            SceneManager.LoadScene(areaToLoad);

            PlayerController.instance.areaTransitionName = thisAreaTransitionName;
        }
    }
}
