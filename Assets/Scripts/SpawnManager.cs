using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject[] enemyPrefab;

    [Header("Wave")]
    public int inimigosPorWave = 20;
    public float tempoEntreWaves = 10f;
    private float tempoEntreSpawns = 1f;

    int inimigosVivos;
    int waveAtual = 1;
    [SerializeField] Text inimigosRestantes;
    [SerializeField] Text proximaWaveContagem;
    [SerializeField] Text wave;
    [SerializeField] GameObject ContagemCanva;

    void Start()
    {
        StartCoroutine(IniciarWave());
    }

    void LateUpdate()
    {
        textos();
    }

    GameObject EscolherInimigo()
    {
        int roll = Random.Range(0, 10); // 0 a 9

        if (roll < 7)
            return enemyPrefab[0]; // comum (70%)
        else if (roll < 9)
            return enemyPrefab[1]; // rápido (20%)
        else
            return enemyPrefab[2]; // robusto (10%)
    }

    void textos()
    {
        
        inimigosRestantes.text = inimigosVivos + "/" + inimigosPorWave;
        wave.text = "Wave: " + waveAtual;
    }

    void AtualizarDificuldade()
    {
        if (waveAtual > 7)
        {
            tempoEntreSpawns = 0.4f;
        }
        else if (waveAtual > 5)
        {
            tempoEntreSpawns = 0.6f;
        }
        else if (waveAtual > 3)
        {
            tempoEntreSpawns = 0.8f;
        }
    }


    IEnumerator IniciarWave()
    {
        inimigosVivos = inimigosPorWave;

        for (int i = 0; i < inimigosPorWave; i++)
        {
            SpawnarInimigo();
            yield return new WaitForSeconds(tempoEntreSpawns);
        }
    }

    void SpawnarInimigo()
    {
        Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject inimigo = EscolherInimigo();
        Instantiate(inimigo, spawn.position, Quaternion.identity);
    }

    public void InimigoMorreu()
    {
        inimigosVivos--;

        if (inimigosVivos <= 0)
        {
            StartCoroutine(ProximaWave());
        }
    }

    IEnumerator ProximaWave()
    {
        Debug.Log("Wave " + waveAtual + " finalizada!");

        float tempo = tempoEntreWaves;

        while (tempo > 0)
        {
            ContagemCanva.SetActive(true);
            proximaWaveContagem.text = "Próxima Wave: " + Mathf.CeilToInt(tempo) + "s";
            tempo -= Time.deltaTime;
            yield return null;
        }

        proximaWaveContagem.text = "";

        waveAtual++;
        inimigosPorWave += 2;
        AtualizarDificuldade();
        StartCoroutine(IniciarWave());
    }

}
