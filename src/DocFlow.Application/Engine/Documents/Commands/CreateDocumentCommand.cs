namespace DocFlow.Application.Engine.Documents.Commands;
public record CreateDocumentCommand(
    int FormularId, 
    int UnitMapId, 
    IReadOnlyDictionary<string,object> Properties);