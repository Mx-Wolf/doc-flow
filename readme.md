# A few hints

```terminal
dotnet ef migrations add DocumentDataType --context DocFlowDbContext -p ./src/DocFlow.Infrastructure -s ./src/DocFlow.Host
```

```terminal
dotnet ef database update --context DocFlowDbContext -p ./src/DocFlow.Infrastructure -s ./src/DocFlow.Host
```
