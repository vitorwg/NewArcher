using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;

    private InputReader _input;

    private void Awake()
    {
        _pauseMenu.SetActive(false);
        //_input.GetComponent<InputReader>();
    }

    private void OnEnable()
    {
        _input.MenuPauseEvent += OnPause;
    }

    private void OnPause(bool arg0)
    {
        Debug.Log(arg0);
    }
}
