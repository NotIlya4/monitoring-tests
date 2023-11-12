terraform {
  required_providers {
    shell = {
      source = "scottwinkler/shell"
      version = "1.7.10"
    }
  }
}

provider "shell" { }

variable "password" {
  default = "pgpass"
}

resource "helm_release" "postgres" {
  chart = "postgresql"
  name  = "postgres"
  repository = "oci://registry-1.docker.io/bitnamicharts"
  
  namespace = "postgres"
  create_namespace = true

  values = [file("values.yaml")]
  
  set {
    name  = "auth.postgresPassword"
    value = var.password
  }
  
  set {
    name  = "primary.initdb.user"
    value = "postgres"
  }
  
  set {
    name  = "primary.initdb.password"
    value = var.password
  }
}

resource "shell_script" "grafana_migrations" {
  lifecycle_commands {
    create = "just restore"
    delete = "just dump"
  }
  
  depends_on = [helm_release.postgres]
}

output "username" {
  value = "postgres"
}

output "password" {
  value = var.password
}

output "host" {
  value = "postgres.postgres.svc.cluster.local"
}

output "port" {
  value = "5432"
}