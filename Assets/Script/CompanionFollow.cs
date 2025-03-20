using System.Collections;
using UnityEngine;

public class CompanionFollow : MonoBehaviour
{
    public Transform player; // Reference to the Player
    public float followSpeed; // Speed of following
    public float followDistance; // Distance threshold for following

        void Update()
    {
        // Calculate distance between Companion and Player
        float distance = Vector3.Distance(transform.position, player.position);

        // If the distance is greater than the follow distance, move toward the player
        if (distance > followDistance)
        {
            Vector3 targetPosition = player.position;
            targetPosition.z = transform.position.z; // Optional, if 2D
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
    private int CompcurrentHealth;
    public int CompmaxHealth;
    private bool isInvicible = false; //Untuk cooldown hit
    public float invincibilityDuration = 1.5f; // Cooldown
    private void Start()
    {
        CompcurrentHealth = CompmaxHealth; // Companion HP
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !isInvicible)
        {
            TakeDamage();
            Debug.Log("Companion Hit!");
        }
    }
    void TakeDamage()
    {
        //returns the higher of the two values.
        CompcurrentHealth = Mathf .Max(CompcurrentHealth - 1, 0);
        if (CompcurrentHealth < 0)
        {
            Destroyed();
        }
        else
        {
            StartCoroutine(InvicibilityCooldown()); //Mulai cooldown
        }
    }
    IEnumerator InvicibilityCooldown()
    {
        isInvicible = true; //Companion kebal
        yield return new WaitForSeconds(invincibilityDuration); //Tunggu
        isInvicible = false; //Bisa kena hit lagi
    }
    void Destroyed()
    {
        
    }
}
