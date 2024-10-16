using UnityEngine;
public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource cardClick, winSound, loseSound;
    public void CardClick()
    {
        cardClick.Play();
    }
    public void WinSound()
    {
        winSound.Play();
    }
    public void LoseSound()
    {
        loseSound.Play();
    }
}