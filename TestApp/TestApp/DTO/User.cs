using SQLite;

namespace TestApp.DTO
{
    class User
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Username { get; set; }
        public byte[] Image { get; set; }
        public string Gender { get; set; }

    }
}
