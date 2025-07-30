
Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardWin
Imports System.Data

Public Class DashboardTEst
    Private Sub Dashboard_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim dashboard As New Dashboard()

        ' Add a Choropleth map item
        Dim choroplethMap As New ChoroplethMapDashboardItem()

        ' Define the dimension (e.g., RegionName from your DB and shapefile)
        Dim regionDimension As New Dimension("RegionName")
        choroplethMap.Area = ShapefileArea.Asia


        ' Set custom shapefile properties
        choroplethMap.Area = ShapefileArea.Custom
        choroplethMap.CustomShapefile.Url = "D:\india_line.shp"


        ' Bind the map to a data source
        Dim data As New DataTable()
        data.Columns.Add("RegionName", GetType(String))
        data.Columns.Add("Sales", GetType(Double))

        data.Rows.Add("North", 120000)
        data.Rows.Add("South", 90000)
        data.Rows.Add("East", 75000)
        data.Rows.Add("West", 103000)

        Dim ds As New DashboardObjectDataSource("Sales by Region")
        ds.DataSource = data

        dashboard.DataSources.Add(ds)
        choroplethMap.DataSource = ds

        ' Add the map to the dashboard
        dashboard.Items.Add(choroplethMap)

        ' Load the dashboard into the viewer
        DashboardViewer1.Dashboard = dashboard
    End Sub
End Class