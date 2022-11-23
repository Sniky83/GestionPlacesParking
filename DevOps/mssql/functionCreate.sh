#!/usr/bin/bash
createUserDB() 
{
    /opt/mssql-tools/bin/sqlcmd -U sa -P !@pside2022! -i /tmp/project/createDB.sql
    /opt/mssql-tools/bin/sqlcmd -U sa -P !@pside2022! -i /tmp/project/createUser.sql
}
