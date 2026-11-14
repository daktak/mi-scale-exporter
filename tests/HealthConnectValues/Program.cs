using MiScaleExporter.Services;

static void Near(double actual, double expected)
{
    if (!double.IsFinite(actual) || Math.Abs(actual - expected) > 0.000001)
        throw new Exception($"Expected {expected}, received {actual}");
}

// Reproduce the plugin's Android Percentage(value / 100) mapping. The value
// stored in Health Connect must still be 34.3%, rather than 0.343%.
Near(AndroidHealthConnectValues.BodyFatForPlugin(34.3) / 100, 34.3);
Near(AndroidHealthConnectValues.BodyFatForPlugin(100) / 100, 100);

var watts = AndroidHealthConnectValues.BasalMetabolicRateWatts(2201);
Near(watts, 106.585462962963);
Near(watts * 86400 / 4184, 2201);

foreach (var invalid in new[] { 0d, -1d, double.NaN, double.PositiveInfinity })
{
    Near(AndroidHealthConnectValues.BodyFatForPlugin(invalid), 0);
    Near(AndroidHealthConnectValues.BasalMetabolicRateWatts(invalid), 0);
}
Near(AndroidHealthConnectValues.BodyFatForPlugin(101), 0);
Console.WriteLine("Health Connect unit regression checks passed.");
