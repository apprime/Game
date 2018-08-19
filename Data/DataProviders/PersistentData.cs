using MySql.Data.MySqlClient;
using System.Data;

namespace Data.DataProviders
{
    public class MySqlParameterCollectionExtensions
    {
        public static MySqlParameterCollection BindParam<T>(this MySqlParameterCollection parameters, MySqlCommand cmd, string name, T value, DbType type)
        {
            parameters.Add(new MySqlParameter
            {
                ParameterName = name,
                DbType = type,
                Value = value,
            });

            return parameters;
        }
    }
}
