using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    public float range = 1000f;
    public GameObject hitEffect;
    public GameObject mainCam;
    RaycastHit hit;
    public Text scoreText;
    int score;
    [SerializeField] Animator animator;
    public AudioSource attackEffect;
    float i;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > i){
            i += 0.5f;
            Shoot();
        }
        if (hit.collider.gameObject.tag == "Enemy") {
            score += 100;
            scoreText.text = "Score: "+score;
            attackEffect.Play();
            // StartCoroutine(Death(3f));
            // animator.SetBool("isDead", true);
            Destroy(hit.collider.gameObject);
        }
    }

    void Shoot(){
        
        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit, range)){
            Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }

    // IEnumerator Death(float delayAmount)
    // {
    //     animator.Play("Zombie Death");
    //     yield return new WaitForSeconds(delayAmount);
    //     score += 100;
    //     scoreText.text = "Score: "+score;
    //     Destroy(hit.collider.gameObject);
    // }
}
