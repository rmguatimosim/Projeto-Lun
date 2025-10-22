using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public float fadeOutDuration = 3f;
    public void FadeOut()
    {
        // if (isFadingOut) return;
        // isFadingOut = true;

        //Begin animator
        // thisAnimator.enabled = true;

        //schedule scene
        StartCoroutine(TransitionToNextScene());
    }

    private IEnumerator TransitionToNextScene()
    {
        yield return new WaitForSeconds(fadeOutDuration);
        SceneManager.LoadScene("Scenes/SceneOne");
    }
}
