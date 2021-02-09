using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameProcess : MonoBehaviour
{
    [SerializeField]
    private List<Cube> AllCubes;
    [SerializeField]
    private GameObject[] Cubes;
    [SerializeField]
    private Text _score;
    private int _currentScore = 0;

    private static Animator _gameOverPanel;
    private static Animation _winPanel;

    private GameObject _nextCube;

    private BitterController _bitterController;
    private CubesStorage _storage;

    public int SpawnCd;

    public static bool IsGameOver = false;

    private void Start()
    {
        IsGameOver = false;

        _storage = GameObject.FindWithTag("CubesStorage").GetComponent<CubesStorage>();
        _bitterController = GameObject.FindWithTag("BitterController").GetComponent<BitterController>();
        _gameOverPanel = GameObject.Find("GameOverPanel").GetComponent<Animator>();
        _winPanel = GameObject.Find("WinPanel").GetComponent<Animation>();

        UpdateLists();
    }

    public void EndTurn()
    {
        if(!IsGameOver)
        {
            _currentScore++;
            _score.text = _currentScore.ToString();
            _score.GetComponent<Animation>().Play();
        }

        StartCoroutine(SpawnBlock());
    }

    private void UpdateLists()
    {
        AllCubes.Clear();
        Cubes = new GameObject[0];

        Cubes = GameObject.FindGameObjectsWithTag("CubeNominal");

        for(int i = 0; i < Cubes.Length; i++)
        {
            AllCubes.Add(Cubes[i].GetComponent<Cube>());
        }
    }

    private GameObject FindMinWeight()
    {
        int min = 999;
        for(int i = 0; i < AllCubes.Count; i++)
        {
            if (min > AllCubes[i].WeightId) min = AllCubes[i].WeightId;
        }
        return _storage.CubesBitters[min];
    }

    private IEnumerator SpawnBlock()
    {
        yield return new WaitForSeconds(SpawnCd);

        NextTurn();
    }

    public void NextTurn()
    {
        UpdateLists();

        _nextCube = FindMinWeight();

        Instantiate(_nextCube, new Vector3(0, 0.75f, -8), Quaternion.identity);
        _bitterController.UpdateBitter();
    }

    public static void GameEnd()
    {
        IsGameOver = true;

        _winPanel.Play();
    }

    public static void GameOver()
    {
        IsGameOver = true;

        _gameOverPanel.SetBool("IsGameOver", IsGameOver);
    }
}
