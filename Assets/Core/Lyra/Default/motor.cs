namespace Lyra
{
    public abstract class motor : aria.act
    {
        public abstract int priority {get;}
        public virtual bool accept2nd {get;} = false;
    }

    public interface ILucid
    {
        /// <summary>
        /// called when the kinesis is stopped, only called when the handler is on
        /// </summary>
        /// <param name="m"></param>
        public void inhalt(motor m);
        public bool on { get; }
    }
}