namespace Assets.BL
{
    public static class IdGenerator
    {
        private static int _id = 1;

        public static int GetId()
        {
            var value = _id;
            _id++;
            return value;
        }
    }
}