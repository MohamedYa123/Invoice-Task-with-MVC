namespace Invoice_Task.SiteClasses
{
    public static class Manager
    {
        public static Random random = new Random();
        public static int getrandomid()
        {
            var i = random.Next(0, int.MaxValue);
            return i;
        }
    }
}
