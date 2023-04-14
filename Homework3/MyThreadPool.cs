using System.Collections.Concurrent;

namespace Homework3;

public class MyThreadPool
{
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    private readonly ConcurrentQueue<Action> _tasksQueue = new();
    private readonly List<Thread> _threads = new();

    public MyThreadPool(int threadNumber)
    {
        for (var i = 0; i < threadNumber; i++)
            _threads.Add(new Thread(() =>
            {
                while (_tasksQueue.IsEmpty && !_cancellationTokenSource.IsCancellationRequested)
                {
                }

                if (!_cancellationTokenSource.IsCancellationRequested && _tasksQueue.TryDequeue(out var action))
                    action.Invoke();
            }));

        foreach (var thread in _threads) thread.Start();
    }

    public IMyTask<TResult> Submit<TResult>(Func<TResult> func)
    {
        var myTask = new MyTask<TResult>(this, func);
        _tasksQueue.Enqueue(() => { myTask.Compute(); });
        return myTask;
    }

    public void Shutdown()
    {
        _cancellationTokenSource.Cancel();
        foreach (var thread in _threads) thread.Join();
    }

    private class MyTask<TResult> : IMyTask<TResult>
    {
        private readonly Func<TResult> _func;
        private readonly object _locker = new();
        private readonly MyThreadPool _threadPool;
        private TResult _result;

        public MyTask(MyThreadPool threadPool, Func<TResult> func)
        {
            _func = func;
            _threadPool = threadPool;
        }

        public bool IsCompleted { get; set; }

        public TResult Result
        {
            get
            {
                Compute();
                return _result;
            }
            set { }
        }

        public IMyTask<TNewResult> ContinueWith<TNewResult>(Func<TResult, TNewResult> func)
        {
            lock (_locker)
            {
                return _threadPool.Submit(() => func(_func()));
            }
        }

        public void Compute()
        {
            if (IsCompleted) return;
            lock (_locker)
            {
                try
                {
                    _result = _func.Invoke();
                }
                catch (Exception e)
                {
                    throw new AggregateException(e.Message);
                }
            }
        }
    }
}