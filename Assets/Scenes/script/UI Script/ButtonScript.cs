using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] AudioSource endSound;
    [SerializeField] Animator exitAnimator;
    [SerializeField] Animator PlayAnimator;
    [SerializeField] Animator CreditAnimator, CreditButton, CloseCreditButton;

    public void PlayGame()
    {
        PlayAnimator.SetBool("selected", true);
        StartCoroutine(SoundPlaying());
        SceneManager.LoadScene(1);
    }
    public void OpenCredit()
    {
        CreditButton.SetBool("selected", true);
        CreditAnimator.SetTrigger("open");
        StartCoroutine(SoundPlaying());
    }
    public void CloseCredit()
    {
        CreditButton.SetBool("selected", false);
        CreditAnimator.SetTrigger("close");
        StartCoroutine(SoundPlaying());
    }
    public void KeluarAplikasi()
    {
        exitAnimator.SetBool("selected", true);
        StartCoroutine(SoundExit());
        Application.Quit();
    }
    IEnumerator SoundPlaying()
    {
        endSound.PlayOneShot(endSound.clip);
        yield return new WaitForSeconds(endSound.clip.length + 4);
    }
    IEnumerator SoundExit()
    {
        endSound.PlayOneShot(endSound.clip);
        yield return new WaitForSeconds(endSound.clip.length);
    }
}
