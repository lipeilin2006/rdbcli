# rdbcli

**rdbcli** is a cross-platform command-line tool for managing and interacting with relational databases. It supports multiple database types and provides a simple interface for executing SQL commands, managing database configurations, and more.

## Features

- **Database Management**: Add, delete, and list database configurations.
- **SQL Execution**: Execute SQL queries or commands on configured databases.
- **Multi-Database Support**: Supports SQL Server, MySQL, PostgreSQL, SQLite, Oracle, ODBC, and Firebird.
- **Localization**: Includes support for English (`en-US`) and Chinese (`zh-CN`) languages.
- **Cross-Platform**: Fully supported on Windows, macOS, and Linux.

## Prerequisites

- .NET 10.0 or higher
- Supported database drivers installed (e.g., MySQL, PostgreSQL, etc.)

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/your-repo/rdbcli.git
   cd rdbcli
   ```

2. Build the project:
   ```bash
   dotnet build
   ```

3. Run the application:
   ```bash
   dotnet run --project RDBCLI
   ```

## Usage

### Commands

#### 1. Add a Database Configuration
```bash
rdbcli database add -n <name> -c <connection-string> -t <database-type>
```
- `-n`: Database name
- `-c`: Connection string
- `-t`: Database type (e.g., `SqlServer`, `MySql`, `PostgreSQL`, etc.)

#### 2. Delete a Database Configuration
```bash
rdbcli database delete -n <name>
```
- `-n`: Database name

#### 3. List All Database Configurations
```bash
rdbcli database list
```

#### 4. Execute SQL Commands
```bash
rdbcli execute -d <database-name> -s "<sql-statement>"
```
- `-d`: Database name
- `-s`: SQL statement to execute
- `-f`: Path to a SQL file (optional)
- `-o`: Output CSV file path (optional)

### Example
```bash
rdbcli database add -n TestDB -c "Server=localhost;Database=test;User Id=sa;Password=your_password;" -t SqlServer
rdbcli execute -d TestDB -s "SELECT * FROM Users"
```

## Configuration

The tool stores database configurations in a YAML file located at:
```
%APPDATA%\rdbcli\config.yaml
```

## Localization

The tool supports the following languages:
- English (default)
- Chinese

To change the language, set the `Culture` property in the application.

## License

This project is licensed under the [MIT License](LICENSE).

## Contributing

Contributions are welcome! Feel free to submit issues or pull requests.

## Acknowledgments

- Built with .NET and System.CommandLine.
- Uses [ConsoleTables](https://github.com/khalidabuhakmeh/ConsoleTables) for table formatting.
- Localization powered by `.resx` resource files.
