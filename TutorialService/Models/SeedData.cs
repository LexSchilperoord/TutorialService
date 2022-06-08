using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TutorialService.Data;
using System;
using System.Linq;

namespace TutorialService.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new TutorialServiceContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<TutorialServiceContext>>());
            // Look for any examples.
            if (context.Example.Any())
            {
                return;  // DB has been seeded
            }

            context.Example.AddRange(
                new Example
                {
                    Name = "My first database example",
                    Creation = DateTime.Parse("2022-6-1"),
                    Creator = "Lex",
                    Rating = "M"
                },
                new Example
                {
                    Name = "My second database example",
                    Creation = DateTime.Parse("2022-6-1"),
                    Creator = "Lex",
                    Rating = "R"
                },
                new Example
                {
                    Name = "Not my first database example",
                    Creation = DateTime.Parse("2022-6-1"),
                    Creator = "NotLex",
                    Rating = "M"
                }
            );
            context.SaveChanges();

            context.Group.AddRange(
                new Group
                {
                    Name = "Cool Examples",
                    Color = "Red"
                },
                new Group
                {
                    Name = "Lame Examples",
                    Color = "Blue"
                }
            );
            context.SaveChanges();

            context.Enrollment.AddRange(
                new Enrollment
                {
                    GroupID = 1,
                    ExampleID = 1,
                    Grade = Grade.A
                },
                new Enrollment
                {
                    GroupID = 2,
                    ExampleID = 3,
                    Grade = Grade.F
                },
                new Enrollment
                {
                    GroupID = 1,
                    ExampleID = 2,
                    Grade = Grade.B
                }
            );
            context.SaveChanges();
        }
    }
}
