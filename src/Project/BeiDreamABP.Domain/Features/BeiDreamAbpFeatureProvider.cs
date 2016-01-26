using Abp.Application.Features;
using Abp.Localization;
using Abp.Runtime.Validation;
using Abp.UI.Inputs;

namespace BeiDreamAbp.Domain.Features
{
    public class BeiDreamAbpFeatureProvider : FeatureProvider
    {
        public override void SetFeatures(IFeatureDefinitionContext context)
        {
            var sampleBooleanFeature = context.Create(
                FeatureNames.ExcelInputTasksBooleanFeature,
                defaultValue: "false",
                displayName: L("Is allow Excel Input Tasks"),
                inputType: new CheckboxInputType()
            );
            sampleBooleanFeature.CreateChildFeature(
                FeatureNames.ExcelInputTasksNumericFeature,
                defaultValue: "10",
                displayName: L("allow Excel Input Tasks Amount"),
                inputType: new SingleLineStringInputType(new NumericValueValidator(1, 1000000))
            );
            context.Create(
                FeatureNames.CreateUsersNumericFeature,
                defaultValue: "50",
                displayName: L("allow Create Users Amount"),
                inputType: new SingleLineStringInputType(new NumericValueValidator(1, 1000000))
            );
        }
        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, BeiDreamAbpConsts.LocalizationSourceName);
        }
    }
}