using ChessScripts3D.Managers;

public class DeleteMe : SingleTon<DeleteMe>
{
    public bool a;
    private void Update()
    {
        if (a == false) return;
        gameObject.SetActive(true);
    }
}
