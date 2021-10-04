using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cash_Register_App
{
    public interface ProductSqlite
    {
        SQLiteConnection GetConnection();
    }
}
