using MySql.Data.MySqlClient;

namespace WebApiCoreWithoutORM.Data
{
    public interface IMySQLConnect: IConnect<MySqlConnection, MySqlCommand>
    {
    }
}
