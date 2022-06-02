# DiscordBot Guide's

Consist of:
- PostgreSQL (Data Source with Extension - pg_amqp)
- RabbitMQ (Get, Store, Send message)
- Discord Bot (Process the message)

## Installation

#### Install RabbitMQ

Source: 

https://www.rabbitmq.com/install-debian.html#manual-installation

Steps:

sudo apt-get update

sudo apt-get -y install socat logrotate init-system-helpers adduser

sudo apt-get -y install wget

wget https://github.com/rabbitmq/rabbitmq-server/releases/..

sudo dpkg -i rabbitmq-server_3.9.13-1_all.deb

rm rabbitmq-server_3.9.13-1_all.deb 

systemctl enable rabbitmq-server

systemctl start rabbitmq-server

netstat -nl | grep -E -i "proto|5672"  *if you want to check

#### Add user to RabbitMQ:

rabbitmqctl add_user login password

rabbitmqctl set_user_tags login administrator

rabbitmqctl set_permissions -p / login ".*" ".*" ".*"

rabbitmq-plugins enable rabbitmq_management

Try to log in:

http://localhost:15672/#/

Go to "Queues":

http://localhost:15672/#/queues

add one queue: first_queue

#### Install extension

Sorces:

https://awide.io/postresql-and-rabbitmq/

http://onreader.mdl.ru/RabbitMQInDepth/content/Ch10.html

Steps:

1)
    open linux terminal
2)
    git clone https://github.com/omniti-labs/pg_amqp.git
3)
    cd pg_amqp
4)
    make
5)
    make install
6)
    Add this line to my postgresql.conf file:

    shared_preload_libraries = 'pg_amqp.so'
    
    (you can use nano to edit file "13" - installed version of PostgreSQL)
    
    nano /etc/postgresql/13/main/postgresql.conf
    
7) Create extension:  
    
    psql -U postgres postgres
    
    CREATE EXTENSION amqp;
    
8) Adding broker:

    INSERT INTO amqp.broker (host, port, vhost, username, password)
      VALUES ('localhost', 5672, '/', 'guest', 'guest')
      RETURNING broker_id;

9) Open "amqpScript" at pgAdmin and read notes

10) check - Is RabbiqMQ gets messges?

http://localhost:15672/#/queues

#### Clone DiscordBot repository

"Release" branch have all features - download it, follow notes in project and paste your paths, check:
- Bot.cs
- Consumer.cs
- config.json - paste your bot's token
- lauch.json - paste your path to DiscordBot.dll

## Usage

prefix for bot - '?'

Commands list:
- ?ping
- ?help / ?help <command> (ex: ?help ping)

## Credits

@shikich
