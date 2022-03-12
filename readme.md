## Running Locally

### Starting RabbitMq
```
docker run \
    -it --rm --name rabbitmq \
    -p 5672:5672 -p 15672:15672 \
    rabbitmq:3.8-management
```

#### RabbitMq Management
- http://localhost:15762/
- Admin User - admin / doodle
- Principle User - nitro / 50BD963C-6224-44DC-90A5-8F25F456BE07

### Starting MongoDb
```
docker run \
    -it --rm --name mongodb-msvc-tenant \
	-p 27017:27017 \
	-e MONGODB_PORT_NUMBER=27017 \
	-e MONGODB_USERNAME=nitro \
	-e MONGODB_PASSWORD=B193DC65-97F4-4F76-BE84-0246C420AC18 \
	bitnami/mongodb:latest
```

### Starting Solution
- Local Debug - Run Combine "All Local"
- Local Debug Docker Compose - Run Combine "Compose"

## Project Tye
- Docker `tye run --docker --dashboard`

