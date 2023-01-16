using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartState : State
{
    private StartView _startView;

    public override void OnEnter()
    {
        base.OnEnter();
        var op = SceneManager.LoadSceneAsync("Start", LoadSceneMode.Additive);
        op.completed += InitializeScene;
    }

    public override void OnExit()
    {
        base.OnExit();

        if (_startView != null)
            _startView.PlayClicked -= OnPlayClicked;

        SceneManager.UnloadSceneAsync("Start");
    }

    private void InitializeScene(AsyncOperation obj)
    {
        _startView = GameObject.FindObjectOfType<StartView>();
        if (_startView != null)
            _startView.PlayClicked += OnPlayClicked;
    }

    private void OnPlayClicked(object sender, EventArgs e)
    {
        StateMachine.Push(States.Playing);
        _startView.gameObject.SetActive(false);
    }
}
