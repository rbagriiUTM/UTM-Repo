// <auto-generated />
namespace MolDavaBanking.Repositories.Migrations
{
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    [GeneratedCode("EntityFramework.Migrations", "6.2.0-61023")]
    public sealed partial class ChangeCardsTableStructure_AddRelationshipBetweenCardsAndUsers : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(ChangeCardsTableStructure_AddRelationshipBetweenCardsAndUsers));
        
        string IMigrationMetadata.Id
        {
            get { return "201905081125032_ChangeCardsTableStructure_AddRelationshipBetweenCardsAndUsers"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}
