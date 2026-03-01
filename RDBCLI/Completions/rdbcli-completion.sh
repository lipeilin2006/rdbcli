#!/bin/sh
_rdbcli_completion() {
    if [ -z "$COMP_WORDS" ] || [ -z "$COMP_CWORD" ]; then
        return 0
    fi

    local cur="${COMP_WORDS[COMP_CWORD]}"
    local prev="${COMP_WORDS[COMP_CWORD-1]}"
    local core_commands="database execute --help --version"
    local database_subcommands="add delete list --help"
    local db_types="Firebird MySql ODBC Oracle PostgreSQL Sqlite SqlServer"
    local add_opts="-n --name -c --connection-string -t --type -h --help"
    local delete_opts="-n --name -h --help"
    local list_opts="-h --help"
    local execute_opts="-d --database -s --sql -f --file -o --output -h --help"

    if [ "$COMP_CWORD" -eq 1 ]; then
        COMPREPLY=$(compgen -W "$core_commands" -- "$cur")
        return
    fi

    case "${COMP_WORDS[1]}" in
        database)
            if [ "$COMP_CWORD" -eq 2 ]; then
                COMPREPLY=$(compgen -W "$database_subcommands" -- "$cur")
                return
            fi
            case "${COMP_WORDS[2]}" in
                add)
                    if [ "$prev" = "-t" ] || [ "$prev" = "--type" ]; then
                        COMPREPLY=$(compgen -W "$db_types" -- "$cur")
                    else
                        COMPREPLY=$(compgen -W "$add_opts" -- "$cur")
                    fi
                    ;;
                delete)
                    COMPREPLY=$(compgen -W "$delete_opts" -- "$cur")
                    ;;
                list)
                    COMPREPLY=$(compgen -W "$list_opts" -- "$cur")
                    ;;
            esac
            ;;
        execute)
            COMPREPLY=$(compgen -W "$execute_opts" -- "$cur")
            ;;
        *)
            COMPREPLY=$(compgen -W "--help --version" -- "$cur")
            ;;
    esac
}

if command -v complete >/dev/null 2>&1; then
    complete -F _rdbcli_completion rdbcli
fi