using UnityEngine;

public class FootstepPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] footstepClips;

    private int lastPlayedIndex = -1;

    public void PlayFootstep()
    {
        if (footstepClips.Length == 0) return;

        int randomIndex;

        // Prevent same clip twice in a row
        do
        {
            randomIndex = Random.Range(0, footstepClips.Length);
        }
        while (randomIndex == lastPlayedIndex && footstepClips.Length > 1);

        lastPlayedIndex = randomIndex;

        audioSource.pitch = Random.Range(0.95f, 1.05f); // small realism variation
        audioSource.PlayOneShot(footstepClips[randomIndex]);
    }
}