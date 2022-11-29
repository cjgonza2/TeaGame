using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChracterManager : MonoBehaviour
{
    public GameManager manager;

    [SerializeField] 
    private Animator lambAnim;
    // Start is called before the first frame update
    void Start()
    {
        if (manager.currentState == GameManager.State.Enter)
        {
            lambAnim.Play("LombardoEnter", 0,0f); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
