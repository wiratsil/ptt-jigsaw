using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JigsawManager : Singleton<JigsawManager>
{
    public GameController gameController;
    public List<PuzzleController> puzzlePrefabs;
    public Image refPuzzle;
    public JigsawTimer jigsawTimer;
    public JigsawResult jigsawResult;
    [Space]
    [SerializeField]
    private float timer;
    [SerializeField]
    private Collider2D bounds;

    private PuzzleController puzzle;
    private bool finish = false;

    // Start is called before the first frame update
    void Start()
    {
        _StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (pos.x <= bounds.bounds.min.x || pos.x >= bounds.bounds.max.x ||
            pos.y <= bounds.bounds.min.y || pos.y >= bounds.bounds.max.y)
        {
            gameController.moving = false;
        }
        else
        {
            gameController.moving = true;
        }

        if (jigsawTimer.timeOut && !finish)
        {
            finish = true;
            Result();
        }
        else if (puzzle != null && puzzle.IsAssembled() && !finish)
        {
            finish = true;
            Result();
        }

    }

    public void _StartGame ()
    {
        int index = Random.RandomRange(0, puzzlePrefabs.Count);
        puzzle = Instantiate(puzzlePrefabs[index]);
        gameController.puzzle = puzzle;
        refPuzzle.sprite = puzzle.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        gameController.enabled = true;

        jigsawTimer.StartTimer(timer);
        jigsawTimer.enabled = true;
    }

    public void Result()
    {
        jigsawResult.ShowResult(puzzle.sucessPiece * 3);
        API_AddScore.Instance.AddScore(puzzle.sucessPiece * 3);
    }
}
