#!/usr/bin/zsh
#compdef rdbcli

_rdbcli() {
    local curcontext="$curcontext" state line
    typeset -A opt_args

    local core_commands=(
        'database:管理数据库配置'
        'execute:执行SQL语句'
        '--help:显示帮助'
        '--version:显示版本'
    )

    local database_subcommands=(
        'add:添加配置'
        'delete:删除配置'
        'list:列出配置'
        '--help:显示帮助'
    )

    local db_types=('Firebird' 'MySql' 'ODBC' 'Oracle' 'PostgreSQL' 'Sqlite' 'SqlServer')

    _arguments \
        '1: :->core_cmd' \
        '*:: :->sub_cmd'

    case $state in
        core_cmd)
            _describe 'command' core_commands
            ;;
        sub_cmd)
            case $words[2] in
                database)
                    _arguments \
                        '2: :->database_subcmd' \
                        '*:: :->database_opts'
                    case $state in
                        database_subcmd)
                            _describe 'subcommand' database_subcommands
                            ;;
                        database_opts)
                            case $words[3] in
                                add)
                                    _arguments \
                                        '-n[名称]:name:' \
                                        '--name[名称]:name:' \
                                        '-c[连接字符串]:connection string:' \
                                        '--connection-string[连接字符串]:connection string:' \
                                        '-t[类型]:type:($db_types)' \
                                        '--type[类型]:type:($db_types)' \
                                        '-h[帮助]' \
                                        '--help[帮助]'
                                    ;;
                                delete)
                                    _arguments \
                                        '-n[名称]:name:' \
                                        '--name[名称]:name:' \
                                        '-h[帮助]' \
                                        '--help[帮助]'
                                    ;;
                                list)
                                    _arguments \
                                        '-h[帮助]' \
                                        '--help[帮助]'
                                    ;;
                            esac
                            ;;
                    esac
                    ;;
                execute)
                    _arguments \
                        '-d[数据库]:database:' \
                        '--database[数据库]:database:' \
                        '-s[SQL语句]:sql:' \
                        '--sql[SQL语句]:sql:' \
                        '-f[文件]:file:_files' \
                        '--file[文件]:file:_files' \
                        '-o[输出]:output:_files' \
                        '--output[输出]:output:_files' \
                        '-h[帮助]' \
                        '--help[帮助]'
                    ;;
            esac
            ;;
    esac
}
compdef _rdbcli rdbcli