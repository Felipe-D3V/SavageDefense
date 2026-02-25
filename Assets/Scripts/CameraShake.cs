using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    Vector3 posicaoInicial;

    float shakeTempo;
    float shakeForca;

    private void Awake()
    {
        Instance = this;
        posicaoInicial = transform.localPosition;
    }


    void Update()
    {
        if (shakeTempo > 0)
        {
            transform.localPosition = posicaoInicial + (Vector3)Random.insideUnitCircle * shakeForca;
            shakeTempo -= Time.unscaledDeltaTime; // funciona mesmo pausado
        }
        else
        {
            shakeTempo = 0f;
            transform.localPosition = posicaoInicial;
        }
    }

    public void Shake(float duracao, float forca)
    {
        shakeTempo = duracao;
        shakeForca = forca;
    }
}
