using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcBehavior : MonoBehaviour
{
    Vector3 walkpoint;
    Transform maincharacter;
    bool isAttacking;
    bool isPatrolling;
    // Start is called before the first frame update
    void Start()
    {
        maincharacter = FindObjectOfType<RubyController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAttacking)
        {
        transform.position=Vector3.MoveTowards(transform.position,maincharacter.position,0.03f);
        GetComponent<Animator>().SetFloat("speed" ,0.11f );       
        }
        if(isPatrolling)
        {
            transform.position=Vector3.MoveTowards(transform.position,walkpoint,0.03f);
            GetComponent<Animator>().SetFloat("speed" ,0.11f );  
            if (Vector3.Distance(walkpoint,transform.position)<.3)
            {
                isPatrolling = false;
            }
        }
    }

    void Findwalkpoint()
    {
        walkpoint = new Vector3(transform.position.x+Random.Range(-10,10),transform.position.y+Random.Range(-10,10),transform.position.z);

    }

    void OnCollisionStay2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null )
        {
            if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("idle") || GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("walk") ){
                isAttacking =true;
                GetComponent<Animator>().SetFloat("speed" ,0f );       
                GetComponent<Animator>().SetTrigger("attack");       
            }
        }
        if(other.gameObject.CompareTag("Lake"))
        {
            Findwalkpoint();
        isPatrolling = true;
        }
    }
   
    void OnCollisionExit2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null )
        {
            isAttacking =false;
            GetComponent<Animator>().SetTrigger("Idle");       
        }
    }

    public void DamageCharacter()
    {
      FindObjectOfType<RubyController>().ChangeHealth(-2);  
    } 
}
