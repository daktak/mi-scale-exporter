namespace MiScaleExporter.Services;

// Adapter for the Android writer in the pinned Plugin.Maui.Health 1.5.0.
// Recheck these workarounds before upgrading that dependency:
// https://github.com/0xc3u/Plugin.Maui.Health/blob/main/src/Plugin.Maui.Health/Health.android.cs
internal static class AndroidHealthConnectValues
{
    public static double BodyFatForPlugin(double percent)
    {
        if (!double.IsFinite(percent) || percent <= 0 || percent > 100)
            return 0;

        // The plugin constructs Percentage(value / 100), but Health Connect's
        // Percentage constructor takes percentage points, not a fraction.
        return percent * 100;
    }

    public static double BasalMetabolicRateWatts(double kilocaloriesPerDay)
    {
        if (!double.IsFinite(kilocaloriesPerDay) || kilocaloriesPerDay <= 0)
            return 0;

        // The Android writer ignores the unit string and constructs Power.Watts.
        return kilocaloriesPerDay * 4184.0 / 86400.0;
    }
}
