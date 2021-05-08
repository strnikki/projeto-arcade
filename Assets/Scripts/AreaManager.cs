using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Trocou de area");
        gameManager.ChangePlayerArea();
    }
}
