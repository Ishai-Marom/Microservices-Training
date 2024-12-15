# Microservices-Training

## Exercise

In this exercise we'll have a little experience of how to create a small microservices based system.

### The Goal

Having 2 different, custom created microservices that talk together with each other.
Along the way, one team may decide to how their slice of the architecture works. For example, which database they communicate with.
The other team is unaffected by this choice made by the first team.

### The Motive

We essentially simulate a microservices system created by 2 different teams.

First team, responsible for creating the first microservice
Second team, responsible for creating the second microservice.

The first team should be able to change the way their bounded context, i.e., their slice of the microservices work as long as they don't break the contract made between the two teams. The second team might even be unaware of the changes done in the first team since.

This is how we'll simulate the independence between the teams.

### Note

We'll accept data loss that will happen when the first team decide to switch their database. While in reality the first team will need to make sure their persistent data have been transfered to the new database, and there are ways of doing so, it is outside the scope of this exercise.

## Set up

### IDE

Some options:
- Using Visual Studio 2022. Best option if it works properly for you.
- Using Visual Studio Code with at least these extensions:
    - .NET Install Tool
    - C#
    - C# Dev Kit
    - C# Extensions
- Any other you see fit.

Here we'll write some infrastructure set up needed to be done for the exercise.

In this example, main solution and sub projects were created with Visual Studio 2022.
I recommend creating project with this. I have no experience of doing it using Visual Studio Code.

#### Server processes
Were created using Visual Studio 2022 "Console App". Make sure you choose the C# version.

#### Web API process
Was created using Visual Studio 2022 "ASP.NET Core Web API". Make sure you choose the C# version.
The custom controller was created using Visual Studio Code: <add> -> <controller> -> <empty controller> to the directory.
I recommend creating classes (or at least controller) for this solution only using Visual Studio 2022 because of possible ways it might edit stuff in the environment.

The external port is found within the `Properties\LaunchSetting.json` file
When you run locally, it will link the API to that port.
When you run in a container, it will use 8080 port. You'll need to use the following command to be able to reach the API properly:
`docker run --name <container-name> -p <external-port>:8080 -d <image-name>:<image-tag>`
if that does not work, try this:
`docker run --name <container-name> -p 5186:8080 -e HOST=<computer-ip> -d <image-name>:<image-tag>`

### Docker desktop

We'll run our system using images that run on the Docker Desktop application available for free in windows.

1) Download Docker-Desktop application.
2) Run it.
3) Make sure it works by running some docker commands.

Here we'll manage our containers.

### Building an image
Go to the directory that contains the Dockerfile and run the command:
`docker build -t <image-repository-name>:<image-tag> -f Dockerfile .`
You can choose whichever <image-repository-name> and <image-tag> as you would like. I recommend avoiding using `latest` as the <image-tag> because docker images use this tag as the default for pulling images.

Note, in case you create a new project and you want to copy an existing Dockerfile from these projects to the new one, you'll need to at least change the final line of that file.

### First database: Redis

Redis is a lightweight, quick, distributed key-value store that saves data on a RAM. But it can be used as a database. It has many modules, but in our example we have wanted to keep things as bare-bones as possible. Therefore, we're using basic Redis key-value store.

Apps:
- Redis = Our database management system. By the official Redis company.
- Redis insight = our GUI to the redis database. By the official Redis company.

Sources:
- https://redis.io/docs/latest/operate/oss_and_stack/install/install-stack/docker/

1) open cmd
2) run command: `docker run -d --name redis-stack -p 6379:6379 -p 8001:8001 redis/redis-stack:latest`
3) Enter the GUI using http://localhost:8001

### Second database: postgreSQL (also shortened simply to postgres)

PostgreSql is a well known, open-source SQL Database Management System (DBMS) that we can use to create SQL (table based) databases. We'll use it as our second database option.

Apps:
- PostgreSQL = Our database management system. By the official PostgreSQL.
- pgweb = our GUI to the PostgreSQL database. By an open-source contributer found on git-hub

Sources:
- https://www.postgresql.org/
- https://github.com/sosedoff/pgweb?tab=readme-ov-file

1) open cmd
2) run command: `docker run --name postgres -e POSTGRES_PASSWORD=<your-password> -p 5432:5432 -d postgres`
3) run command: `docker run --name pgweb -p 8081:8081 -d sosedoff/pgweb`
4) Enter the GUI using http://localhost:8081
5) Login:

![image](https://github.com/user-attachments/assets/9ff10f9d-a9fa-4069-8d1d-3f94ba6b280f)

    - Host = the current ip of your computer (using localhost won't work here).
    - port = 5432
    - Username = the username you set up for your postgres container.
    - Password = <your-password>
    - Database = the name of the you want to connect to. There always exists a default database called "postgres".
    - SSL Mode = disable.

## Important

Because the containers connect to databases, then they need to know who is the database host. When developing locally then the host is simply `localhost`, but when moving to running containers this is no longer the case.
In that case you'll need to put your computer current ip for it to work.
I added an environment variable called `HOST` that will tell the app that the databases server is running on the host ip written there, otherwise it will point them to `localhost`.


### Exercise Note
When running the containers, I personally was not able to talk with the API when running it like that:
- Web API: run locally
- API client service : run on a container.

The rest I was able to run.
- Both Locally
- Both in a container
- Reverse version:
    - Web API: run in a container
    - API client service: run locally.

I'm not sure why, so if you encounter the same problem don't waste time on it. The other ways of running it should work as the guide tells you.

### Final Note

The code here is simply meant as an example of a simple microservice architecture. Do not consider this code as production ready in any way.
- It does not guarantee Cyber-Security.
- Its not backward or forward compatible.
- Its not using best C# practices.
- Its not using best practices for docker, or a docker orchestration infrastructure such as Kubernetes/Openshift.
- Its not using service discovery properly
- Its not even designed well.
Its simply meant to have a little simulation of what its like to create microservices architecture so you could see benefits that this architecture gives you as written in the "The Motive" for this exercise.

Good Luck :)
