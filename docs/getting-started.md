# Getting Started

Run IdentityServer as Docker container

```sh
docker run -p 5000:5000 identitybasenet/identitybase
```

```yml
version: "2.0"

services:

  # https://hub.docker.com/_/postgres/
  postgres:
    restart: unless-stopped
    ports:
      - 5432:5432
      - 5433:5433
    environment:
      - POSTGRES_PASSWORD=root

  # https://hub.docker.com/_/rabbitmq/
  rabbitmq:
    restart: unless-stopped
    image: rabbitmq:3-management
    ports:
      - 15672:15672
      - 5672:5672
    environment:
      - RABBITMQ_ERLANG_COOKIE=SWQOKODSQALRPCLNMEQG
      - RABBITMQ_DEFAULT_USER=rabbitmq
      - RABBITMQ_DEFAULT_PASS=rabbitmq
      - RABBITMQ_DEFAULT_VHOST=/

  identitybase:
    restart: unless-stopped
    image: identitybasenet/identitybase
    ports:
      - 5000:5000
    environment:
      - FOO=bar
```


### more info is comming soon!