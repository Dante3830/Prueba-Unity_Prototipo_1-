using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision) {
        // Si el personaje se colisiona con el objeto,
        // el personaje desaparece
        if (collision.transform.CompareTag("Player")) {
            Debug.Log("Player Damaged");
            Destroy(collision.gameObject);
        }
    }
}
