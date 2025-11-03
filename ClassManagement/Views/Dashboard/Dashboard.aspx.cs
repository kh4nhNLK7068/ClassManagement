using System;
using System.Collections.Generic;

public partial class Dashboard : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindPieChart();
        }
    }

    private void BindPieChart()
    {
        // Browser statistics
        var browserUsage = new List<BrowserUsage>
        {
            new BrowserUsage { Browser = "Chrome", Value = 64.67, Explode = false },
            new BrowserUsage { Browser = "Internet Explorer", Value = 0.61, Explode = false },
            new BrowserUsage { Browser = "Safari", Value = 19.06, Explode = true },
            new BrowserUsage { Browser = "Firefox", Value = 3.66, Explode = false },
            new BrowserUsage { Browser = "Opera", Value = 2.36, Explode = false },
            new BrowserUsage { Browser = "Samsung Internet", Value = 2.81, Explode = false },
            new BrowserUsage { Browser = "Edge", Value = 3.11, Explode = false },
            new BrowserUsage { Browser = "Others", Value = 3.34, Explode = false }
        };

        PieChartBrowserUsage.DataSource = browserUsage;
        PieChartBrowserUsage.DataBind();
    }
}
public class BrowserUsage
{
    public string Browser { get; set; }
    public double Value { get; set; }
    public bool Explode { get; set; }
}