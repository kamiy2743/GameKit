using System.Collections.Generic;

namespace GameKit.ScreenResolution
{
    public sealed record ScreenResolution
    {
        public static readonly ScreenResolution FullScreen = new();
        public static readonly ScreenResolution SR_1920x1080 = new(1920, 1080);
        public static readonly ScreenResolution SR_1600x900 = new(1600, 900);
        public static readonly ScreenResolution SR_1280x720 = new(1280, 720);
        
        public static readonly IReadOnlyList<ScreenResolution> Values = new[]
        {
            FullScreen,
            SR_1920x1080,
            SR_1600x900,
            SR_1280x720,
        };
        
        public float? Width { get; }
        public float? Height { get; }
        public bool IsFullScreen { get; }

        ScreenResolution(float width, float height)
        {
            Width = width;
            Height = height;
            IsFullScreen = false;
        }

        ScreenResolution()
        {
            Width = null;
            Height = null;
            IsFullScreen = true;
        }

        public override string ToString()
        {
            if (IsFullScreen)
            {
                //TODO LocalizedStringConstantsから取得
                return "FullScreen";
            }
            return $"{Width} x {Height}";
        }
    }
}