using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashEffect : MonoBehaviour
{
    [SerializeField] private PlayerSO player;

    [SerializeField] private Material flashMaterial;

    [SerializeField] private SpriteRenderer spriteRenderer;

    private Material originalMaterial;

    private Coroutine flashRoutine;


    void Start()
    {
        originalMaterial = spriteRenderer.material;
    }



    public void Flash()
    {

        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }

        flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        spriteRenderer.material = flashMaterial;

        yield return Waiter.GetWait(player.flashDuration);

        spriteRenderer.material = originalMaterial;

        flashRoutine = null;
    }

}
