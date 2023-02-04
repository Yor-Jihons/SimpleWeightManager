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

    [Fact]
    public void DateWeightInfoTest()
    {
        string filepath = @"..\..\..\weights.xml";
        var dateWeightInfo = SimpleWeightManager.ClassMappings.DateWeightInfo.Load( filepath );

        Assert.Equal( 0, dateWeightInfo.IsBodyFatPercentageShowed );

        Assert.Equal( 5, dateWeightInfo.DateWeights.Count );
        Assert.Equal( 2022, dateWeightInfo.DateWeights[1].Year );
        Assert.Equal( 12, dateWeightInfo.DateWeights[1].Month );
        Assert.Equal( 8, dateWeightInfo.DateWeights[1].Day );

        Assert.Equal( "170.3", dateWeightInfo.DateWeights[2].Height );
        Assert.Equal( "74.5", dateWeightInfo.DateWeights[2].Weight );
        Assert.Equal( "64", dateWeightInfo.DateWeights[2].Weight2Aim );
        Assert.Equal( "0", dateWeightInfo.DateWeights[2].BodyFatPercentage );
    }

    [Fact]
    public void DateWeightManagerTest()
    {
        string filepath = @"..\..\..\weights.xml";
        var dateWeightInfo = SimpleWeightManager.ClassMappings.DateWeightManager.Create( filepath );

        Assert.False( dateWeightInfo.IsEmpty() );

        var date1 = dateWeightInfo.FetchLatestWeight();
        var date2 = SimpleWeightManager.ClassMappings.DateWeight.Create( new System.DateTime( 2022, 12, 12 ), "170.3", "73.0", "64", "0" );
        Assert.Equal( date2, date1 );

        var date3 = dateWeightInfo.FetchPrevWeight();
        var date4 = SimpleWeightManager.ClassMappings.DateWeight.Create( new System.DateTime( 2022, 12, 11 ), "170.3", "73.4", "64", "0" );
        Assert.Equal( date4, date3 );

        // 存在しない場合に追加する
        var date5 = SimpleWeightManager.ClassMappings.DateWeight.Create( new System.DateTime( 2022, 12, 17 ), "163.3", "73.4", "64", "54" );
        int pos_date5      = -1;
        bool existed_date5 = dateWeightInfo.Has( date5, out pos_date5 );
        Assert.False( existed_date5 );

        dateWeightInfo.Add( date5 );

        var date6 = dateWeightInfo.FetchLatestWeight();
        Assert.Equal( date5, date6 );

        // 存在している場合は変更する
        var date7 = SimpleWeightManager.ClassMappings.DateWeight.Create( new System.DateTime( 2022, 12, 7 ), "170.3", "73.4", "64", "0" );
        int pos_date7      = -1;
        bool existed_date7 = dateWeightInfo.Has( date6, out pos_date7 );
        Assert.True( existed_date7 );

        var date8 = dateWeightInfo.FetchLatestWeight();
        Assert.Equal( "2022/12/17 土曜日\n163.3cm\n73.4kg", date8.ToString() );

        var date9 = SimpleWeightManager.ClassMappings.DateWeight.Create( new System.DateTime( 2022, 12, 7 ), "180", "63", "64", "0" );
        Assert.True( dateWeightInfo.Edit( pos_date7, date8 ) );

        var date10 = dateWeightInfo.FetchLatestWeight();
        Assert.Equal( "2022/12/17 土曜日\n163.3cm\n73.4kg", date10.ToString() );

        Assert.Equal( "目標体重: 54kg", dateWeightInfo.ToAimString() );

        Assert.Equal( "目標まであと -19.400000000000006kg！", dateWeightInfo.ToMessageString() );

        dateWeightInfo.Clear();

        Assert.Equal( 0, dateWeightInfo.Count() );
    }

    [Fact]
    public void GraphElementTest()
    {
        string filepath = @"..\..\..\weights.xml";
        var dateWeightInfo = SimpleWeightManager.ClassMappings.DateWeightManager.Create( filepath );

        var graphElement = dateWeightInfo.Fetch();

        Assert.Equal( 73.0, graphElement.Weights[1] );
        Assert.Equal( 1, graphElement.Ticks[1] );
        Assert.Equal( "12/8", graphElement.Xticks[1] );
    }
}