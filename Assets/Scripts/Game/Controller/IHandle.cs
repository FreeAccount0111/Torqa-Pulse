namespace Game.Controller
{
    public interface IHandle
    {
        public void OnPointDownHandle();
        public void OnPointDragHandle();
        public void OnPointUpHandle();
    }
}
