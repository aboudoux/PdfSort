namespace SortExpenses.Tests.Tools
{
    public static class Expenses
    {
        public static string Folder => "..\\..\\NDF\\";
        public static string Pdf(int number) => $"{Folder}{number}.pdf";
    }
}
