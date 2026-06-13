using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectFiles.Scripts.TriggerObject
{
    public class NextLevelTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player")) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}