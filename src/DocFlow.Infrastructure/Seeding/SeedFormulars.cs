using DocFlow.Domain.Entities;
using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Values;

using Microsoft.EntityFrameworkCore;

namespace DocFlow.Infrastructure.Seeding;
internal static class SeedFormulars
{
    public static async Task SeedAsync(DbContext context, bool storeManaged, CancellationToken cancellationToken)
    {
        var formulars = context.Set<Formular>();
        if (!formulars.Any())
        {
            formulars.AddRange(new List<Formular>
            {
                new()
                {
                    Id = 1001,
                    DocumentData = typeof(IncidentReport),
                    Presentable = new Presentable
                    {
                        Code = "Incident",
                        Color = "Error",
                        Name = "Incident Report",
                        SequenceNumber = 100,
                        IsEnabled = true,
                    }
                },
                new()
                {
                   Id = 1101,
                   DocumentData = typeof(TranslationRequest),
                   Presentable = new Presentable
                   {
                       Code = "Translation",
                       Color = "Primary",
                       Name = "Translation Request",
                       SequenceNumber = 200,
                       IsEnabled = true,
                   }
                },
                new()
                {
                    Id = 1102,
                    DocumentData = typeof(BugFixRequest),
                    Presentable = new Presentable
                    {
                        Code = "Bug Fix",
                        Color = "Primary",
                        Name = "Bug Fix Request",
                        SequenceNumber = 210,
                        IsEnabled = true,
                    }
                }
            });
        }

        await Task.CompletedTask;
    }
}
