using System.Collections.Generic;
using System.Linq;
using GameKit.Localization.Internal;

namespace GameKit.ScreenResolution
{
    public sealed record ScreenResolution
    {
        public static readonly ScreenResolution FullScreen = new(nameof(FullScreen));
        public static readonly ScreenResolution SR_1920x1080 = new(nameof(SR_1920x1080), 1920, 1080);
        public static readonly ScreenResolution SR_1600x900 = new(nameof(SR_1600x900), 1600, 900);
        public static readonly ScreenResolution SR_1280x720 = new(nameof(SR_1280x720), 1280, 720);
        
        public static readonly IReadOnlyList<ScreenResolution> Values = new[]
        {
            FullScreen,
            SR_1920x1080,
            SR_1600x900,
            SR_1280x720,
        };
        
        public string Identifier { get; }
        public int? Width { get; }
        public int? Height { get; }
        public bool IsFullScreen { get; }
        

        ScreenResolution(string identifier, int width, int height)
        {
            Identifier = identifier;
            Width = width;
            Height = height;
            IsFullScreen = false;
        }

        ScreenResolution(string identifier)
        {
            Identifier = identifier;
            Width = null;
            Height = null;
            IsFullScreen = true;
        }
        
        public static ScreenResolution FromIdentifier(string identifier)
        {
            return Values.First(x => x.Identifier == identifier);
        }

        public override string ToString()
        {
            if (IsFullScreen)
            {
                return LocalizedStringConstants.GameKit.ScreenResolution.FullScreen.GetValue();
            }
            return $"{Width} x {Height}";
        }
    }
}