#!/bin/bash
_rdbcli() {
    local cur prev words cword
    _init_completion -n : || return

    local core_commands="database execute"
    local database_subcommands="add delete list"
    local db_types="Firebird MySql ODBC Oracle PostgreSQL Sqlite SqlServer"
    local add_opts="-n --name -c --connection-string -t --type -h --help"
    local delete_opts="-n --name -h --help"
    local list_opts="-h --help"
    local execute_opts="-d --database -s --sql -f --file -o --output -h --help"

    if [[ $cword -eq 1 ]]; then
        COMPREPLY=($(compgen -W "$core_commands --help --version" -- "$cur"))
        return
    fi

    case "${words[1]}" in
        database)
            if [[ $cword -eq 2 ]]; then
                COMPREPLY=($(compgen -W "$database_subcommands --help" -- "$cur"))
                return
            fi
            case "${words[2]}" in
                add)
                    if [[ $prev == "-t" || $prev == "--type" ]]; then
                        COMPREPLY=($(compgen -W "$db_types" -- "$cur"))
                    else
                        COMPREPLY=($(compgen -W "$add_opts" -- "$cur"))
                    fi
                    ;;
                delete)
                    COMPREPLY=($(compgen -W "$delete_opts" -- "$cur"))
                    ;;
                list)
                    COMPREPLY=($(compgen -W "$list_opts" -- "$cur"))
                    ;;
            esac
            ;;
        execute)
            COMPREPLY=($(compgen -W "$execute_opts" -- "$cur"))
            ;;
        *)
            COMPREPLY=($(compgen -W "--help --version" -- "$cur"))
            ;;
    esac
}
complete -F _rdbcli -o bashdefault -o default rdbcli