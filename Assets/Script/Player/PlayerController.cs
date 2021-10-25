using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Animator enemyAnimator;
    private Animator playerAnimator;
    private BoxCollider enemyHitbox;
    private SphereCollider enemySpherebox;
    private bool isOnEnemyHead = false;
    private int deathCount = 0;

    public int getDeathCount() {
        return deathCount;
    }

    public bool getIsOnEnemyHead(){
        return isOnEnemyHead;
    }
    public void setIsOnEnemyHead(bool value) {
        isOnEnemyHead = value;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Collectible")) {
            hit.gameObject.SendMessage("PickUp");
        }
        if (hit.gameObject.CompareTag("Enemy")) {
            if (hit.collider.GetType() == typeof(SphereCollider)) {
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
                Debug.Log("DED:" + this.gameObject);
                //play death animation
                
                //SceneManager.LoadScene(1);
            }
        }
    }
}
