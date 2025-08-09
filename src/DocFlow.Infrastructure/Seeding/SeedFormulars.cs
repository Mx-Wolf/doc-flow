using DocFlow.Domain.Entities;
using DocFlow.Domain.Entities.StateMachine.Flow;
using DocFlow.Domain.Values;

using Microsoft.EntityFrameworkCore;

namespace DocFlow.Infrastructure.Seeding;
internal static class SeedFormulars
{
    public static async Task SeedAsync(DbContext context, bool _, CancellationToken cancellationToken)
    {
        var formulars = context.Set<Formular>();
        if (!(await formulars.AnyAsync(cancellationToken)))
        {
            await formulars.AddRangeAsync((List<Formular>)
            [
                new Formular
                {
                    Id = new FormularId(1001),
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

                new Formular
                {
                    Id = new FormularId(1101),
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

                new Formular
                {
                    Id = new FormularId(1102),
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
            ], cancellationToken);
        }

        await Task.CompletedTask;
    }
}
