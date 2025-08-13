namespace DocFlow.Domain.Entities.StateMachine.State;

public class DocumentData<TData>:Document where TData : class
{
  public required TData Data { get; set; }
}