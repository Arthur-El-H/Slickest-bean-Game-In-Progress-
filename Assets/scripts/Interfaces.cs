

    public interface IState
    {
    void Enter();
    void Execute();
    void Exit();
    void SpaceHolded();
    void SpaceUp();
    void ABtnPressed();
    void DBtnPressed();
    void WBtnPressed();
    void OnTheGround();
    void OffTheGround();
    void OnTheWall(bool right);
    void OffTheWall();
    void CrashIntoBean();
}

public enum dashDir { up, left, right}