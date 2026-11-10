using Moq;
using PSCleanArchitectureGC.Application.Contracts.Persistence;
using PSCleanArchitectureGC.Domain.Entities;

namespace PSCleanArchitectureGC.UnitTests.Mocks
{
    public class CategoryRepositoryMocks
    {
        public static Mock<ICategoryRepository> GetCategoryRepository()
        {
            var concertGuid = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}");
            var musicalGuid = Guid.Parse("{6313179F-7837-473A-A4D5-A5571B43E6A6}");
            var playGuid = Guid.Parse("{BF3F3002-7E53-441E-8B76-F6280BE284AA}");
            var conferenceGuid = Guid.Parse("{FE98F549-E790-4E9F-AA16-18C2292A2EE9}");

            var categories = new List<Category>
            {
                new Category
                {
                    CategoryId = concertGuid,
                    Name = "Concerts",
                    Events =
                    [
                        new Event
                        {
                            EventId = Guid.Parse("{3448D5A4-0F72-4DD7-BF15-C14A46B26C00}"),
                            Name = "Pink",
                            Price = 85,
                            Artist = "Pink",
                            Date = new DateTime(2026, 11, 08),
                            CategoryId = concertGuid
                        }
                    ]
                },
                new Category
                {
                    CategoryId = musicalGuid,
                    Name = "Musicals",
                    Events =
                    [
                        new Event
                        {
                            EventId = Guid.Parse("{EE272F8B-6096-4CB6-8625-BB4BB2D89E8B}"),
                            Name = "Waitress",
                            Price = 65,
                            Artist = "Sara Bareilles",
                            Date = new DateTime(2025, 08, 15),
                            Description = "An expert pie-maker who dreams of a way out of her small town",
                            ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/GloboTicket/banjo.jpg",
                            CategoryId = musicalGuid
                        }
                    ]
                },
                new Category
                {
                    CategoryId = conferenceGuid,
                    Name = "Conferences"
                },
                 new Category
                {
                    CategoryId = playGuid,
                    Name = "Plays"
                }
            };

            Mock<ICategoryRepository> mockCategoryRepository = new ();

            mockCategoryRepository.Setup(r => r.AddAsync(It.IsAny<Category>())).ReturnsAsync(
                (Category category) =>
                {
                    categories.Add(category);
                    return category;
                });

            mockCategoryRepository.Setup(r => r.ListAllAsync()).ReturnsAsync(categories);
            
            mockCategoryRepository.Setup(r => r.GetCategoriesWithEvents(It.IsAny<bool>()))
                .ReturnsAsync((bool includePassedEvents) =>
                {
                    var result = categories.Select(c => new Category
                    {
                        CategoryId = c.CategoryId,
                        Name = c.Name,
                        Events = includePassedEvents 
                            ? c.Events 
                            : c.Events?.Where(e => e.Date >= new DateTime(2026, 11, 08)).ToList()
                    }).ToList();
                    return result;
                });

            return mockCategoryRepository;
        }
    }
}
