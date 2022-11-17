using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingScroll : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player) && Input.GetButtonDown("Diside"))
        {
            player.ModeChange(!player.AdultState);
        }
    }
}
