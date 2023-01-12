using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using DinkToPdf.Contracts;

namespace DinkToPdf
{
    public class SynchronizedConverter : BasicConverter
    {
        Thread _conversionThread;

        BlockingCollection<Task> conversions = new BlockingCollection<Task>();

        bool _kill;

        private readonly object _startLock = new object();

        public SynchronizedConverter(ITools tools) : base(tools)
        {

        }

        public override byte[] Convert(IDocument document)
        {
            return Invoke(() => base.Convert(document));
        }

        public TResult Invoke<TResult>(Func<TResult> @delegate)
        {
            StartThread();

            Task<TResult> task = new Task<TResult>(@delegate);

            lock (task)
            {
                //add task to blocking collection
                conversions.Add(task);

                //wait for task to be processed by conversion thread 
                Monitor.Wait(task);
            }

            //throw exception that happened during conversion
            if (task.Exception != null)
            {
                throw task.Exception;
            }

            return task.Result;
        }

        private void StartThread()
        {
            lock (_startLock)
            {
                if (_conversionThread == null)
                {
                    _conversionThread = new Thread(Run)
                    {
                        IsBackground = true,
                        Name = "wkhtmltopdf worker thread"
                    };

                    _kill = false;

                    _conversionThread.Start();
                }
            }
        }
        
        // ReSharper disable once UnusedMember.Local
        private void StopThread()
        {
            lock (_startLock)
            {
                if (_conversionThread != null)
                {
                    _kill = true;

                    while (_conversionThread.ThreadState == ThreadState.Stopped)
                    { }

                    _conversionThread = null;
                }
            }
        }

        private void Run()
        {
            while (!_kill)
            {
                //get next conversion taks from blocking collection
                Task task = conversions.Take();

                lock (task)
                {
                    //run taks on thread that called RunSynchronously method
                    task.RunSynchronously();

                    //notify caller thread that task is completed
                    Monitor.Pulse(task);
                }
            }
        }
    }
}
