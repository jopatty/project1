using UnityEngine;
using System.Collections;
using TMPro;

public class EnemyFollow : MonoBehaviour
{
    public Transform companion; // Reference to the Companion
    public float followSpeed; // Speed of following
    public float followDistance; // Distance threshold for following

    void Update()
    {
        //Move toward the Companion
        transform.position = Vector3.MoveTowards(transform.position, companion.position, followSpeed * Time.deltaTime);

        //Position the HP bar above the enemy
        if (hpBarGreen != null)
        {
            hpBarGreen.transform.position = transform.position + hpBarOffset;
            hpBarRed.transform.position = transform.position + hpBarOffset;
        }
    // Calculate distance between Enemy and Companion
    float distance = Vector3.Distance(transform.position, companion.position);

        // If the distance is greater than the follow distance, move toward the player
        if (distance > followDistance)
        {
            Vector3 targetPosition = companion.position;
            targetPosition.z = transform.position.z; // Optional, if 2D
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
    private int CompcurrentHealth;
    public int CompmaxHealth;
    private bool isInvicible = false; //Untuk cooldown hit
    public float invincibilityDuration = 1.5f; // Cooldown
    void Start()
    {
        CompcurrentHealth = CompmaxHealth;
        if (hpBarGreen != null && hpBarRed != null)
        {
            hpBarGreen.SetActive(false); //Hide the green bar initially
            hpBarRed.SetActive(false); //Hide the red bar initially
        }
    CompcurrentHealth = CompmaxHealth; // Companion HP
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage();
            Destroy (collision.gameObject);
            Debug.Log("Hit");
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
    public GameObject hpBarGreen; //The green HP bar (current health)
    public GameObject hpBarRed; //The red HP bar (full health background)
    public Vector3 hpBarOffset = new Vector3(0f, 1f, 0f); //Adjust the offset
    void TakeDamage()
    {
        CompcurrentHealth--;
        if (CompcurrentHealth <= 0)
        {
            Destroyed();
    }
        //Show the HP bar if health is not full or after taking damage
        if (hpBarGreen != null && hpBarRed != null)
        {
            hpBarGreen.SetActive(true); //Show the green bar
            hpBarRed.SetActive(true);
            UpdateHpBar(); //Update HP bar size
        }
    }
    private void UpdateHpBar()
    {
        //Update the green bar's width based on the current health
        float healthPercentage = (float)CompcurrentHealth / CompmaxHealth;
        Vector3 scale = hpBarGreen.transform.localScale;
        scale.x = healthPercentage; //Adjust the width of the green bar
        hpBarGreen.transform.localScale = scale;
    }
}