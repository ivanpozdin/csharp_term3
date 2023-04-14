namespace Homework3;

public interface IMyTask<TResult>
{
    bool IsCompleted { get; set; }
    TResult Result { get; set; }
    IMyTask<TNewResult> ContinueWith<TNewResult>(Func<TResult, TNewResult> func);
}