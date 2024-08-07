using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using TodoList.Api.Controllers;
using Xunit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;
using ArchUnitNET.xUnit;
using TodoList.Api.Services;
using Microsoft.EntityFrameworkCore;
using TodoList.Api.Repositories;

namespace TodoList.Api.UnitTests.Fitness
{
    public class ArchitectureTests
    {
        private static readonly Architecture Architecture = new ArchLoader().LoadAssemblies(typeof(Startup).Assembly).Build();

        [Fact]
        public void Controllers_Should_Only_Inject_IServices()
        {
            var controllers = Classes().That().AreAssignableTo(typeof(BaseController<>)).As("Controllers");
            var services = Interfaces().That().AreAssignableTo(typeof(IService<>)).As("Services");
            var dbContexts = Classes().That().AreAssignableTo(typeof(DbContext)).As("DbContexts");
            var repositories = Interfaces().That().AreAssignableTo(typeof(IRepository<>)).As("Repositories");

            var rule = Classes().That().Are(controllers)
                .Should().DependOnAny(services)
                .AndShould().NotDependOnAny(dbContexts).AndShould().NotDependOnAny(typeof(DbContext))
                .AndShould().NotDependOnAny(repositories);

            rule.Check(Architecture);
        }


    }
}