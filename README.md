# Microservices-Training

## Excercise

In this exercise we'll have a little experience of how to create a small microservices based system.

### The Goal

Having 2 different, custom created microservices that talk together with each other.
Along the way, one team may decide to how their slice of the architechture works. For example, which database they communicate with.
The other team is unaffected by this choice made by the first team.

### Motive

We essentially simulate a microservices system created by 2 different teams.

First team, responsible for creating the first microservice
Second team, responsible for creating the second microservice.

The first team should be able to change the way their bounded context, i.e., their slice of the microservices work as long as they don't break the contract made between the two teams. The second team might even be unaware of the changes done in the first team since.

This is how we'll simulate the independence between the teams.

### Note

We'll accept data loss that will happen when the first team decide to switch their database. While in reality the first team will need to make sure their persistant data have been transfered to the new database, and there are ways of doing so, it is outside the scope of this exercise.

## Set up

Here we'll write some infrastructure set up needed to be done for the excercise.

### Docker desktop

We'll run our system using images that run on the Docker Desktop application available for free in windows.

1) Download Docker-Desktop application.
2) Run it.
3) Make sure it works by running some docker commands.

Here we'll manage our containers.

### First database: Redis

Redis is a lightweight, quick, distributed key-value store that saves data on a RAM. But it can be used as a database. It has many modules, but in our example we have wanted to keep things as bare-bones as ppossible. Therefore, we're using basic Redis key-value store.

Apps:
- Redis = Our database management system. By the official Redis company.
- Redis insight = our GUI to the redis database. By the official Redis company.

Sources:
- https://redis.io/docs/latest/operate/oss_and_stack/install/install-stack/docker/

1) open cmd
2) run command: `docker run -d --name redis-stack -p 6379:6379 -p 8001:8001 redis/redis-stack:latest`
3) Enter the GUI using http://localhost:8081

### Second database: postgreSQL (also shortened simply to postgres)

PostgreSql is a well known, open-source SQL Database Management System (DBMS) that we can use to create SQL (table based) databases. We'll use it as our second database option.

Apps:
- PostgreSQL = Our database management system. By the official PostgreSQL.
- pgweb = our GUI to the PostgreSQL database. By an open-source contributer found on github

Sources:
- https://www.postgresql.org/
- https://github.com/sosedoff/pgweb?tab=readme-ov-file

1) open cmd
2) run command: `docker run --name <your-container-name> -e POSTGRES_PASSWORD=<your-password>  -p 5432:5432 -d postgres`
3) run commmand: `docker run -p 8081:8081 sosedoff/pgweb`
4) Enter the GUI using http://localhost:8081
5) Login:

![image](https://github.com/user-attachments/assets/9ff10f9d-a9fa-4069-8d1d-3f94ba6b280f)

    - Host = the current ip of your computer (using localhost won't work here).
    - port = 5432
    - Username = the username you set up for your postgres container.
    - Password = <your-password>
    - Database = the name of the  you want to connect to. There always exists a default database called "postgres".
    - SSL Mode = disable.
