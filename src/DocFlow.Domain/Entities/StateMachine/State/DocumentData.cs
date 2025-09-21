using DocFlow.Domain.Values;

namespace DocFlow.Domain.Entities.StateMachine.State;

public class DocumentData<TData> : Document where TData : class
{
    public TData? Data { get; private set; }
    public Result<TData, Exception> GetDocumentData() 
        => Data != null
        ? Result<TData, Exception>.Success(Data) 
        : Result<TData, Exception>.Failure(new InvalidDataException());
    public void SetDateOnce(TData data)
    {
        ArgumentNullException.ThrowIfNull(data);
        if (Data != null) throw new InvalidOperationException("already initalized");
        Data = data;
    }
}