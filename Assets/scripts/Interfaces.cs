

    public interface IState
    {
    void Enter();
    void Execute();
    void Exit();
    void SpaceHolded();
    void SpaceUp();
    void ABtnPressed();
    void DBtnPressed();
    void wBtnPressed();

    }

public enum dashDir { up, left, right}