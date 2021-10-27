using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    public AudioSource enemyDeath;
    public AudioClip enemyDeathClip;
    private Animator enemyAnimator;
    private Animator playerAnimator;
    private BoxCollider enemyHitbox;
    private SphereCollider enemySpherebox;
    private bool isOnEnemyHead = false;
    private int deathCount = 0;

    public bool getIsOnEnemyHead(){
        return isOnEnemyHead;
    }
    public void setIsOnEnemyHead(bool value) {
        isOnEnemyHead = value;
    }

    public void Start() {
        deathCount = PlayerPrefs.GetInt("deaths");
        enemyDeath.clip = enemyDeathClip;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Collectible")) {
            hit.gameObject.SendMessage("PickUp");
        }
        if (hit.gameObject.CompareTag("Enemy")) {
            if (hit.collider.GetType() == typeof(SphereCollider)) {
                enemyDeath.Play();
                enemyAnimator = hit.gameObject.GetComponent<Animator>();
                enemyHitbox = hit.gameObject.GetComponent<BoxCollider>();
                enemySpherebox = hit.gameObject.GetComponent<SphereCollider>();

                isOnEnemyHead = true;
                Destroy(enemyHitbox);
                Destroy(enemySpherebox);
                
                enemyAnimator.Play("Die");
                Destroy(hit.gameObject, enemyAnimator.GetCurrentAnimatorStateInfo(0).length);
            }
            else {
                deathCount++;
                PlayerPrefs.SetInt("deaths", deathCount);
                SceneManager.LoadScene(2);
            }
        }
        if (hit.gameObject.CompareTag("Door")) {
            //Change to LVL2 instead of Win menu
            SceneManager.LoadScene(5);
        }
        if (hit.gameObject.CompareTag("Win")) {
            //Change to LVL2 instead of Win menu
            SceneManager.LoadScene(4);
        }
    }
}
