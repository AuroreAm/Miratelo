namespace Lyra
{
    [path ("script")]
    public class fail : task {
        protected override void _start() {
            fail ();
        }
    }
}