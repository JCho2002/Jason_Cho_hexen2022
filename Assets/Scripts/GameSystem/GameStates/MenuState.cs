using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuState : State
{
    private MenuView _menuView;

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("Enter the MenuState");
        var op = SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Additive);
        op.completed += InitializeScene;
    }

    public override void OnExit()
    {
        base.OnExit();

        if (_menuView != null)
            _menuView.PlayClicked -= OnPlayClicked;

        SceneManager.UnloadSceneAsync("Menu");
    }

    private void InitializeScene(AsyncOperation obj)
    {
        _menuView = GameObject.FindObjectOfType<MenuView>();
        if (_menuView != null)
            _menuView.PlayClicked += OnPlayClicked;
    }

    private void OnPlayClicked(object sender, EventArgs e)
    {
        StateMachine.MoveTo(States.Playing);
    }
}
