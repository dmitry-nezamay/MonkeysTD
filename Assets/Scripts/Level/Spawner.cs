using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct PathAndContent
{
    public Path Path;
    public SpawnContent SpawnContent;
}

[System.Serializable]
public struct Wave
{
    public List<PathAndContent> PathsAndContents;
}

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;

    private int _spawnContentRunningCoroutinesNumber = 0;

    public int WavesNumber => _waves.Count;

    public event UnityAction<int> NewWaveStarted;

    public void StartSpawn()
    {
        StopAllCoroutines();
        NewWaveStarted?.Invoke(0);
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        for (int i = 0; i < _waves.Count; i++)
        {
            NewWaveStarted?.Invoke(i + 1);
            _spawnContentRunningCoroutinesNumber = 0;
            yield return StartCoroutine(SpawnWave(_waves[i]));
        }
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        foreach (var pathAndContent in wave.PathsAndContents)
        {
            StartCoroutine(SpawnContent(pathAndContent));
            _spawnContentRunningCoroutinesNumber++;
        }

        yield return new WaitUntil(() => _spawnContentRunningCoroutinesNumber == 0);
    }

    private IEnumerator SpawnContent(PathAndContent pathAndContent)
    {
        Path path = pathAndContent.Path;
        SpawnContent content = pathAndContent.SpawnContent;

        int _floatersToSpawn = 0;

        foreach (FloaterToSpawn floaterInWave in content.Floaters)
            _floatersToSpawn += floaterInWave.NumberToSpawn;

        float secondsBetweenInstantiation = 0;

        if (_floatersToSpawn > 0)
            secondsBetweenInstantiation = content.Duration / (float)_floatersToSpawn;
        else
            secondsBetweenInstantiation = 0;

        float normalizedTime = 0;
        float _sumOfProbabilities = 0;
        Dictionary<FloaterToSpawn, float> floatersInWaveProbabilities;
        Dictionary<FloaterToSpawn, int> spawnedNumber = new Dictionary<FloaterToSpawn, int>();

        foreach (FloaterToSpawn floaterInWave in content.Floaters)
            spawnedNumber.Add(floaterInWave, 0);

        for (int i = 0; i < _floatersToSpawn; i++)
        {
            floatersInWaveProbabilities = new Dictionary<FloaterToSpawn, float>();
            normalizedTime = i * secondsBetweenInstantiation / content.Duration;
            _sumOfProbabilities = 0;

            foreach (FloaterToSpawn floaterInWave in content.Floaters)
            {
                float probability = floaterInWave.SpawnProbability.Evaluate(normalizedTime);

                if (probability > 0 && floaterInWave.NumberToSpawn > spawnedNumber[floaterInWave])
                {
                    floatersInWaveProbabilities.Add(floaterInWave, floaterInWave.SpawnProbability.Evaluate(normalizedTime));
                    _sumOfProbabilities += floatersInWaveProbabilities[floaterInWave];
                }
            }

            if (_sumOfProbabilities > 0)
            {
                float randomPoint = Random.value * _sumOfProbabilities;

                foreach (FloaterToSpawn floaterInWave in floatersInWaveProbabilities.Keys)
                {
                    if (randomPoint < floatersInWaveProbabilities[floaterInWave])
                    {
                        Floater floater = Instantiate(floaterInWave.Floater, path.SpawnPoint.transform.position, Quaternion.identity, Level.Instance.FloatersContainer);
                        floater.Init(path);
                        spawnedNumber[floaterInWave]++;
                        break;
                    }
                    else
                    {
                        randomPoint -= floatersInWaveProbabilities[floaterInWave];
                    }
                }
            }

            yield return new WaitForSeconds(secondsBetweenInstantiation);
        }

        _spawnContentRunningCoroutinesNumber--;
    }
}
