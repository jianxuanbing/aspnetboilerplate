using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Domain.Repositories;

namespace BeiDreamAbp.Domain.Editions
{
    public class EditionManager : AbpEditionManager
    {
        public const string DefaultEditionName = "Standard";
        public const string DefaultEditionDisplayName = "DisplayStandard";

        public EditionManager(
            IRepository<Edition> editionRepository,
            IRepository<EditionFeatureSetting, long> editionFeatureRepository)
            : base(
                editionRepository,
                editionFeatureRepository
            )
        {
        }
    }
}