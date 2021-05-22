

    public interface IState
    {
    void Enter();
    void Execute();
    void Exit();
    }

public enum direction { up, down, left, right, ul, ur, dl, dr}