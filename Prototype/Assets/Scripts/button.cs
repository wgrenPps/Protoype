using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class button : MonoBehaviour
{
    Scene scene;
    void Start() {
        scene = SceneManager.GetActiveScene();
    }

    public void DeathReset() {
        SceneManager.LoadScene(scene.name);
    }
}
