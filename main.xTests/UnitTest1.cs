using Xunit;

namespace main.xTests;

public class UnitTest1
{
    [Fact]
    public void DateWeightAsDummyTest()
    {
        var dateWeight = SimpleWeightManager.ClassMappings.DateWeight.CreateAsDummy();

        Assert.Equal( 0, dateWeight.Year );
        Assert.Equal( 0, dateWeight.Month );
        Assert.Equal( 0, dateWeight.Day );
        Assert.Equal( "0.0", dateWeight.Height );
        Assert.Equal( "0.0", dateWeight.Weight );
        Assert.Equal( "0.0", dateWeight.BodyFatPercentage );
        Assert.Equal( "0.0", dateWeight.Weight2Aim );
        Assert.Equal( "---", dateWeight.ToDateString( SimpleWeightManager.ClassMappings.DateWeightDateType.ForGraph ) );
        Assert.Equal( "---", dateWeight.ToDateString( SimpleWeightManager.ClassMappings.DateWeightDateType.ForDataCard ) );
        Assert.Equal( "---", dateWeight.ToHeightString() );
        Assert.Equal( "0.0%", dateWeight.ToBodyFatPercentageString() );
        Assert.Equal( "おめでとうございます！ 目標達成です！", dateWeight.DifferenceFromGoal() );
    }

    [Fact]
    public void DateWeightCreationTest()
    {
        var dateWeight = SimpleWeightManager.ClassMappings.DateWeight.Create( new System.DateTime( 2023, 2, 4 ), "170.3", "70", "0.0", "60" );

        Assert.Equal( 2023, dateWeight.Year );
        Assert.Equal( 2, dateWeight.Month );
        Assert.Equal( 4, dateWeight.Day );
        Assert.Equal( "170.3", dateWeight.Height );
        Assert.Equal( "70", dateWeight.Weight );
        Assert.Equal( "0.0", dateWeight.BodyFatPercentage );
        Assert.Equal( "60", dateWeight.Weight2Aim );
        Assert.Equal( "2/4", dateWeight.ToDateString( SimpleWeightManager.ClassMappings.DateWeightDateType.ForGraph ) );
        Assert.Equal( "2023/2/4 土曜日", dateWeight.ToDateString( SimpleWeightManager.ClassMappings.DateWeightDateType.ForDataCard ) );
        Assert.Equal( "170.3cm", dateWeight.ToHeightString() );
        Assert.Equal( "0.0%", dateWeight.ToBodyFatPercentageString() );
        Assert.Equal( "目標まであと -10kg！", dateWeight.DifferenceFromGoal() );

        string bmi = SimpleWeightManager.ClassMappings.DateWeight.CalcBMI( dateWeight );
        Assert.Equal( "24.14", bmi );

        string bestWeight = SimpleWeightManager.ClassMappings.DateWeight.CalcBestWeight( dateWeight );
        Assert.Equal( "63.80kg", bestWeight );
    }

    [Fact]
    public void DateWeightComparisonTest()
    {
        var d1 = SimpleWeightManager.ClassMappings.DateWeight.Create( new System.DateTime( 2023, 2, 4 ), "170.3", "70", "0.0", "60" );
        var d2 = SimpleWeightManager.ClassMappings.DateWeight.Create( new System.DateTime( 2023, 2, 4 ), "170.3", "72", "0.0", "60" );

        var d3 = SimpleWeightManager.ClassMappings.DateWeight.Create( new System.DateTime( 2022, 2, 4 ), "170.3", "70", "0.0", "60" );
        var d4 = SimpleWeightManager.ClassMappings.DateWeight.Create( new System.DateTime( 2024, 2, 4 ), "170.3", "70", "0.0", "60" );

        var d5 = SimpleWeightManager.ClassMappings.DateWeight.Create( new System.DateTime( 2023, 1, 4 ), "170.3", "70", "0.0", "60" );
        var d6 = SimpleWeightManager.ClassMappings.DateWeight.Create( new System.DateTime( 2023, 3, 4 ), "170.3", "70", "0.0", "60" );

        var d7 = SimpleWeightManager.ClassMappings.DateWeight.Create( new System.DateTime( 2023, 2, 3 ), "170.3", "70", "0.0", "60" );
        var d8 = SimpleWeightManager.ClassMappings.DateWeight.Create( new System.DateTime( 2023, 2, 5 ), "170.3", "70", "0.0", "60" );

        // x1 == x2 ->  0
        // x1 >  x2 ->  1
        // x1 <  x2 -> -1

        Assert.Equal( d1, d2 );
        Assert.Equal( 0, d1.CompareTo( d2 ) );

        Assert.Equal( 1, d1.CompareTo( d3 ) );
        Assert.Equal( -1, d1.CompareTo( d4 ) );

        Assert.Equal( 1, d1.CompareTo( d5 ) );
        Assert.Equal( -1, d1.CompareTo( d6 ) );

        Assert.Equal( 1, d1.CompareTo( d7 ) );
        Assert.Equal( -1, d1.CompareTo( d8 ) );
    }
}