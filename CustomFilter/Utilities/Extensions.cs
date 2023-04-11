namespace CustomFilter.Utilities
{
    public static class Extensions
    {
        public static string MyTrim(this String myString)
        {
            //TODO : regex ile düzenle!!
            return myString.Replace(" ", "");
        }
    }
}
