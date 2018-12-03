using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public Carousel carousel;
    public void StartGame()
    {
        carousel.audioSource.Play();
        SceneManager.LoadScene("Game");
    }
}
