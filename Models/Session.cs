namespace erecruiter
{
    public static class Session 
    {
        public static bool isLogged { get; set; }
        public static int CanEdit { get; set;}
        public static int CanConfig { get; set; }
        public static string UserId { get; set; }
        public static string FullName { get; set; }
        public static string Login { get; set; }
        public static string Password { get; set; }
    }
}
