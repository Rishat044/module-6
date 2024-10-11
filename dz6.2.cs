using System;

public interface IReportBuilder
{
    void SetHeader(string header);
    void SetContent(string content);
    void SetFooter(string footer);
    Report GetReport();
}

public class Report
{
    public string Header { get; set; }
    public string Content { get; set; }
    public string Footer { get; set; }

    // Метод для отображения отчета
    public void Display()
    {
        Console.WriteLine(Header);
        Console.WriteLine(Content);
        Console.WriteLine(Footer);
    }
}

public class TextReportBuilder : IReportBuilder
{
    private Report _report = new Report();

    public void SetHeader(string header)
    {
        _report.Header = $"*** {header} ***\n";
    }

    public void SetContent(string content)
    {
        _report.Content = $"{content}\n";
    }

    public void SetFooter(string footer)
    {
        _report.Footer = $"--- {footer} ---\n";
    }

    public Report GetReport()
    {
        return _report;
    }
}

public class HtmlReportBuilder : IReportBuilder
{
    private Report _report = new Report();

    public void SetHeader(string header)
    {
        _report.Header = $"<h1>{header}</h1>\n";
    }

    public void SetContent(string content)
    {
        _report.Content = $"<p>{content}</p>\n";
    }

    public void SetFooter(string footer)
    {
        _report.Footer = $"<footer>{footer}</footer>\n";
    }

    public Report GetReport()
    {
        return _report;
    }
}

public class ReportDirector
{
    public void ConstructReport(IReportBuilder builder, string header, string content, string footer)
    {
        builder.SetHeader(header);
        builder.SetContent(content);
        builder.SetFooter(footer);
    }
}






class Program
{
    static void Main(string[] args)
    {
        var director = new ReportDirector();


        IReportBuilder textBuilder = new TextReportBuilder();
        director.ConstructReport(textBuilder, "Отчет о продажах", "Продажи за месяц выросли на 10%", "Конец отчета");
        Report textReport = textBuilder.GetReport();
        Console.WriteLine("Текстовый отчет:");
        textReport.Display();



        IReportBuilder htmlBuilder = new HtmlReportBuilder();
        director.ConstructReport(htmlBuilder, "Отчет о продажах", "Продажи за этот месяц выросли на 10%", "Конец отчета");
        Report htmlReport = htmlBuilder.GetReport();
        Console.WriteLine("HTML отчет:");
        htmlReport.Display();
    }
}
