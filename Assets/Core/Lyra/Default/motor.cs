namespace Lyra
{
    public abstract class motor : sys.self
    {
        public abstract int Priority {get;}
        public virtual bool AcceptSecondState {get;} = false;
    }

    public interface IMotorHandler
    {
        /// <summary>
        /// called when the motor is stopped, only called when the handler is on
        /// </summary>
        /// <param name="m"></param>
        public void OnMotorEnd(motor m);
        public bool on { get; }
    }
}