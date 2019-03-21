using System;
namespace WebApiCoreWithoutORM.Models
{
    public class People
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Active { get; set; }
    }
}
