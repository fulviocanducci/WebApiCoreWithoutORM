using MySql.Data.MySqlClient;
using WebApiCoreWithoutORM.Models;

namespace WebApiCoreWithoutORM.Data
{
    public interface IDalPeople: IDal<People, MySqlCommand> { }
}
