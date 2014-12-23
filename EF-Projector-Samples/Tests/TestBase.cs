using System;
using System.Diagnostics;

namespace EF_Projector_Samples.Tests
{
    public abstract class TestsBase
    {
        private Stopwatch _watch;

        protected void StartWatch()
        {
            if(_watch != null)
            {
                _watch.Stop();
            }

            _watch = Stopwatch.StartNew();
        }

        protected void StopWatchWriteTime(string label = null)
        {
            _watch.Stop();

            Console.WriteLine("{0}: {1}", label ?? "TimeElapsed", _watch.Elapsed);
        }
    }
}