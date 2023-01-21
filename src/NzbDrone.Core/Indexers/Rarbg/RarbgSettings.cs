using System.Collections.Generic;
using FluentValidation;
using NzbDrone.Core.Annotations;
using NzbDrone.Core.Validation;

namespace NzbDrone.Core.Indexers.Rarbg
{
    public class RarbgSettingsValidator : AbstractValidator<RarbgSettings>
    {
        public RarbgSettingsValidator()
        {
            RuleFor(c => c.BaseUrl).ValidRootUrl();

            RuleFor(c => c.SeedCriteria).SetValidator(_ => new SeedCriteriaSettingsValidator());
        }
    }

    public class RarbgSettings : ITorrentIndexerSettings
    {
        private static readonly RarbgSettingsValidator Validator = new RarbgSettingsValidator();

        public RarbgSettings()
        {
            BaseUrl = "https://torrentapi.org";
            RankedOnly = false;
            MinimumSeeders = IndexerDefaults.MINIMUM_SEEDERS;
            Categories = new int[]
            {
                (int)RarbgCategories.XXX
            };
        }

        [FieldDefinition(0, Label = "API URL", HelpText = "URL to Rarbg api, not the website.")]
        public string BaseUrl { get; set; }

        [FieldDefinition(1, Type = FieldType.Checkbox, Label = "Ranked Only", HelpText = "Only include ranked results.")]
        public bool RankedOnly { get; set; }

        [FieldDefinition(2, Type = FieldType.Captcha, Label = "CAPTCHA Token", HelpText = "CAPTCHA Clearance token used to handle CloudFlare Anti-DDOS measures on shared-ip VPNs.")]
        public string CaptchaToken { get; set; }

        [FieldDefinition(3, Type = FieldType.Number, Label = "Minimum Seeders", HelpText = "Minimum number of seeders required.", Advanced = true)]
        public int MinimumSeeders { get; set; }

        [FieldDefinition(4)]
        public SeedCriteriaSettings SeedCriteria { get; set; } = new SeedCriteriaSettings();

        [FieldDefinition(5, Type = FieldType.Select, Label = "Categories", SelectOptions = typeof(RarbgCategories), HelpText = "Categories for use in search and feeds. If unspecified, all options are used.")]
        public IEnumerable<int> Categories { get; set; }

        public NzbDroneValidationResult Validate()
        {
            return new NzbDroneValidationResult(Validator.Validate(this));
        }
    }

    public enum RarbgCategories
    {
        [FieldOption]
        Movie_Xvid = 14,
        [FieldOption]
        Movie_Xvid_720p = 48,
        [FieldOption]
        Movie_x264 = 17,
        [FieldOption]
        Movie_x264_720p = 45,
        [FieldOption]
        Movie_x264_1080p = 44,
        [FieldOption]
        Movie_x264_4K = 50,
        [FieldOption]
        Movie_x264_3D = 47,
        [FieldOption]
        Movie_x265_1080p = 54,
        [FieldOption]
        Movie_x265_4K = 51,
        [FieldOption]
        Movie_x265_4K_HDR = 52,
        [FieldOption]
        Movie_BD_Remux = 46,
        [FieldOption]
        Movie_Full_BD = 42,
        [FieldOption]
        XXX = 4,
    }
}
