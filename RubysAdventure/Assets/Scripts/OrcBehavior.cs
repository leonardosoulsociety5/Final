using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcBehavior : MonoBehaviour
{
    Vector3 walkpoint;
    Transform maincharacter;
    bool isAttacking;
    bool isPatrolling;
    public float speed;

    public AudioClip hurtSound;

    private float oldPosition = 0.0f;

    public int health = 3;
    // Start is called before the first frame update
    void Start()
    {
        maincharacter = FindObjectOfType<RubyController>().transform;
        oldPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAttacking)
        {
        transform.position=Vector3.MoveTowards(transform.position,maincharacter.position,speed);
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

        if(transform.position.x > oldPosition){
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        if(transform.position.x < oldPosition){
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        oldPosition = transform.position.x;
    }

    void Findwalkpoint()
    {
        walkpoint = new Vector3(transform.position.x+Random.Range(-10,10),transform.position.y+Random.Range(-10,10),transform.position.z);

    }

    void OnTriggerStay2D(Collider2D other)
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
   
    void OnTriggerExit2D(Collider2D other)
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

    public void DamageOrc()
    {
        AudioSource source = GetComponent<AudioSource>();
        source.PlayOneShot(hurtSound);
        health--;

        if(health == 0){
            StartCoroutine(DestroyGameObject());
        }
    }

    IEnumerator DestroyGameObject( ){
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }

}
