using MySql.Data.MySqlClient;
using System.Collections.Generic;
using WebApiCoreWithoutORM.Models;

namespace WebApiCoreWithoutORM.Data
{
    public sealed class DalPeople : IDalPeople
    {
        public IMySQLConnect Connect { get; }

        public DalPeople(IMySQLConnect connect)
        {
            Connect = connect;
        }       

        public void AddParameters(MySqlCommand command, People model)
        {            
            command.Parameters.Add("@name", MySqlDbType.String, 50, "name").Value = model.Name;
            command.Parameters.Add("@createdat", MySqlDbType.Date).Value = model.CreatedAt;
            command.Parameters.Add("@active", MySqlDbType.Bit).Value = model.Active;
            if (model.Id > 0)
            {
                command.Parameters.Add("@id", MySqlDbType.Int32).Value = model.Id;
            }
        }

        public People Add(People model)
        {
            using (MySqlCommand command = Connect.CreateCommand())
            {                
                command.CommandText = "INSERT INTO `people`(`name`,`createdat`,`active`) VALUES(@name,@createdat,@active)";
                AddParameters(command, model);
                if (command.ExecuteNonQuery() > 0)
                {
                    model.Id = (int)command.LastInsertedId;
                }
            }
            return model;
        }

        public bool Edit(People model)
        {
            using (MySqlCommand command = Connect.CreateCommand())
            {
                command.CommandText = "UPDATE `people` SET `name`=@name,`createdat`=@createdat,`active`=@active WHERE `id`=@id";
                AddParameters(command, model);
                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool Remove(People model)
        {
            return Remove(model.Id);
        }

        public bool Remove(object id)
        {
            using (MySqlCommand command = Connect.CreateCommand())
            {
                command.CommandText = "DELETE FROM `people` WHERE `id`=@id";
                command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                return command.ExecuteNonQuery() > 0;
            }
        }

        public IEnumerable<People> All()
        {
            using (MySqlCommand command = Connect.CreateCommand())
            {
                command.CommandText = "SELECT `id`,`name`,`createdat`, `active` FROM `people`";
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            yield return new People
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                CreatedAt = reader.GetDateTime(2),
                                Active = reader.GetBoolean(3)
                            };
                        }
                    }
                }
            }

        public People Find(object id)
        {
            using (MySqlCommand command = Connect.CreateCommand())
            {
                command.CommandText = "SELECT `id`,`name`,`createdat`, `active` FROM `people` WHERE `id`=@id";
                command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        return new People
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            CreatedAt = reader.GetDateTime(2),
                            Active = reader.GetBoolean(3)
                        };
                    }
                }
            }
            return null;
        }    
    }
}
