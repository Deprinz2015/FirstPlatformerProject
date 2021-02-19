using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScript : MonoBehaviour
{
    private Transform trans;
    
    [SerializeField] private float startingX;
    [SerializeField] private float startingY;

    // Start is called before the first frame update
    void Start()
    {
        trans = this.gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.R))
        {
            trans.position = new Vector3(startingX, startingY, trans.position.z);
        }
    }
}
