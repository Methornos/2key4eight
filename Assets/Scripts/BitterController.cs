using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BitterController : MonoBehaviour
{
    [SerializeField]
    private Image _mouseInput;
    [SerializeField]
    private LineRenderer _track;

    private float _mouseStartPosition;

    private GameProcess _gameProcess;

    private RaycastHit _hit;

    public Transform CubeBitter;
    public float Force;

    private void Start()
    {
        _gameProcess = GameObject.FindWithTag("GameProcess").GetComponent<GameProcess>();

        UpdateBitter();
    }

    private void Update()
    {
        if (Physics.Raycast(_track.transform.position, Vector3.forward, out _hit, Mathf.Infinity))
        {
            if (_hit.transform.tag == "CubeNominal" ||
                _hit.transform.tag == "Wall")
            {
                _track.SetPosition(1, new Vector3(0, 0, _hit.distance));
                
            }
        }

        if(CubeBitter &&
            !GameProcess.IsGameOver)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                _mouseStartPosition = Input.mousePosition.x;
                _mouseInput.rectTransform.position = Input.mousePosition;
                _mouseInput.gameObject.SetActive(true);

                _track.transform.position = new Vector3(0, 1, -7);
                _track.gameObject.SetActive(true);
            }
            if(Input.GetKey(KeyCode.Mouse0))
            {
                float offset = (Input.mousePosition.x - _mouseStartPosition) / Screen.width;

                CubeBitter.position = new Vector3(Mathf.Clamp(CubeBitter.position.x + offset, -3.5f, 3.5f), 0.5f, -8);
                _track.transform.position = new Vector3(Mathf.Clamp(_track.transform.position.x + offset, -3.5f, 3.5f), 1, -7);
            }
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                CubeBitter.GetComponent<Rigidbody>().AddForce(Vector3.forward * Force);
                _mouseInput.gameObject.SetActive(false);
                _gameProcess.EndTurn();
                CubeBitter = null;

                _track.gameObject.SetActive(false);
            }
        }
    }

    public void UpdateBitter()
    {
        CubeBitter = GameObject.FindWithTag("CubeBitter").GetComponent<Transform>();
    }
}
