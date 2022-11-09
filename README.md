# M2MT-system
 Model-To-Model Translation system is designed to offer support for creating and maintaining translations between two or more models.

## !**Important**!
- Since the backend uses npgsql, don't forget to add/ change the connections string in the `test-env.json` and `appsettings.Development.json`. Which includes:
  - password
  - location
  - username
  - database, since database, if not defined, is equal to username
