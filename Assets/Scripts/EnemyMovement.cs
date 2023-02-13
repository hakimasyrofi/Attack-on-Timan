using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    public float lookRadius;
    public GameObject player;

    Transform target;
    NavMeshAgent agent;
    [SerializeField] Animator animator;
    HPSlider hpSlider;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("ThirdPersonPlayer");
        target = player.transform;
        agent = GetComponent<NavMeshAgent>();
        hpSlider = player.GetComponent<HPSlider>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius){
            agent.SetDestination(target.position);

            animator.SetBool("isMoving", true);

            if (distance <= agent.stoppingDistance){
                //Attack target
                // face target
                // Debug.Log(hpSlider.life);
                FaceTarget();


                hpSlider.life -=1;
                if (hpSlider.life == 0){
                    ReloadLevel();
                }
            }
        }

        else {
            animator.SetBool("isMoving", false);
        }

    }

    void FaceTarget(){
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    // void Die(){
    //     death.gameObject.SetActive(true);
    //     Invoke(nameof(ReloadLevel), 1.3f);
    //     death.gameObject.SetActive(false);
    // }
    

    void ReloadLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
