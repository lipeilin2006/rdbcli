#!/usr/bin/tcsh
complete rdbcli \
    'p1=(database execute --help --version)' \
    'p2:database=(add delete list --help)' \
    'p3:add=(-n --name -c --connection-string -t --type -h --help)' \
    'p3:delete=(-n --name -h --help)' \
    'p3:list=(-h --help)' \
    'p2:execute=(-d --database -s --sql -f --file -o --output -h --help)' \
    'p4:-t=(Firebird MySql ODBC Oracle PostgreSQL Sqlite SqlServer)' \
    'p4:--type=(Firebird MySql ODBC Oracle PostgreSQL Sqlite SqlServer)'