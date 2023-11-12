# Monitoring tests

## Requirements
- k6
- justfile
- kubectl
- terraform, terragrunt
- go, python, dotnet
- pg_dump, psql

## Deploy
To deploy, follow these steps:
```shell
cd deploy
just apply
```

## Endpoints
### Postgres
- Host: localhost:5432
- Login: postgres
- Password: pgpass

Postgres is used for storing Grafana dashboards. During the deployment database is restored from "deploy/postgres/dump.sql" and during destroy it dumps to "deploy/postgres/dump.sql". Grafana dashboards are built using the Grafana UI, and I find this approach more convenient than using Grafonnet. Hence, the usage of Postgres and pg_dump.

### Grafana
- Host: localhost:3000
- Login: admin
- Password: prom-operator

Grafana provides default dashboards from the kube-prometheus-stack in the "General" folder, and there are custom dashboards in the "Test" folder.

### Prometheus
- Host: localhost:9090
- Login: admin
- Password: prom-operator

### Myapp
- Host: localhost:5000

### Myapp OpenTelemetry Collector metrics
- Host: localhost:8889/metrics